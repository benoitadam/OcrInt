using System;
using System.Linq;
using System.Collections.Generic;
namespace OcrInt
{
    public class ProductAttributeCollection : Dictionary<int, Dictionary<string, TagValue>>
    {
        public TagValue this[ProductType productType, string attributeTypeName]
        {
            get
            {
                return this[productType.Id][attributeTypeName];
            }
            set
            {
                Dictionary<string, TagValue> coll;
                if(!this.TryGetValue(productType.Id, out coll))
                    this[productType.Id] = coll = new Dictionary<string, TagValue>();
                coll[attributeTypeName] = value;
            }
        }
    }
}