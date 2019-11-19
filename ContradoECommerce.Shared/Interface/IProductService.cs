using ContradoECommerce.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContradoECommerce.Shared.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}
