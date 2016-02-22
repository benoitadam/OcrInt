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
        public string Text;
        public Word[] Words;

        public Doc(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Analyse la chaine de caractère et calcule le résultat.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Doc Compute(TagFlyweight tags = null)
        {
            try
            {
                if (tags == null)
                    tags = TagFlyweight.Default;

                // Nettoyage du texte
                Text = CleanText(Text);

                // Récupère les mots dans le texte
                Words = tags.ExtractWords(Text);

                // Récupère les mots dans le texte
                CompoundTags(Words);

                return this;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Compute({tags})", ex);
            }
        }

        /// <summary>
        /// Nettoyage du texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CleanText(string text)
        {
            try
            {
                return text.Simplify();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CleanText", ex);
            }
        }
        
        /// <summary>
        /// Recherche les groupes de mots
        /// </summary>
        /// <param name="words"></param>
        public static void CompoundTags(Word[] words)
        {
            try
            {
                // Pour chaque mot
                var wordsLength = words.Length;
                for (int i = 0; i < wordsLength; i++)
                {
                    var word = words[i];
                    var tag = words[i].Tag;

                    // Pour chacun des mots composés possibles
                    foreach (var compoundTag in tag.CompoundTags.Values)
                    {
                        int wordCount = compoundTag.WordCount;
                        int lastWordIndex = i + wordCount;

                        // Fin de la liste de mots
                        if (lastWordIndex > wordsLength)
                            continue;

                        // Teste chacun des mots suivants
                        int j = i + 1;
                        for (; j < lastWordIndex; j++)
                        {
                            var word2 = words[j];
                            var tag2 = word2.Tag;
                            if (!tag2.CompoundTags.ContainsKey(compoundTag.Name))
                                break;
                        }

                        // Si tous les mots contiennent le mot composé
                        if (j == lastWordIndex)
                        {
                            // Ajoute le mot composé
                            word.CompoundTags[compoundTag.Name] = compoundTag;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ComputeGroups", ex);
            }
        }
    }
}
