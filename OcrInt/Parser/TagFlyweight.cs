using System;
using System.Linq;
using System.Collections.Generic;

namespace OcrInt
{
    /// <summary>
    /// Permets de récupérer les mots clés associés aux mots
    /// </summary>
    public class TagFlyweight : Flyweight<string, Tag>
    {
        public static TagFlyweight Default = new TagFlyweight();

        /// <summary>
        /// Crée le mot clé à la volée
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override Tag Create(string key)
        {
            try
            {
                var tag = new Tag(key);

                // Si le mot clé est une composition de plusieurs mots clés
                var words = key.Split(' ');
                if (words.Length > 1)
                {
                    tag.WordCount = words.Length;

                    // On ajoute pour chacun des mots clés le groupe de mots clés
                    foreach (var word in words)
                    {
                        this[word].CompoundTags[key] = tag;
                    }
                }

                return tag;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Create({key})", ex);
            }
        }
        
        /// <summary>
        /// Récupère les mots dans le texte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Word[] ExtractWords(string text)
        {
            try
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
                        var tagKey = words[j];
                        var tag = this[tagKey];
                        var word = Word.Create(words[j], i, j, tag);
                        results.Add(word);
                    }
                }

                return results.ToArray();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in ExtractWords", ex);
            }
        }
    }
}