using khanami.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using khanami.Entities;
 
using Microsoft.Extensions.Options;
using khanami.Model;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection.Emit;
using khanami.Interfaces;
 
namespace khanami
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
 
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
          
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();



            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireStringRole", policy =>
                {
                    policy.RequireRole("string2");
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
                
            });
           
           
          

            builder.Services.AddDbContext<DBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConStr"));
            });

             var app = builder.Build();
            app.UseCors("AllowAnyOrigin");
            app.UseSwagger();
            app.UseRouting();
            app.UseSwaggerUI();
            app.MapControllers();
            app.UseHttpsRedirection();
          
            app.UseAuthentication();
            app.UseAuthorization();
          
            app.UseStaticFiles();
            
         


            //  app.MapGet("/", () => "Hello World!");

            app.Run();
        }
        public class AuthOptions
        {
            public const string ISSUER = "MyAuthServer"; // издатель токена
            public const string AUDIENCE = "MyAuthClient"; // потребитель токена
            const string KEY = "secretkeysecsdaslmdk213asdsdfadd21313fdsaasdfaw213123fdsa1234dasdas!123";   // ключ для шифрации
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }

    }
}
 

