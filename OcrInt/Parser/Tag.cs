using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class Tag
    {
        public string Name { get; set; }

        public AttributeCollection Attributes { get; set; }

        public SynonymCollection Synonyms { get; set; }

        public ProductCollection Products { get; set; }

        public TagCollection Tags { get; set; }

        public double? Number { get; set; }

        public bool IsInvert { get; set; }

        public Tag(string name)
        {
            Name = name;
            Attributes = new AttributeCollection();
            Synonyms = new SynonymCollection();
            Products = new ProductCollection();
            Tags = new TagCollection();
        }
    }
}







//public SortedSet<TagGroup> Groups { get; set; }

//public Dictionary<string, Tag> Links { get; set; }
//public void AddLink(Tag tag)
//{
//    Links[tag.Name] = tag;
//}