using AutoMapper;
using Domain.Entities.BasketModules;
using Domain.Entities.Identity_Modules;
using Domain.Entities.OrderModules;
using Domain.Entities.ProductModules;
using Shared.DTOS;
using Shared.DTOS.BasketDto;
using Shared.DTOS.IdentityDtos;
using Shared.DTOS.OrderDtos;
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
                .ForMember(dist => dist.productBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.productType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.pictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
            #endregion
            #region Basket
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>()
     .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
     .ReverseMap()
     .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

            #endregion
            #region Identity
            CreateMap<Address, AddressDto>().ReverseMap();
            #endregion
            #region Order
            CreateMap<ShippingAddressDto, ShippingAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.deliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d=>d.Total,o=>o.MapFrom(s=>s.GetTotal()));

            CreateMap<OrderItem, OrderItemsDto>()
               
                .ForMember(D => D.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(D => D.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(D =>D.Price , o=>o.MapFrom(s=>s.Price))
                .ForMember(D => D.Quantity,o=>o.MapFrom(s=>s.Quantity));


            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            #endregion

        }
    }
}
