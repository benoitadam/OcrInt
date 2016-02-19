using System;
using System.Linq;
using System.Collections.Generic;

namespace OcrInt
{
    public class AttributeType
    {
        public ProductType ProductType;
        public string Name;

        public AttributeType(ProductType productType, string name)
        {
            this.ProductType = productType;
            this.Name = name;
        }
    }
}