using AspNet.Multilayer.Docker.Repository.Models;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 使用者資料儲存庫
    /// </summary>
    public class UserRepository : GenericRepository<User, PostgresDbContext>, IUserRepository
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbContext">本站台使用之 PostgreSql DbContext</param>
        public UserRepository(PostgresDbContext dbContext) : base(dbContext)
        {
        }
    }
}
