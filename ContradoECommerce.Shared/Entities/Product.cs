using ContradoECommerce.Shared.Entities.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContradoECommerce.Shared.Entities
{
    public class Product : IProduct
    {
        [JsonProperty(PropertyName = "productid")]
        public long ProductId { get; set; }

        [JsonProperty(PropertyName = "prodname")]
        public string ProdName { get; set; }

        [JsonProperty(PropertyName = "proddescription")]
        public string ProdDescription { get; set; }

        [JsonProperty(PropertyName = "categoryname")]
        public string CategoryName { get; set; }
    }
}
