using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModules;
using Domain.Exceptions;
using Service.Abstractions;
using Service.Specifications;
using Shared;
using Shared.DTOS;
using Shared.Enums;


namespace Service
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService

    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
           var Repo = unitOfWork.GetRepository<ProductBrand,int>();
           var Brands= await  Repo.GetAllAsync();
            return mapper.Map< IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Specifications = new Specifications.ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(Specifications);
            var data = mapper.Map< IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Products.Count();
            return new PaginatedResult<ProductDto>(ProductCount, queryParams.PageIndex , 0, data);


        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types=await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return mapper.Map< IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
           
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product= await  unitOfWork.GetRepository<Product,int>().GetByIdAsync(Specifications);
           if(Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return mapper.Map<Product, ProductDto>(Product);
        }
    }
}
