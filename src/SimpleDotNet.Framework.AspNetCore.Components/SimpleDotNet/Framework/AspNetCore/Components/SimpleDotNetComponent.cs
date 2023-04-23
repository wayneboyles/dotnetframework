using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace SimpleDotNet.Framework.AspNetCore.Components
{
    public abstract class SimpleDotNetComponent : OwningComponentBase
    {
        private ILoggerFactory _loggerFactory;

        private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        protected ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);

        protected ILogger Logger => _lazyLogger.Value;

        [Inject]
        protected IServiceProvider NonScopedServices { get; set; }

        #region GetService Methods

        protected TService LazyGetRequiredService<TService>(ref TService reference) => LazyGetService(typeof(TService), ref reference);

        protected TService LazyGetNonScopedRequiredService<TService>(ref TService reference) => LazyGetNonScopedRequiredService(typeof(TService), ref reference);

        protected TService LazyGetNonScopedService<TService>(ref TService reference) => LazyGetNonScopedService(typeof(TService), ref reference);

        protected TRef LazyGetService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                reference = (TRef)ScopedServices.GetService(serviceType)!;
            }

            return reference;
        }

        protected TRef LazyGetNonScopedRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                reference = (TRef)NonScopedServices.GetRequiredService(serviceType);
            }

            return reference;
        }

        protected TRef LazyGetNonScopedService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                reference = (TRef)NonScopedServices.GetService(serviceType)!;
            }

            return reference;
        }

        #endregion
    }
}
