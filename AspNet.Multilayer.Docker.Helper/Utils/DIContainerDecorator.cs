using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Resolving;

namespace AspNet.Multilayer.Docker.Helper
{
    public class DIContainerDecorator : IContainer, IServiceResolver
    {
        private readonly Container ContainerInstance;

        public DIContainerDecorator(Container container)
        {
            this.ContainerInstance = container;
        }
        
        public IDisposer Disposer => this.ContainerInstance.Disposer;
        public object Tag => this.ContainerInstance.Tag;

        public event EventHandler<LifetimeScopeBeginningEventArgs> ChildLifetimeScopeBeginning
        {
            add => this.ContainerInstance.ChildLifetimeScopeBeginning += value;
            remove => this.ContainerInstance.ChildLifetimeScopeBeginning -= value;
        }
        
        public event EventHandler<LifetimeScopeEndingEventArgs> CurrentScopeEnding
        {
            add => this.ContainerInstance.CurrentScopeEnding += value;
            remove => this.ContainerInstance.CurrentScopeEnding -= value;
        }
        
        public event EventHandler<ResolveOperationBeginningEventArgs> ResolveOperationBeginning
        {
            add => this.ContainerInstance.ResolveOperationBeginning += value;
            remove => this.ContainerInstance.ResolveOperationBeginning -= value;
        }
        
        public DiagnosticListener DiagnosticSource => this.ContainerInstance.DiagnosticSource;
        public IComponentRegistry ComponentRegistry => this.ContainerInstance.ComponentRegistry;
        
        public object ResolveComponent(ResolveRequest request)
        {
            return this.ContainerInstance.ResolveComponent(request);
        }

        public void Dispose()
        {
            this.ContainerInstance.Dispose();
        }

        public object Resolve(Type serviceType)
        {
            return this.ContainerInstance.GetService(serviceType);
        }

        public ValueTask DisposeAsync()
        {
            return this.ContainerInstance.DisposeAsync();
        }

        public ILifetimeScope BeginLifetimeScope()
        {
            return this.ContainerInstance.BeginLifetimeScope();
        }

        public ILifetimeScope BeginLifetimeScope(object tag)
        {
            return this.ContainerInstance.BeginLifetimeScope(tag);
        }

        public ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return this.ContainerInstance.BeginLifetimeScope(configurationAction);
        }

        public ILifetimeScope BeginLifetimeScope(object tag, Action<ContainerBuilder> configurationAction)
        {
            return this.ContainerInstance.BeginLifetimeScope(tag, configurationAction);
        }

        public object? GetService(Type serviceType)
        {
            return this.ContainerInstance.GetService(serviceType);
        }
    }
}