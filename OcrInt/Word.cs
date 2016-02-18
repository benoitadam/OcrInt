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
        public int Tag;
        public int LineNbr;
        public int WordNbr;
        public bool CanGroup;

        #region Helper


        #endregion

        #region MethodFactory

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
            var tag = Tag.Get(value);

            return new Word()
            {
                Value = value,
                LineNbr = lineNbr,
                WordNbr = wordNbr,
            };
        }

        #endregion

        #region Word Type

        public class Number : Word
        {

        }

        public class GroupWord : Word
        {

        }

        #endregion
    }
}
