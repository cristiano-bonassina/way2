using System;
using System.Threading.Tasks;
using LogicArt.Framework.Core.Bus;
using LogicArt.Framework.Core.Bus.Abstractions;
using LogicArt.Framework.Core.DependencyInjection;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Application.Events
{
    /// <summary>
    /// This event set the creation and modification date of entities when they are persisted in the database
    /// </summary>
    public class AuditEvent : Event, IPreInsertEvent, IPreUpdateEvent
    {

        public AuditEvent(IInstanceProvider provider) : base(provider)
        {
        }

        public Task<bool> OnPreInsertAsync(object entity)
        {
            ((IAuditable)entity).CreatedAt = DateTimeOffset.Now;
            return Task.FromResult(false);
        }

        public Task<bool> OnPreUpdateAsync(object previousEntity, object newEntity)
        {
            ((IAuditable)newEntity).ModifiedAt = DateTimeOffset.Now;
            return Task.FromResult(false);
        }

    }
}
