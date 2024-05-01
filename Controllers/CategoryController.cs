
using khanami.Contracts;
using khanami.Data;
using khanami.Entities;
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
    public class CategoryController : Controller
    {
        private readonly DBContext _dbContext;
        public CategoryController(DBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Categorys()
        {
            List<ItemCategory> categoryes = new List<ItemCategory>();
            categoryes = await _dbContext.Category.ToListAsync();
            var response = categoryes.Select(b => new CatResponse(b.Category_id,b.CategoryName));
            return Ok(response);
        }
    }
}
