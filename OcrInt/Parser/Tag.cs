using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class Tag
    {
        private TagValue? max = null;

        public string Name { get; set; }

        public ProductAttributeCollection Attributes { get; set; }
        
        public Dictionary<ProductType, TagValue> Products { get; set; }

        /// <summary>
        /// Les mots clés composés de plusieurs mots
        /// </summary>
        public Dictionary<string, Tag> CompoundTags { get; set; }

        /// <summary>
        /// Si le mot clé peut être un nombre
        /// </summary>
        public TagValue Number { get; set; }

        /// <summary>
        /// Le mot clé avec le plus gros score
        /// </summary>
        public TagValue Max
        {
            get
            {
                if (max.HasValue)
                {

                }
            }
        }

        /// <summary>
        /// Nombre de mots contenus dans ce mot clé
        /// "grand" => 1
        /// "grand format" => 2
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// Si le tag peut être un mot inversé (ex: "sans")
        /// </summary>
        public bool IsInvert;

        public Tag(string name)
        {
            Name = name;
            Attributes = new ProductAttributeCollection();
            Products = new Dictionary<ProductType, TagValue>();
            CompoundTags = new Dictionary<string, Tag>();
            WordCount = 1;
        }
    }
}