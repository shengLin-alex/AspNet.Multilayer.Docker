using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AspNet.Multilayer.Docker.Helper
{
    /// <summary>
    /// Log method invoke and out
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodInterceptorAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILogger<MethodInterceptorAttribute> Logger => DependencyResolver.Current.Resolve<ILogger<MethodInterceptorAttribute>>();

        /// <summary>
        /// Invoke this Interceptor
        /// </summary>
        /// <param name="context"><see cref="AspectContext"/></param>
        /// <param name="next">The actual method that runtime invoke</param>
        /// <returns>The task that AOP Invoke</returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                this.Logger.LogInformation($"Before service call -> {context.ImplementationMethod.Name}");
                await next(context);
            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception, exception.Message);
                throw;
            }
            finally
            {
                this.Logger.LogInformation($"After service call -> {context.ImplementationMethod.Name}");
            }
        }
    }
}
