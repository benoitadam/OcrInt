using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class Tag
    {
        #region MAX

        private TagValue max;

        /// <summary>
        /// Récupère la plus grosse valeur
        /// </summary>
        public TagValue Max
        {
            get
            {
                if (max.IsEmpty)
                {
                    foreach (var prod in Products.Values)
                        if (prod.Score > max.Score)
                            max = prod;

                    foreach (var attr in Attributes.Values)
                        if (attr.Value.Score > max.Score)
                            max = attr.Value;

                    if (Number.Score > max.Score)
                        max = Number;
                }
                return max;
            }
        }

        /// <summary>
        /// Récupère la plus gros score
        /// </summary>
        public int MaxScore { get { return Max.Score; } }

        #endregion

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
        /// Nombre de mots contenus dans ce mot clé
        /// "grand" => 1
        /// "grand format" => 2
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// Si le mot clé est un séparateur de définition
        /// </summary>
        public bool IsSeparator { get; set; }

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
        
        /// <summary>
        /// Affiche le texte pour le débogage
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}