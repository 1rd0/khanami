using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace khanami.Contracts
{
     
        public record ShopCartResponse
    (

          string UserName,
          string items
    );

     
}
