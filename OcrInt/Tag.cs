using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class TagFlyweight
    {
        static readonly Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public static Tag GetFlyweight(string key)
        {
            Tag tag;
            return tags.TryGetValue(key, out tag) ? tag : null;
        }

        public static Tag Add(string key)
        {
            Tag tag;
            if (!tags.TryGetValue(key, out tag))
            {
                tag = new Tag(key);
                tags.Add(key, tag);
            }
            return tag;
        }
    }

    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
            AttributeName = String.Empty;
            Links = new Dictionary<string, Tag>();
        }

        public string Name { get; set; }

        public string AttributeName { get; set; }

        public string Product { get; set; }

        public string Attribute { get; set; }

        public Tag SynonymeOf { get; set; }

        public bool IsNumber { get { return Number != 0; } }

        public int Number { get; set; }

        public Dictionary<string, Tag> Links { get; set; }
        public bool IsInvert { get; internal set; }

        public void AddLink(Tag tag)
        {
            Links[tag.Name] = tag;
        }
    }
}
