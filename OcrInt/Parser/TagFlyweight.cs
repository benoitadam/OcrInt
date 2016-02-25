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
        private static Action[] charToActions;
        
        enum Action : byte
        {
            None = 0,
            Space = 1,
            Line = 2,
            Dot = 3,
        }

        static TagFlyweight()
        {
            charToActions = new Action[127];
            charToActions[' '] = Action.Space;
            charToActions['.'] = Action.Dot;
            charToActions['!'] = Action.Dot;
            charToActions['?'] = Action.Dot;
            charToActions['-'] = Action.Dot;
            charToActions['+'] = Action.Dot;
            charToActions['|'] = Action.Dot;
            charToActions[':'] = Action.Dot;
            charToActions['\n'] = Action.Line;
        }

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
                var words = new List<Word>();
                int lineNbr = 1, wordNbr = 1, i = 0;

                // Pour chaque caractére
                var textLength = text.Length;
                int start = 0;
                for (; i < textLength; i++)
                {
                    var chr = text[i];

                    // Nous utilisons un tableau pour les actions, car c’est plus rapide que des «if, else» imbriqués
                    var action = chr > 127 ? Action.None : charToActions[chr];

                    // Si le caractère n'est pas lié à une action de séparation de mot
                    if (action == Action.None)
                        continue;

                    // On ajoute le mot précédent
                    // Ajoute seulement les mots d’une taille supérieure à 0
                    if (i - start > 0)
                    {
                        var key = text.Substring(start, i - start);
                        var word = Word.Create(key, lineNbr, wordNbr, this[key]);
                        words.Add(word);
                        wordNbr++;
                    }

                    // Le mot suivant commence au caractère d’après.
                    start = i + 1;

                    // Caractère de séparation d'une ligne
                    if (action == Action.Line)
                    {
                        // Changement de la ligne
                        wordNbr = 1;
                        lineNbr++;
                        continue;
                    }

                    // Caractère de séparation des produits
                    if (action == Action.Dot)
                    {
                        // Crée un petit mot pour les caractères de séparation
                        var key = chr.ToString();
                        var word = Word.Create(key, lineNbr, wordNbr, this[key]);
                        words.Add(word);
                        wordNbr++;
                    }
                }

                // On ajoute le dernier mot
                if (i - start > 0)
                {
                    var key = text.Substring(start, i - start);
                    var word = Word.Create(key, lineNbr, wordNbr, this[key]);
                    words.Add(word);
                }

                return words.ToArray();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in ExtractWords", ex);
            }
        }
    }
}