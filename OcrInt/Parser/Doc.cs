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
        public Definition[] Definitions;

        public Doc(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

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

                // Extraction des différentes déclarations de produit
                Definitions = GetDefinitions(Words);

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
                if (text == null)
                    throw new ArgumentNullException("text");

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
                if (words == null)
                    throw new ArgumentNullException("words");

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
                throw new Exception("Error in CompoundTags", ex);
            }
        }

        /// <summary>
        /// Extraction des différentes déclarations de produit
        /// </summary>
        /// <param name="words"></param>
        public static Definition[] GetDefinitions(Word[] words)
        {
            try
            {
                if (words == null)
                    throw new ArgumentNullException("words");

                // Algorithme de découpe des définitions de produit.
                Definition lastDefinition = null;
                var current = new Definition();
                var definitions = new List<Definition>();
                var wordsLength = words.Length;

                // Pour chaque mot
                for (int i = 0; i < wordsLength; i++)
                {
                    var word = words[i];

                    // Si le mot clé avec le plus gros score est un mot composé
                    var wordCount = word.MaxTag.WordCount;
                    if (wordCount > 1)
                    {
                        // On ajoute tous les mots sans faire de traitement
                        i += wordCount - 1;
                        for (int j = 0; j < wordCount; j++)
                            current.Words.Add(word);
                        continue;
                    }

                    // Pour chaque texte entre 2 séparateurs (point, retour à la ligne, tiret...)
                    // ou un nombre
                    if (word.Tag.IsSeparator || !word.Tag.Number.IsEmpty)
                    {
                        // Si la phrase courante est la suite de la phrase précédente
                        if (current.IsFollowing(lastDefinition))
                        {
                            lastDefinition.Add(current);
                            current = new Definition();
                        }
                        else
                        {
                            // Sinon il s’agit d’une nouvelle définition de produit.
                            definitions.Add(current);
                            lastDefinition = current;
                            current = new Definition();
                        }

                        // Pour un nombre
                        if (!word.Tag.Number.IsEmpty)
                        {
                            // Ajoute le nombre
                            current.Words.Add(word);
                        }

                        continue;
                    }

                    // Ajoute le mot
                    current.Words.Add(word);
                }

                // Si la phrase courante est la suite de la phrase précédente
                if (current.IsFollowing(lastDefinition))
                {
                    lastDefinition.Add(current);
                }
                else
                {
                    // Sinon il s’agit d’une nouvelle définition de produit.
                    definitions.Add(current);
                }

                return definitions.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetDefinitions", ex);
            }
        }
    }
}
