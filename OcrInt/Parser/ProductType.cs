using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class ProductType
    {
        public int Id { get; set; }
        
        public ProductType(int id)
        {
            Id = id;
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