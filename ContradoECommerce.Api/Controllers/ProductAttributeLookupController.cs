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
    public class ProductAttributeLookupController : ControllerBase
    {
        private readonly IProductAttributeLookupService _productAttributeLookupService;

        public ProductAttributeLookupController(IProductAttributeLookupService productAttributeLookupService)
        {
            _productAttributeLookupService = productAttributeLookupService;
        }

        [HttpGet]
        [Route("productattributelookups")]
        public async Task<List<ProductAttributeLookup>> GetProductAttributeLookups()
        {
            return await _productAttributeLookupService.GetProductAttributeLookups();
        }

    }
}
