using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Infrastructure.Persistence.Mapping
{
    /// <summary>
    /// Base class for database configuration of persisted entities
    /// </summary>
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntity<Guid>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
