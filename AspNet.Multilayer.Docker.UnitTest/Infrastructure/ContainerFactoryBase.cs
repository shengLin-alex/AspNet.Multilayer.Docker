using System;
using System.Globalization;
using System.Reflection;
using AspNet.Multilayer.Docker.Helper;
using Autofac;
using Autofac.Core;

namespace AspNet.Multilayer.Docker.UnitTest
{
    /// <summary>
    /// 建構相依性注入容器的基底類別，使用抽象單例模式
    /// </summary>
    public abstract class ContainerFactoryBase<TContainerFactory>
        where TContainerFactory : ContainerFactoryBase<TContainerFactory>
    {
        /// <summary>
        /// 單一實例，延遲載入
        /// </summary>
        private static Lazy<TContainerFactory> Instance;

        /// <summary>
        /// DI 容器實例
        /// </summary>
        private IContainer ContainerInstance;

        /// <summary>
        /// 除了底層共同註冊之外，不同的 TestClass 額外註冊的類別委派
        /// </summary>
        protected readonly Action<ContainerBuilder> ExternalTypeRegister;

        /// <summary>
        /// 建構子
        /// </summary>
        protected ContainerFactoryBase()
        {
            this.ExternalTypeRegister = null;
        }

        /// <summary>
        /// 建構子，支援額外類型註冊
        /// </summary>
        /// <param name="externalTypeRegister">除了底層共同註冊之外，不同的 TestClass 額外註冊的類別委派</param>
        protected ContainerFactoryBase(Action<ContainerBuilder> externalTypeRegister)
        {
            this.ExternalTypeRegister = externalTypeRegister;
        }
        
        /// <summary>
        /// 取得 Container
        /// </summary>
        /// <returns>IContainer</returns>
        public IContainer Container => this.ContainerInstance ?? (this.ContainerInstance = this.BuildContainer());

        /// <summary>
        /// 建立具備額外類型註冊的 UnitTestContainerBuilder 之單一實例。
        /// <para>在真正呼叫 <see cref="GetInstance"/> 之前，允許重新設定。</para>
        /// </summary>
        /// <param name="externalTypeRegister">除了底層共同註冊之外，不同的 TestClass 額外註冊的類別委派</param>
        public static void InitExternalRegistrarInstance(Action<ContainerBuilder> externalTypeRegister)
        {
            if (Instance == null || !Instance.IsValueCreated)
            {
                Instance = new Lazy<TContainerFactory>(() => (TContainerFactory)Activator.CreateInstance(
                    typeof(TContainerFactory),
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                    null,
                    new object[] { externalTypeRegister },
                    CultureInfo.InvariantCulture));
            }
        }
        
        /// <summary>
        /// 取得 TContainerFactory 的單一實例，
        /// <para> 若測試類別未呼叫 <see cref="InitExternalRegistrarInstance"/> </para>
        /// 則會取得無額外註冊類型的原始 TContainerFactory
        /// </summary>
        /// <returns>TContainerFactory</returns>
        public static TContainerFactory GetInstance()
        {
            Instance ??= new Lazy<TContainerFactory>(() => (TContainerFactory) Activator.CreateInstance(
                typeof(TContainerFactory),
                true));

            return Instance.Value;
        }
        
        /// <summary>
        /// 重設 ContainerBuilder
        /// </summary>
        public static void RestContainerBuilder()
        {
            if (Instance == null || !Instance.IsValueCreated)
            {
                return;
            }

            Instance.Value.Container.Dispose();
            Instance = null;
        }
        
        /// <summary>
        /// 建構容器
        /// </summary>
        /// <returns>IContainer</returns>
        protected IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            this.RegisterTypes(builder);
            
            this.ExternalTypeRegister?.Invoke(builder);
            IContainer container = builder.Build();

            if (container is Container c)
            {
                DependencyResolver.Current.SetResolver(new DIContainerDecorator(c));
            }

            return container;
        }
        
        /// <summary>
        /// 於子類實作之註冊類型之設定
        /// </summary>
        /// <param name="builder">Used to build an <see cref="T:Autofac.IContainer" /> from component registrations.</param>
        protected abstract void RegisterTypes(ContainerBuilder builder);
    }
}