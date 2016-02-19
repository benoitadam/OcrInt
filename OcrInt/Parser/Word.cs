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
        public string Value;
        public TagCollection Tags;
        public int LineNbr;
        public int WordNbr;
        
        private Word() { }

        /// <summary>
        /// Méthode de fabrique d’un mot
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lineNbr"></param>
        /// <param name="wordNbr"></param>
        /// <returns></returns>
        public static Word Create(string value, int lineNbr, int wordNbr)
        {
            var tag = TagFlyweight.Default[value];
            
            return new Word()
            {
                Value = value,
                LineNbr = lineNbr,
                WordNbr = wordNbr,
                Tags = new TagCollection() { { value, tag } },
            };
        }
    }
}
