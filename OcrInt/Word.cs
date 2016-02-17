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
            return new Word()
            {
                Value = value,
                LineNbr = lineNbr,
                WordNbr = wordNbr,
            };
        }

        /// <summary>
        /// Récupère les mots dans le texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Word[] GetWords(string text)
        {
            var results = new List<Word>();

            // Pour chaque ligne
            string[] lines = text.Split('\n');
            int linesNbr = lines.Length;
            for (int i = 0; i < linesNbr; i++)
            {
                // Pour chaque mot dans la ligne
                string[] words = lines[i].Split(' ');
                int lineWordsNbr = words.Length;
                for (int j = 0; j < lineWordsNbr; j++)
                {
                    // Ajoute le mot à la liste des mots
                    var word = Create(words[i], i, j);
                    results.Add(word);
                }
            }

            return results.ToArray();
        }
    }
}
