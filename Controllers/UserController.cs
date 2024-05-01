using khanami.Contracts;
using khanami.Data;
using khanami.Entities;
using khanami.Model;
 using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static khanami.Program;

namespace khanami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly
            DBContext _dbContext;
 private readonly IConfiguration _configuration;
        public UserController(IPasswordHasher passwordHasher,DBContext dbcontext,IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbcontext;
            _configuration = configuration;
         }

        private string Cratetoken(User user)
        {
            List<Claim> Claims = new List<Claim> { 
            new Claim(ClaimTypes.Name,user.userName),
            new Claim(ClaimTypes.Role,user.Profession),

            };

            var jwt = new JwtSecurityToken(
                
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims:Claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256)

                ); 


            return  new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("register")]
        public ActionResult<User> Register(User req)
        {
            string passwordHash = _passwordHasher.Generate(req.PasswordHashe);

            User US = new User { Id = req.Id , userName =req.userName,PasswordHashe=passwordHash, Profession=req.Profession};
            
            if(US!= null)
            {
                _dbContext.Users.Add(US);
                _dbContext.SaveChanges();
                return Ok(US);

            }



            return NotFound("go around");
        }

        [HttpPost("login")]
        public ActionResult<User> login(User req)
        {
           
                var US = _dbContext.Users.FirstOrDefault(u => u.userName == req.userName);

                if (US != null)
                {
                    bool password = _passwordHasher.Verify(req.PasswordHashe, US.PasswordHashe);

                    if (password)
                    {
                    var token = Cratetoken(US);
                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest("Invalid password");
                    }
                }
                else
                {
                    return NotFound("User not found");
                }
            
        }







    }
}
