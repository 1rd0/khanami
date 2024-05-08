using khanami.Contracts;
using khanami.Data;
using khanami.Entities;
using khanami.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this for Include method
using System;
using System.Linq;
using System.Threading.Tasks;
 

using Newtonsoft.Json;
using System.Collections.Generic;
namespace khanami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Shopingcarts : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Shopingcarts(DBContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{name}")]
public async Task<ActionResult> ShopCartByName([FromRoute] string name)
{
    List<Carts> ShopCarts = await _dbContext.Carts.ToListAsync();
    var item = ShopCarts.FirstOrDefault(b => b.UserName == name);

    if (item != null)
    {
        var response = new ShopCartResponse(item.UserName, item.ItemsCarts);
        return Ok(response.items);
    }
    else
    {
        // Return an appropriate response when the item is not found
        return NotFound("Cart not found for the provided user name.");
    }
}


        [HttpPost("{name}")]
        public async Task<ActionResult> CreateShoppingCart([FromRoute] string name)
        {
            try
            {
                var randomId = GenerateRandomId();
                var newCart = new Carts { Id = randomId, UserName = name, ItemsCarts = "[]" };

                _dbContext.Carts.Add(newCart);
                await _dbContext.SaveChangesAsync();

                return Ok(newCart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the shopping cart: {ex.Message}");
            }
        }

        private int GenerateRandomId()
        {
            // Generating a random integer ID
            Random rand = new Random();
            return rand.Next(10000, 99999); // Change range according to your requirements
        }


        [HttpPost("editcartshopbyname")]
    public async Task<ActionResult> EditCartShopByName([FromBody] EditCartRequest editRequest)
    {
        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.UserName == editRequest.Username);

        if (cart != null)
        {
            try
            {
                    string shopcartsJson = JsonConvert.SerializeObject(editRequest.Shopcarts);

                    // Присваиваем строку JSON свойству ItemsCarts
                    cart.ItemsCarts = shopcartsJson;

                    // Обычно здесь следует выполнить сохранение изменений в базе данных
                    await _dbContext.SaveChangesAsync();

                // Возвращаем успешный ответ с обновленной корзиной
                return Ok(cart);
            }
            catch (Exception ex)
            {
                // Возвращаем ответ о возникшей ошибке
                return StatusCode(500, $"Произошла ошибка при обновлении корзины: {ex.Message}");
            }
        }
        else
        {
            // Возвращаем ответ о том, что корзина не найдена
            return NotFound("Корзина не найдена для указанного имени пользователя.");
        }
    }


}
}
