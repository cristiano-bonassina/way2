using System;
using LogicArt.Framework.Core.DependencyInjection;

namespace Way2.Infrastructure.DependencyResolution
{
    public class InstanceProvider : IInstanceProvider
    {

        private readonly IServiceProvider _serviceProvider;

        public InstanceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            var disposable = _serviceProvider as IDisposable;
            disposable?.Dispose();
        }

        public T GetInstance<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        public object GetInstance(Type type)
        {
            return _serviceProvider.GetService(type);
        }

    }
}
