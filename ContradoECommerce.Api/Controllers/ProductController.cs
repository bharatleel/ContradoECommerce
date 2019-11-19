using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContradoECommerce.Shared.Entities;
using ContradoECommerce.Shared.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContradoECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getproducts")]
        public async Task<List<Product>> GetProducts()
        {
            return await _productService.GetProducts();
        }
    }
}