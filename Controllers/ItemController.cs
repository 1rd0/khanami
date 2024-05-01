using khanami.Contracts;
using khanami.Data;
using khanami.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace khanami.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
     
     public class ItemController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public ItemController(DBContext context)
        {
            _dbContext = context;
        }


        [HttpGet]
        
        public async Task<IActionResult> Items() {

            List<Item> items = new List<Item>();
            items = await _dbContext.Items.ToListAsync();
            var response = items.Select(b => new ItemResponse(b.Id, b.Name, b.Description, b.Price,b.Category, b.Imgurl));
            return Ok(response);
        }

        [HttpGet("{id}")]
        
        public  async Task<ActionResult> ItemsById([FromRoute] int id)
        {
            var item = await _dbContext.Items.FindAsync(id);
            if(item == null)
            {
                return Ok("not exists");
            }
            else
            {
                return Ok(item);
            }

            
        } 


        [HttpPost]
        [Authorize(Policy = "RequireStringRole")]
        public async Task<ActionResult<int>> GetItem([FromBody] ItemRequest request) //(int id, string name, string des, decimal price)
        {
            var rand = new Random();
            var myRandomNumber = rand.Next(1, 1000000);


            var NewItem = new Item { Id = myRandomNumber,

            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            Imgurl = request.Imgurl,
            };
          await _dbContext.Items.AddAsync(NewItem);
            await _dbContext.SaveChangesAsync();
            
           
            return Ok(NewItem.Id);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "RequireStringRole")]
        public async Task<ActionResult<int>> DeleteItem(int id)
        {

            return Ok(await _dbContext.Items.Where(b => b.Id ==id).ExecuteDeleteAsync());
        }

    }
}
