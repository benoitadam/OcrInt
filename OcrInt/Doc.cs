using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OcrInt
{
    public class Doc
    {
        private static Regex SPECIAL_CHAR = new Regex("[^0-9a-z\n ]+", RegexOptions.Compiled);
        private static Regex MULTI_SPACE_REGEX = new Regex(" +", RegexOptions.Compiled);
        private static Regex MULTI_ENTER_REGEX = new Regex(" ?\n ?", RegexOptions.Compiled);
        private static Regex FORMAT_REGEX = new Regex("([0-9]+) ?[x /] ?([0-9]+)", RegexOptions.Compiled);
        private string Text;
        private Word[] Words;

        public Doc(string text)
        {
        }

        /// <summary>
        /// Nettoyage du texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private void CleanText()
        {
            Text = Text.RemoveDiacriticsExt();
            Text = Text.ToLowerInvariant();
            Text = SPECIAL_CHAR.Replace(Text, " ");
            Text = MULTI_SPACE_REGEX.Replace(Text, " ");
            Text = MULTI_ENTER_REGEX.Replace(Text, "\n");
            Text = FORMAT_REGEX.Replace(Text, "$1x$2");
        }

        /// <summary>
        /// Récupère les mots dans le texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public void ExtractWords(string text)
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
                    var word = Word.Create(words[i], i, j);
                    results.Add(word);
                }
            }

            Words = results.ToArray();
        }
    }
}
