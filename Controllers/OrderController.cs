using khanami.Contracts;
using khanami.Data;
using khanami.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace khanami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private DBContext db;

        public OrderController(DBContext db)
        {
            this.db = db;
        }


        [HttpPost]
        //[Authorize(Policy = "RequireStringRole")]
        public async Task<ActionResult<int>> GetOrder([FromBody] EditCartRequest editRequest) //(int id, string name, string des, decimal price)
        {
            var rand = new Random();
            var myRandomNumber = rand.Next(1, 1000000);
            string shopcartsJson = JsonConvert.SerializeObject(editRequest.Shopcarts);

            var NewOrder = new Orders
            {
                Id = myRandomNumber,
                OrderName = editRequest.Username,
                OrderCarts = shopcartsJson,

            };
            await db.Orders.AddAsync(NewOrder);
            await db.SaveChangesAsync();


            return Ok(NewOrder.Id);
        }

         
    }
}
