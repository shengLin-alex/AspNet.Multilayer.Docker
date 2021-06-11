using Microsoft.EntityFrameworkCore;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 本站台使用之 SqlServer <see cref="DbContext"/> 的設計階段 <see cref="DbContext"/> 工廠
    /// </summary>
    public class SqlServerDbContextFactory : DesignTimeDbContextFactoryBase<SqlServerDbContext>
    {
        /// <summary>
        /// 連線字串鍵值
        /// </summary>
        protected override string ConnectionKey => "DockerSqlServer";

        /// <summary>
        /// 抽象方法，建立 DbContext 實例之實際邏輯
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder{TContext}"/></param>
        /// <param name="connectionString">連線字串</param>
        /// <returns><see cref="DbContext"/>實際型別</returns>
        protected override SqlServerDbContext CreateNewInstance(DbContextOptionsBuilder<SqlServerDbContext> optionsBuilder, string connectionString)
        {
            // 設定使用 SqlServer 為 DbProvider
            optionsBuilder.UseSqlServer(connectionString);

            return new SqlServerDbContext(optionsBuilder.Options);
        }
    }
}
