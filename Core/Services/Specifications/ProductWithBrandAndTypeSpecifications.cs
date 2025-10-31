using Domain.Entities.ProductModules;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        // Get All Products With Brands and Types
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) &&
            (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId) && 
            (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.Contains(queryParams.SearchValue.ToLower())))  
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            #region Sorting
            switch (queryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;

            }


            #endregion
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        // Get Product By Id With Brand and Type
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
