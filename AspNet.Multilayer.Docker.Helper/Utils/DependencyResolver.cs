using System;
using AspectCore.DependencyInjection;

namespace AspNet.Multilayer.Docker.Helper
{
    /// <summary>
    /// Dependency Resolver
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Resolver's Lazy instance
        /// </summary>
        private static Lazy<DependencyResolver> ResolverInstance;

        /// <summary>
        /// SyncRoot
        /// </summary>
        private static readonly object SyncRoot = new();

        /// <summary>
        /// DI Resolver
        /// </summary>
        private IServiceResolver Resolver;

        /// <summary>
        /// Get single instance of Dependency Resolver
        /// </summary>
        public static IDependencyResolver Current
        {
            get
            {
                if (ResolverInstance != null && ResolverInstance.IsValueCreated)
                {
                    return ResolverInstance.Value;
                }

                lock (SyncRoot)
                {
                    ResolverInstance = new Lazy<DependencyResolver>(() => new DependencyResolver());
                }

                return ResolverInstance.Value;
            }
        }

        /// <summary>
        /// private constructor
        /// </summary>
        private DependencyResolver()
        {
        }

        /// <summary>
        /// set up resolver
        /// </summary>
        /// <param name="resolver">di service resolver</param>
        public void SetResolver(IServiceResolver resolver)
        {
            this.Resolver = resolver;
        }

        /// <summary>
        /// Resolve specified service
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <returns>specified service object</returns>
        public TService Resolve<TService>()
        {
            return this.Resolver.Resolve<TService>();
        }

        /// <summary>
        /// Resolve specified service by specified key
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <param name="key">specified key</param>
        /// <returns>specified service object</returns>
        public TService ResolveKeyed<TService>(object key)
        {
            // return this.Resolver.ResolveKeyed<TService>(key);
            throw new NotImplementedException("AspectCore not support keyed resolve!");
        }
    }
}
