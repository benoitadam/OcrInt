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

        public ProductAttributeCollection Attributes { get; set; }

        public SynonymCollection Synonyms { get; set; }

        public ProductCollection Products { get; set; }

        public TagCollection CompoundTags { get; set; }

        public double? Number { get; set; }

        public bool IsInvert { get; set; }

        /// <summary>
        /// Nombre de mots contenus dans ce mot clé
        /// "grand" => 1
        /// "grand format" => 2
        /// </summary>
        public int WordCount { get; set; }

        public Tag(string name)
        {
            Name = name;
            Attributes = new ProductAttributeCollection();
            Synonyms = new SynonymCollection();
            Products = new ProductCollection();
            CompoundTags = new TagCollection();
            WordCount = 1;
        }
    }
}