using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 用於設計 Database schema 階段時建立 DbContext 之工廠基底類別
    /// </summary>
    /// <typeparam name="TContext"><see cref="DbContext"/>實際型別</typeparam>
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 抽象屬性，連線字串鍵值
        /// </summary>
        protected abstract string ConnectionKey { get; }

        /// <summary>
        /// 建立 DbContext
        /// </summary>
        /// <param name="args">EF Core Migrations 主程式進入點之參數</param>
        /// <returns><see cref="DbContext"/>實際型別</returns>
        public TContext CreateDbContext(string[] args)
        {
            return this.BuildContext();
        }

        /// <summary>
        /// 抽象方法，建立 DbContext 實例之實際邏輯
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder{TContext}"/></param>
        /// <param name="connectionString">連線字串</param>
        /// <returns><see cref="DbContext"/>實際型別</returns>
        protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> optionsBuilder, string connectionString);

        /// <summary>
        /// 在此取得連線字串與<see cref="DbContextOptionsBuilder{TContext}"/>，並帶入抽象方法<see cref="CreateNewInstance"/>
        /// </summary>
        /// <returns><see cref="DbContext"/>實際型別</returns>
        private TContext BuildContext()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                      .AddJsonFile("migrationsetting.json")
                                                                      .AddJsonFile($"migrationsetting.{environmentName}.json", true)
                                                                      .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            string connectionString = configuration.GetConnectionString(this.ConnectionKey);
            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();

            return this.CreateNewInstance(optionsBuilder, connectionString);
        }
    }
}
