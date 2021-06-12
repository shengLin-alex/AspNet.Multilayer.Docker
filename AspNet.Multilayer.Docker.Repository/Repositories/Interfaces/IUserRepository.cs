using AspNet.Multilayer.Docker.Repository.Models;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 使用者資料儲存庫介面
    /// </summary>
    public interface IUserRepository : IGenericRepository<User, PostgresDbContext>
    {
    }
}
