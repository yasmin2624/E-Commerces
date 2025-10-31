using AutoMapper;
using Domain.Entities.ProductModules;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PictureUrlResolver(IConfiguration Configuration ) : IValueResolver<Product, ProductDto, string>
    {
        //https://localhost:7220/images/products/ItalianChickenMarinade.png
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{Configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
                return Url;
            }
        }
    }
}
