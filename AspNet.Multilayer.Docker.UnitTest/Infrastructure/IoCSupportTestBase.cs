using System;
using Autofac;

namespace AspNet.Multilayer.Docker.UnitTest
{
    /// <summary>
    /// 支援相依性解析之測試類別基底
    /// </summary>
    /// <typeparam name="TContainerFactory">注入容器工廠</typeparam>
    public class IoCSupportedTestBase<TContainerFactory>
        where TContainerFactory : ContainerFactoryBase<TContainerFactory>
    {
        /// <summary>
        /// 建構子
        /// </summary>
        protected IoCSupportedTestBase()
        {
        }

        /// <summary>
        /// 初始化支援額外類型註冊之相依性注入的測試類別
        /// </summary>
        /// <param name="externalRegistrar">額外註冊的類別委派</param>
        protected static void UseExternalRegistrar(Action<ContainerBuilder> externalRegistrar)
        {
            ContainerFactoryBase<TContainerFactory>.InitExternalRegistrarInstance(externalRegistrar);
        }

        /// <summary>
        /// 完成容器使用
        /// </summary>
        protected static void FinishUsingContainer()
        {
            ContainerFactoryBase<TContainerFactory>.RestContainerBuilder();
        }

        /// <summary>
        /// 解析指定服務
        /// </summary>
        /// <typeparam name="TService">指定服務型別</typeparam>
        /// <returns>指定服務</returns>
        protected TService Resolve<TService>()
        {
            return this.UseAutofacContainer().Resolve<TService>();
        }

        /// <summary>
        /// 由鍵值解析指定服務
        /// </summary>
        /// <typeparam name="TService">指定服務型別</typeparam>
        /// <param name="serviceKey">鍵值</param>
        /// <returns>指定服務</returns>
        protected TService ResolveKeyed<TService>(object serviceKey)
        {
            return this.UseAutofacContainer().ResolveKeyed<TService>(serviceKey);
        }

        /// <summary>
        /// 使用 Autofac DI 容器
        /// </summary>
        /// <returns>Autofac DI 容器</returns>
        protected IContainer UseAutofacContainer()
        {
            return ContainerFactoryBase<TContainerFactory>.GetInstance().Container;
        }
    }
}