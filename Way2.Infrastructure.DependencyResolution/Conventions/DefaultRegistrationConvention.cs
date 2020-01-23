using System;
using System.Linq;
using System.Reflection;
using Lamar;
using Lamar.Scanning;
using Lamar.Scanning.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Way2.Infrastructure.DependencyResolution.Conventions
{

    public class DefaultRegistrationConvention : IRegistrationConvention
    {

        public void ScanTypes(TypeSet types, ServiceRegistry services)
        {
            foreach (var type in types.FindTypes(TypeClassification.Concretes).Where(type => type.GetConstructors().Any()))
            {
                var serviceType = FindPluginType(type);
                if (serviceType != null && ShouldAdd(services, serviceType, type))
                {
                    services.AddScoped(serviceType, type);
                }
            }
        }

        public bool ShouldAdd(IServiceCollection services, Type serviceType, Type implementationType)
        {

            var matches = services.Where(x => x.ServiceType == serviceType).ToArray();
            if (!matches.Any())
            {
                return true;
            }

            var hasMatch = matches.Any(x => x.Matches(serviceType, implementationType));
            return !hasMatch;

        }

        public virtual Type FindPluginType(Type concreteType)
        {
            var interfaceName = "I" + concreteType.Name;
            return concreteType.GetTypeInfo().GetInterfaces().FirstOrDefault(t => t.Name == interfaceName);
        }

    }

}
