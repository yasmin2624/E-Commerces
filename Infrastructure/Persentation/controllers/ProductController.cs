using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.controllers
{
    [ApiController]
    [Route("api/[controller]")] //BaseUrl/api/Product
    public class ProductController(IServiceManager serviceManager ): ControllerBase
    {
        #region GetAllProducts
        [HttpGet]
        public async Task <ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
        {
            var Products = await serviceManager.ProductService.GetAllProductsAsync();   
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
