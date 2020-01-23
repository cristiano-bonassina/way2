using System;
using System.Linq;

namespace Way2.Infrastructure.DependencyResolution.Extensions
{
    public static class TypeExtensions
    {

        public static bool IsAssignableToGenericType(this Type type, Type genericType)
        {

            if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            return type.BaseType != null && IsAssignableToGenericType(type.BaseType, genericType);

        }

    }
}
