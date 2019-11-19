using System;
using System.Collections.Generic;
using System.Text;

namespace ContradoECommerce.Shared.Entities.Interface
{
    public interface IProduct
    {
        long ProductId { get; set; }

        string ProdName { get; set; }

        string ProdDescription { get; set; }

        public string CategoryName { get; set; }
    }
}
