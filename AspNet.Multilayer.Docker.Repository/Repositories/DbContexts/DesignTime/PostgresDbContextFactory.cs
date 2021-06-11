using Microsoft.EntityFrameworkCore;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 本站台使用之 PostgreSql <see cref="DbContext"/> 的設計階段 <see cref="DbContext"/> 工廠
    /// </summary>
    public class PostgresDbContextFactory : DesignTimeDbContextFactoryBase<PostgresDbContext>
    {
        /// <summary>
        /// 連線字串鍵值
        /// </summary>
        protected override string ConnectionKey => "DockerPostgres";

        /// <summary>
        /// 抽象方法，建立 DbContext 實例之實際邏輯
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder{TContext}"/></param>
        /// <param name="connectionString">連線字串</param>
        /// <returns><see cref="DbContext"/>實際型別</returns>
        protected override PostgresDbContext CreateNewInstance(DbContextOptionsBuilder<PostgresDbContext> optionsBuilder, string connectionString)
        {
            // 設定使用 Npgsql 為 DbProvider
            optionsBuilder.UseNpgsql(connectionString);

            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}
