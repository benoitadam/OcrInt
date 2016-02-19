using System;
using System.Linq;
using System.Collections.Generic;
namespace OcrInt
{
    /// <summary>
    /// le poids-mouche est un patron de conception :
    /// https://fr.wikipedia.org/wiki/Poids-mouche_(patron_de_conception)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class Flyweight<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> tags = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Récupère un mot clé
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Get(TKey key)
        {
            TValue value;
            return tags.TryGetValue(key, out value) ? value : default(TValue);
        }

        /// <summary>
        /// Récupère ou ajoute un mot clé
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetOrCreate(TKey key)
        {
            TValue tag;
            if (!tags.TryGetValue(key, out tag))
            {
                tag = Create(key);
                tags.Add(key, tag);
            }
            return tag;
        }

        /// <summary>
        /// Récupère ou ajoute un mot clé
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return GetOrCreate(key); }
        }

        /// <summary>
        /// Crée une nouvelle valeur à partir de la clé
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract TValue Create(TKey key);
    }

}



//public Tag AddSynonym(string key, string synonym)
//{
//    Tag tag = GetOrCreate(key);
//    tags[synonym] = tag;
//    return tag;
//}

//public Tag AddAttribut(string attribut, string attributType, int productId)
//{
//    Tag tag = GetOrCreate(attribut);
//    tag.Attributes[productId] = attributType;
//    return tag;
//}

//public Tag AddProduct(ProductType productType, string productName)
//{
//    Tag tag = GetOrCreate(productName);
//    tag.ProductId = productId;
//    return tag;
//}