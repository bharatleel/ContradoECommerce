using ContradoECommerce.DataAccess.Interface;
using ContradoECommerce.Shared.Entities;
using ContradoECommerce.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContradoECommerce.Service
{
    public class ProductAttributeLookupService : IProductAttributeLookupService
    {
        //private readonly IProductAttributeLookupDataContext _productAttributeLookupDataContext;
        //public ProductAttributeLookupService(IProductAttributeLookupDataContext productAttributeLookupDataContext)
        //{
        //    _productAttributeLookupDataContext = productAttributeLookupDataContext;
        //}

        private readonly ISqlService _sqlService;
        public ProductAttributeLookupService(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public Task<ProductAttributeLookup> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductAttributeLookup>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductAttributeLookup>> GetProductAttributeLookups()
        {
            //return await _productAttributeLookupDataContext.GetProductAttributeLookups();
            return await _sqlService.ExecuteStoredProcedureAndReturnListObjectAsync<ProductAttributeLookup>("usp_productattributelookup_get_all", null);
        }
    }
}
