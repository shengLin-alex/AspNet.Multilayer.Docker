using Autofac;

namespace AspNet.Multilayer.Docker.Helper
{
    /// <summary>
    /// Autofac Type register interface
    /// </summary>
    public interface ITypeRegistrar
    {
        /// <summary>
        /// register order
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Register type
        /// </summary>
        /// <param name="builder">Used to build an <see cref="T:Autofac.IContainer" /> from component registrations.</param>
        void Register(ContainerBuilder builder);
    }
}
