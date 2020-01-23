using System;
using LogicArt.Framework.Core.DependencyInjection;

namespace Way2.Application.Validations.Abstractions.Extensions
{
    public static class InstanceProviderExtensions
    {

        public static IValidation GetValidationFor(this IInstanceProvider instanceProvider, Type entityType)
        {
            var validationType = typeof(IValidation<>).MakeGenericType(entityType);
            return (IValidation)instanceProvider.GetInstance(validationType);
        }

    }
}
