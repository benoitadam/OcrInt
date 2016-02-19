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

        public override Tag Create(string key)
        {
            var tag = new Tag(key);

            // Si le mot clé est une composition de plusieurs mots clés
            var words = key.Split(' ');
            if(words.Length > 1)
            {
                // On ajoute pour chacun des mots clés le groupe de mots clés
                foreach (var word in words)
                {
                    this[word].Tags[key] = tag;
                }
            }

            return tag;
        }
    }
}