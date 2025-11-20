using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity_Modules;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper ,IBasketRepository basketRepository , UserManager<ApplicationUser> userManager ,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyProductService = new Lazy <IProductService>(()=>new ProductService(unitOfWork,mapper));
        private readonly Lazy<IBasketService> LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager , configuration , mapper));
        private readonly Lazy<IOrderService> LazyOrderService = new Lazy<IOrderService>(() => new OrderService(basketRepository, mapper, unitOfWork));
        public IProductService ProductService => LazyProductService.Value;
        public IBasketService BasketService => LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => LazyAuthenticationService.Value;

        public IOrderService OrderService => LazyOrderService.Value;
    }
}
