using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persentation.Attributes;
using Service.Abstractions;
using Shared;
using Shared.DTOS;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.controllers
{
 
    public class ProductController(IServiceManager serviceManager ) : APIBaseController
    {
        #region GetAllProducts
        [Authorize]
        [HttpGet]
        [Cashe]
        
        public async Task <ActionResult<PaginatedResult<ProductDto>>> GetAllProduct([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await serviceManager.ProductService.GetAllProductsAsync(queryParams);   
            return Ok(Products);

        }

        #endregion

        #region GetProductById
        [HttpGet("{Id}")]
        public async Task <ActionResult<ProductDto>> GetProduct(int Id)
        {
            var Product = await serviceManager.ProductService.GetProductByIdAsync(Id);
            return Ok(Product);

        }


        #endregion

        #region GetAllTypes
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        #endregion

        #region GetAllBrands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        } 
        #endregion
    }
}
