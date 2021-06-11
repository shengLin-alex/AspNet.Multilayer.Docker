using AspectCore.DependencyInjection;

namespace AspNet.Multilayer.Docker.Helper
{
    /// <summary>
    /// Dependency Resolver interface
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// set container
        /// </summary>
        /// <param name="resolver">di service resolver</param>
        void SetResolver(IServiceResolver resolver);

        /// <summary>
        /// Resolve specified service
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <returns>specified service object</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Resolve specified service by specified key
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <param name="key">specified key</param>
        /// <returns>specified service object</returns>
        TService ResolveKeyed<TService>(object key);
    }
}
