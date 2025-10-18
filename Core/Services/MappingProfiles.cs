using AutoMapper;
using Domain.Entities;
using Shared.DTOS;
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
        }
    }
}
