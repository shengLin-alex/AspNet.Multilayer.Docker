using AspNet.Multilayer.Docker.Repository.Models;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 訂單儲存庫介面
    /// </summary>
    public interface IOrderRepository : IGenericRepository<Order, SqlServerDbContext>
    {
    }
}
