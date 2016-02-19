using System;
using System.Linq;
using System.Collections.Generic;
namespace OcrInt
{
    public class AttributeCollection : Dictionary<AttributeType, string>
    {
        public string this[ProductType productType, string attributeTypeName]
        {
            get { return this[new AttributeType(productType, attributeTypeName)]; }
            set { this[new AttributeType(productType, attributeTypeName)] = value; }
        }
    }
}