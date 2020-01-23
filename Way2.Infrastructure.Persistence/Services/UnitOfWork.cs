using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Way2.Application.Services;

namespace Way2.Infrastructure.Persistence.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDbContextTransaction _transaction;

        public ApplicationDbContext Context { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await this.Context.Database.BeginTransactionAsync();
        }

        public async Task EndTransactionAsync(bool commit = true)
        {
            if (commit)
            {
                await this.CommitAsync();
            }
            else
            {
                await this.RollbackAsync();
            }
        }

        private async Task CommitAsync()
        {

            while (this.HasChanges())
            {
                await this.Context.SaveChangesAsync();
            }

            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();

        }

        private async Task RollbackAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        private bool HasChanges()
        {
            return this.Context.ChangeTracker.Entries().Any(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified ||
                x.State == EntityState.Deleted);
        }

    }
}
