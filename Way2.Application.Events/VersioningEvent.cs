using System.Threading.Tasks;
using LogicArt.Framework.Core.Bus;
using LogicArt.Framework.Core.Bus.Abstractions;
using LogicArt.Framework.Core.DependencyInjection;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Application.Events
{
    /// <summary>
    /// This event increase the version of entities before they are persisted in the database
    /// </summary>
    public class VersioningEvent : Event, IPreInsertEvent, IPreUpdateEvent
    {

        public VersioningEvent(IInstanceProvider provider) : base(provider)
        {
        }

        public Task<bool> OnPreInsertAsync(object entity)
        {
            ((IPersistable)entity).Version++;
            return Task.FromResult(false);
        }


        public Task<bool> OnPreUpdateAsync(object previousEntity, object newEntity)
        {
            ((IPersistable)newEntity).Version++;
            return Task.FromResult(false);
        }

    }
}
