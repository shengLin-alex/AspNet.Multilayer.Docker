using System.Collections.Generic;
using System.Linq;
using AspNet.Multilayer.Docker.Repository;
using AspNet.Multilayer.Docker.Repository.Models;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AspNet.Multilayer.Docker.UnitTest
{
    [TestClass]
    public class MockUserRepoTest : IoCSupportedTestBase<ContainerFactory>
    {
        [TestInitialize]
        public void TestInit()
        {
            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetAll()).Returns(new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Jackson"
                },
                new User
                {
                    Id = 2,
                    Name = "Anne"
                }
            });
            
            UseExternalRegistrar(builder =>
            {
                builder.RegisterInstance(mockUserRepo.Object)
                    .As<IUserRepository>()
                    .SingleInstance();
            });
        }
        
        /// <summary>
        /// 清理測試方法
        /// </summary>
        [TestCleanup]
        public void TestMethodCleanUp()
        {
            FinishUsingContainer();
        }
        
        [TestMethod]
        public void TestGetALL()
        {
            IUserRepository userRepo = this.Resolve<IUserRepository>();
            
            Assert.IsTrue(userRepo.GetAll().FirstOrDefault(u => u.Id == 1)?.Name == "Jackson");
            Assert.IsTrue(userRepo.GetAll().FirstOrDefault(u => u.Id == 2)?.Name == "Anne");
        }
    }
}