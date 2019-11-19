using ContradoECommerce.Shared.Entities.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContradoECommerce.Shared.Entities
{
    public class ProductAttributeLookup : IProductAttributeLookup
    {
        [JsonProperty(PropertyName = "attributeid")]
        public int AttributeId { get; set; }

        [JsonProperty(PropertyName = "prodcatid")]
        public int ProdCatId { get; set; }

        [JsonProperty(PropertyName = "attributename")]
        public string AttributeName { get; set; }
    }
}
