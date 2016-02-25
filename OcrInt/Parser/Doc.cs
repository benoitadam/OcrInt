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
        public ExtractedProduct[] ExtractedProducts;

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
                ExtractedProducts = ProductExtraction(Words);

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
        public static ExtractedProduct[] ProductExtraction(Word[] words)
        {
            try
            {
                if (words == null)
                    throw new ArgumentNullException("words");

                var extractedProducts = new List<ExtractedProduct>();
                var extractedProduct = new ExtractedProduct();

                // Algorithme de découpe des définitions de produit.

                // Pour chaque texte entre 2 séparateurs (point, retour à la ligne, tiret...)
                // On détecte la présence d’un nombre potentielle.
                // Si le score du nombre potentiel est supérieur aux attributs possibles qui lui sont liés.
                // Il s’agit très certainement d’une nouvelle définition de produit. 

                var wordsLength = words.Length;
                for (int i = 0; i < wordsLength; i++)
                {
                    //var word = words[i];

                    //extractedProduct.Add(word);

                    //if(word.Tag.Number.HasValue)
                    //{
                    //    var max = word.Tag.Max
                    //    foreach (var )
                    //    word.Tag.Number.Score
                    //}
                }







                    // Pour chaque mot
                    //var wordsLength = words.Length;
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

                return extractedProducts.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ProductExtraction", ex);
            }
        }
    }
}
