using AspNet.Multilayer.Docker.Repository;
using AspNet.Multilayer.Docker.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Multilayer.Docker.Web.Controllers
{
    public class ApiController : Controller
    {
        /// <summary>
        /// User Repo
        /// </summary>
        private readonly IUserRepositoy UserRepository;

        /// <summary>
        /// Order Repo
        /// </summary>
        private readonly IOrderRepository OrderRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userRepository">User repo</param>
        /// <param name="orderRepository">Order repo</param>
        public ApiController(
            IUserRepositoy userRepository,
            IOrderRepository orderRepository)
        {
            this.UserRepository = userRepository;
            this.OrderRepository = orderRepository;
        }
        
        /// <summary>
        /// Add new user data
        /// </summary>
        /// <param name="user">User</param>
        /// <returns> json response </returns>
        [HttpPost]
        [Route("api/test/user")]
        public IActionResult AddUser([FromBody] User user)
        {
            User newUser = this.UserRepository.Add(user);
            
            return this.Json(newUser);
        }

        /// <summary>
        /// Add new order data
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns> json response </returns>
        [HttpPost]
        [Route("api/test/order")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            Order newOrder = this.OrderRepository.Add(order);

            return this.Json(newOrder);
        }

        /// <summary>
        /// Get all user data
        /// </summary>
        /// <returns> json response </returns>
        [Route("api/test/users")]
        public IActionResult GetUsers()
        {
            return this.Json(this.UserRepository.GetAll());
        }

        /// <summary>
        /// Get all order data
        /// </summary>
        /// <returns> json response </returns>
        [Route("api/test/orders")]
        public IActionResult GetOrders()
        {
            return this.Json(this.OrderRepository.GetAll());
        }
    }
}