using AutoMapper;
using Domain.Contracts;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyProductService = new Lazy <IProductService>(()=>new ProductService(unitOfWork,mapper));
        public IProductService ProductService => LazyProductService.Value;
    }
}
