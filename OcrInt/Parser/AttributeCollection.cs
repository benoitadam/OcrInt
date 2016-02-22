using System;
using System.Linq;
using System.Collections.Generic;
namespace OcrInt
{
    public class ProductAttributeCollection : Dictionary<int, ProductAttributeCollection.AttributeCollection>
    {
        public TagValue this[ProductType productType, string attributeTypeName]
        {
            get
            {
                return this[productType.Id][attributeTypeName];
            }
            set
            {
                AttributeCollection coll;
                if(!this.TryGetValue(productType.Id, out coll))
                    this[productType.Id] = coll = new AttributeCollection();
                coll[attributeTypeName] = value;
            }
        }

        public class AttributeCollection : Dictionary<string, TagValue>
        {
        }
    }
}