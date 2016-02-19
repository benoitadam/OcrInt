using System;
using System.Linq;
using System.Collections.Generic;

namespace OcrInt
{
    /// <summary>
    /// Permets de récupérer les mots clés associés aux mots
    /// </summary>
    public class ProductTypeFlyweight : Flyweight<int, ProductType>
    {
        public static ProductTypeFlyweight Default = new ProductTypeFlyweight();

        public override ProductType Create(int key)
        {
            return new ProductType(key);
        }
    }
}