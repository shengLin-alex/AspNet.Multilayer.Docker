using System.Reflection;
using AspNet.Multilayer.Docker.Helper;
using Autofac;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// Autofac Type register
    /// </summary>
    public class AutofacTypeRegistrar : ITypeRegistrar
    {
        /// <summary>
        /// register order
        /// </summary>
        public int Order => 3;

        /// <summary>
        /// Register type
        /// </summary>
        /// <param name="builder">Used to build an <see cref="T:Autofac.IContainer" /> from component registrations.</param>
        public void Register(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder
               .RegisterAssemblyTypes(assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
    }
}
