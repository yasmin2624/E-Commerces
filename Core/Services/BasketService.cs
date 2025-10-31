using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModules;
using Domain.Exceptions;
using Service.Abstractions;
using Shared.DTOS.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
            {
               return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Failed to create or update basket.");
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
         => await basketRepository.DeleteBasketAsync(Key);

        public async Task<BasketDto?> GetBasketAsync(string basketId)
        {
           var basket= await basketRepository.GetBasketAsync(basketId);
            if(basket is not null)
            {
                return mapper.Map<CustomerBasket,BasketDto>(basket);
            }
            else
            {throw new BasketNotFoundException(basketId);

            }
                var mappedBasket = mapper.Map<BasketDto>(basket);
            return mappedBasket;
        }
    }
}
