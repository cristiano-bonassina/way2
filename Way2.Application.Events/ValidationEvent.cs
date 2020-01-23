using System.Threading.Tasks;
using LogicArt.Framework.Core.Bus;
using LogicArt.Framework.Core.Bus.Abstractions;
using LogicArt.Framework.Core.DependencyInjection;
using Way2.Application.Validations.Abstractions.Extensions;
using Way2.Application.Validations.Exceptions;

namespace Way2.Application.Events
{
    /// <summary>
    /// This event validates entities before they are persisted in the database
    /// </summary>
    public class ValidationEvent : Event, IPreInsertEvent, IPreUpdateEvent
    {

        public ValidationEvent(IInstanceProvider provider) : base(provider)
        {
        }

        public async Task<bool> OnPreInsertAsync(object entity)
        {
            await this.ValidateAsync(entity);
            return false;
        }

        public async Task<bool> OnPreUpdateAsync(object previousEntity, object newEntity)
        {
            await this.ValidateAsync(newEntity);
            return false;
        }

        private async Task ValidateAsync(object entity)
        {

            var entityType = entity.GetType();
            var validation = this.InstanceProvider.GetValidationFor(entityType);
            if (validation == null)
            {
                return;
            }

            var result = await validation.ValidateAsync(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result);
            }

        }

    }
}
