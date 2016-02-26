using System;
using System.Linq;
using System.Collections.Generic;
namespace OcrInt
{
    public class ProductAttributeCollection : Dictionary<int, ProductAttribute>
    {
        public TagValue this[ProductType productType, string attributeTypeName]
        {
            set
            {
                ProductAttribute attr;

                if (!this.TryGetValue(productType.Id, out attr))
                    this[productType.Id] = attr = new ProductAttribute() {
                        ProductType = productType,
                        AttributeTypeName = attributeTypeName,
                    };

                attr.Value = value;
            }
        }
    }
}