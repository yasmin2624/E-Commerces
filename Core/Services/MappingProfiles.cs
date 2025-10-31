using AutoMapper;
using Domain.Entities.BasketModules;
using Domain.Entities.ProductModules;
using Shared.DTOS;
using Shared.DTOS.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            #region Product 

            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
            #endregion
            #region Basket
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            #endregion
        }
    }
}
