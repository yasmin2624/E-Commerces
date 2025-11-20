using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities.OrderModules;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemsDto, string>
    {

        public string Resolve(OrderItem source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
           
            var Url = $"{configuration.GetSection("URLS")["BaseUrl"]}{source.Product.PictureUrl}";
            return Url;
        }

    }
}
