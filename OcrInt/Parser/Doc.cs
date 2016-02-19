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
        //private static Regex FORMAT_REGEX = new Regex("([0-9]+) ?[x /] ?([0-9]+)", RegexOptions.Compiled);
        private string Text;
        private Word[] Words;

        public Doc(string text)
        {
        }

        public void Compute()
        {
            // Nettoyage du texte
            Text = CleanText(Text);

            // Récupère les mots dans le texte
            Words = ExtractWords(Text);
        }

        /// <summary>
        /// Nettoyage du texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CleanText(string text)
        {
            text = text.RemoveDiacriticsExt();
            text = text.ToLowerInvariant();
            text = SPECIAL_CHAR.Replace(text, " ");
            text = MULTI_SPACE_REGEX.Replace(text, " ");
            text = MULTI_ENTER_REGEX.Replace(text, "\n");
            //text = FORMAT_REGEX.Replace(text, "$1x$2");
            return text;
        }

        /// <summary>
        /// Récupère les mots dans le texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Word[] ExtractWords(string text)
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

            return results.ToArray();
        }
    }
}
