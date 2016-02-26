using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    /// <summary>
    /// Class qui représente les méthodes de manipulation des mots d’un document
    /// </summary>
    public class Word
    {
        private Tag max;
        public string Value;
        public int LineNbr;
        public int WordNbr;
        public Tag Tag;

        /// <summary>
        /// Groupe de mots clés pour les mots composés
        /// </summary>
        public Dictionary<string, Tag> CompoundTags;

        /// <summary>
        /// Récupère le score maximale
        /// </summary>
        public int MaxScore
        {
            get
            {
                return MaxTag.MaxScore;
            }
        }

        /// <summary>
        /// Récupère le mot clé avec le plus gros score
        /// </summary>
        public Tag MaxTag
        {
            get
            {
                if(max == null)
                {
                    max = Tag;

                    // Les mots composés sont prioritaires
                    foreach (var compoundTag in CompoundTags.Values)
                        if(max.MaxScore <= compoundTag.MaxScore)
                            max = compoundTag;
                }
                return max;
            }
        }

        private Word()
        {
            CompoundTags = new Dictionary<string, Tag>();
        }

        /// <summary>
        /// Méthode de fabrique d’un mot
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lineNbr"></param>
        /// <param name="wordNbr"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static Word Create(string value, int lineNbr, int wordNbr, Tag tag)
        {
            return new Word()
            {
                Value = value,
                LineNbr = lineNbr,
                WordNbr = wordNbr,
                Tag = tag,
            };
        }
        
        /// <summary>
        /// Affiche le texte pour le débogage
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
