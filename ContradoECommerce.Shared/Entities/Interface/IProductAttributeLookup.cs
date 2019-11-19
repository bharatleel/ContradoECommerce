using System;
using System.Collections.Generic;
using System.Text;

namespace ContradoECommerce.Shared.Entities.Interface
{
    public interface IProductAttributeLookup
    {
        int AttributeId { get; set; } // int, not null

        int ProdCatId { get; set; } // int, not null

        string AttributeName { get; set; } // varchar(500), not null
    }
}
