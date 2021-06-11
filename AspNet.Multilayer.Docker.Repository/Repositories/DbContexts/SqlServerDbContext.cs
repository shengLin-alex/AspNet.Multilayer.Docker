using AspNet.Multilayer.Docker.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 本站台使用之 SqlServer <see cref="DbContext"/>
    /// </summary>
    public class SqlServerDbContext : DbContext
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/></param>
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// <see cref="User"/> 資料集
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// <see cref="OnModelCreating"/>
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("multilayerdemo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
