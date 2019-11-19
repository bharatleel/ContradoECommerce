using ContradoECommerce.DataAccess.Interface;
using ContradoECommerce.Shared.Entities;
using ContradoECommerce.Shared.Entities.Interface;
using ContradoECommerce.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContradoECommerce.Service
{
    public class ProductService : IProductService
    {
        private readonly ISqlService _sqlService;
        public ProductService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public Task<Product> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _sqlService.ExecuteStoredProcedureAndReturnListObjectAsync<Product>("usp_product_get_all", null);
        }
    }
}
