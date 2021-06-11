using AspNet.Multilayer.Docker.Repository.Models;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 訂單儲存庫
    /// </summary>
    public class OrderRepository : GenericRepository<Order, SqlServerDbContext>, IOrderRepository
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dbContext">db context</param>
        public OrderRepository(SqlServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
