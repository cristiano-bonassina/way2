using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LogicArt.Framework.Core.Bus.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Way2.Infrastructure.Persistence.Conventions;
using Way2.Infrastructure.Persistence.Mapping;

namespace Way2.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        private readonly IEventBus _eventBus;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(ILoggerFactory loggerFactory, IEventBus eventBus)
        {

            _loggerFactory = loggerFactory;
            _eventBus = eventBus;

            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Database.AutoTransactionsEnabled = false;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            const string name = "./app.db";
            var path = Path.Combine(AppContext.BaseDirectory, name);
            var connectionString = $"Data Source={path}";
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            // Insensitive compare 
            connection.CreateCollation("LATIN1_GENERAL_CS_AI", (x, y) => string.Compare(x, y, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace));

            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .UseLazyLoadingProxies()
                .UseSqlite(connection);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

            builder.ApplyConfiguration(new CityTypeConfiguration());
            builder.ApplyConfiguration(new CityWeatherTypeConfiguration());

            this.ApplyNamingConventions(builder);

            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Convert property names to table names
        /// </summary>
        /// <param name="builder"></param>
        private void ApplyNamingConventions(ModelBuilder builder)
        {

            var mapper = new SnakeCaseNameTranslator();
            var types = builder.Model.GetEntityTypes().ToList();

            types.ForEach(e => e.SetTableName(mapper.TranslateTypeName(e)));

            types.SelectMany(e => e.GetProperties())
                .ToList()
                .ForEach(p => p.SetColumnName(mapper.TranslateMemberName(p)));

        }

        private IList<EntityEntry> GetEntityEntries(int skip = 0)
        {
            return this.ChangeTracker
                .Entries()
                .Skip(skip)
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .ToList();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {

            var operations = new List<EntityOperation>();
            var entries = this.GetEntityEntries();
            while (entries.Any())
            {

                var entriesCount = this.ChangeTracker.Entries().Count();

                foreach (var entry in entries)
                {

                    var operation = new EntityOperation(entry);
                    operations.Add(operation);

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            await _eventBus.NotifyPreInsertAsync(operation.Entity);
                            continue;
                        case EntityState.Modified:
                            await _eventBus.NotifyPreUpdateAsync(operation.PreviousEntity, entry.Entity);
                            continue;
                        case EntityState.Deleted:
                            await _eventBus.NotifyPreDeleteAsync(operation.Entity);
                            break;
                    }

                }

                entries = this.GetEntityEntries(entriesCount);

            }

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            foreach (var operation in operations)
            {

                switch (operation.State)
                {
                    case EntityState.Added:
                        await _eventBus.NotifyPostInsertAsync(operation.Entity);
                        continue;
                    case EntityState.Modified:
                        await _eventBus.NotifyPostUpdateAsync(operation.PreviousEntity, operation.Entity);
                        continue;
                    case EntityState.Deleted:
                        await _eventBus.NotifyPostDeleteAsync(operation.Entity);
                        break;
                }

            }

            return result;

        }

        private struct EntityOperation
        {

            public EntityState State { get; }
            public object Entity { get; }
            public object PreviousEntity { get; }

            public EntityOperation(EntityEntry entry)
            {
                this.State = entry.State;
                this.Entity = entry.Entity;
                this.PreviousEntity = entry.OriginalValues?.ToObject();
            }

        }

    }
}