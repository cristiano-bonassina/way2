using LogicArt.Framework.Core.Bus;
using LogicArt.Framework.Core.Bus.Abstractions;
using System;
using System.Linq;
using Way2.Domain.Entities;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Application.Events.Schema
{
    /// <summary>
    /// Configures the events that will be triggered for the entities
    /// </summary>
    public class EventSchemaProvider : IEventSchemaProvider
    {
        public IEventSchema GetSchema()
        {

            var schema = new EventSchema();

            var entityTypes = typeof(Entity<>).Assembly
                .GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IEntity<Guid>).IsAssignableFrom(x))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                var @event = new EventBuilder()
                    .ForEntity(entityType)
                    .WithEvent(typeof(AuditEvent))
                    .WithEvent(typeof(ValidationEvent))
                    .WithEvent(typeof(VersioningEvent))
                    .Build();
                schema.Add(@event);
            }

            return schema;

        }
    }
}
