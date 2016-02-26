using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public struct ProductType
    {
        public int Id;
        
        public ProductType(int id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is ProductType) && Equals((ProductType)obj);
        }

        public bool Equals(ProductType obj)
        {
            return Id.Equals(obj.Id);
        }
        
        /// <summary>
        /// Implicit TagValue to string conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(ProductType value)
        {
            return value.Id;
        }

        /// <summary>
        /// Explicit string to TagValue conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ProductType(int value)
        {
            return new ProductType(value);
        }

        /// <summary>
        /// Affiche le texte pour le débogage
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}









//public Product()
//{
//    Attributes = new List<Tag>();
//    InvertAttributes = new List<Tag>();
//    Number = 1;
//}

//public string Name { get; set; }

//public double? Number { get; set; }

//public List<Tag> Attributes { get; set; }

//public List<Tag> InvertAttributes { get; set; }

//public void AddAttribute(Tag tag, bool isInvert)
//{
//    if (isInvert)
//        InvertAttributes.Add(tag);
//    else
//        Attributes.Add(tag);
//}

//public override string ToString()
//{
//    var sb = new StringBuilder();
//    return Append(sb.Append(" > ")).ToString();
//}

//public StringBuilder Append(StringBuilder sb)
//{
//    sb.Append(Number).Append(" ").Append(Name);

//    if (Attributes.Count != 0)
//        sb.Append(" : ");

//    foreach (var attr in Attributes)
//        sb.Append(attr.Name).Append(", ");

//    foreach (var attr in InvertAttributes)
//        sb.Append("non ").Append(attr.Name).Append(", ");

//    if (Attributes.Count > 0 || InvertAttributes.Count > 0)
//        sb.Length -= 2;

//    return sb;
//}