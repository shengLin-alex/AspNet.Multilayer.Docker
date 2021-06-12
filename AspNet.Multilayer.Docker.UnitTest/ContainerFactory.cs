using System;
using Autofac;

namespace AspNet.Multilayer.Docker.UnitTest
{
    public class ContainerFactory : ContainerFactoryBase<ContainerFactory>
    {
        /// <summary>
        /// 建構子
        /// </summary>
        protected ContainerFactory() : base()
        {
        }

        /// <summary>
        /// 建構子，支援額外類型註冊
        /// </summary>
        /// <param name="externalTypeRegister">除了底層共同註冊之外，不同的 TestClass 額外註冊的類別委派</param>
        protected ContainerFactory(Action<ContainerBuilder> externalTypeRegister) : base(externalTypeRegister)
        {
        }
        
        protected override void RegisterTypes(ContainerBuilder builder)
        {
        }
    }
}