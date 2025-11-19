using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOS.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.controllers
{
   
    public class Basketcontroller(IServiceManager serviceManager) :APIBaseController
    {
        //Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(Key);
            //if (basket == null) return NotFound();
            return Ok(basket);

        }

        //Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var basket = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        //Delete Basket
        [HttpDelete("{Key}")]
        public async Task<IActionResult> DeleteBasket(string Key)
        {
           var Result = await serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }

    }
}
