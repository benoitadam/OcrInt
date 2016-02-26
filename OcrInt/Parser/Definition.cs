using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    /// <summary>
    /// Une définition correspond au texte situé entre 2 mots clés de délimitation.
    /// </summary>
    public class Definition
    {
        public List<Word> Words;

        public Definition()
        {
            Words = new List<Word>();
        }

        /// <summary>
        /// Récupère le mot qui correspond le plus à un nombre
        /// </summary>
        /// <returns></returns>
        public Word GetNumber()
        {
            Word result = null;

            // Recherche le 1er nombre avec le meilleur score
            foreach (var word in Words)
                if (!word.Tag.Number.IsEmpty && (result == null || result.Tag.Number.Score < word.Tag.Number.Score))
                    result = word;

            return result;
        }

        /// <summary>
        /// Récupère le score pour un type de produit
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetScore(ProductType type)
        {
            // Calcule le score lors du 1er appel
            var score = 0;

            // Pour chaque mot de la définition
            foreach (var word in Words)
            {
                // Récupère le mot clé du mot
                var tag = word.Tag;

                // Si le mot clé possède un attribut lié au type de produit.
                ProductAttribute attr;
                if (tag.Attributes.TryGetValue(type, out attr))
                {
                    score += attr.Value.Score;
                    continue;
                }

                // Si le mot clé est un nombre
                TagValue number = word.Tag.Number;
                if (!number.IsEmpty)
                {
                    score += number.Score;
                    continue;
                }

                // Si le mot clé est un produit
                TagValue product;
                if (word.Tag.Products.TryGetValue(type, out product))
                {
                    score += product.Score;
                    continue;
                }
            }

            return score;
        }
        
        /// <summary>
        /// Affiche le texte pour le débogage
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach(var word in Words)
                sb.Append(word).Append(' ');

            if (sb.Length > 0)
                sb.Length--;

            return sb.ToString();
        }

        /// <summary>
        /// Si la phrase courante est la suite de la phrase précédente
        /// </summary>
        /// <param name="lastDefinition"></param>
        /// <returns></returns>
        public bool IsFollowing(Definition lastDefinition)
        {
            if(lastDefinition == null)
                return false;

            var products = GetProducts();

            // Si la définition ne contient pas de mot clé de produit
            // Et forcément la première phrase doit contenir un produit
            if (products.Count == 0)
                return true;

            //var lastAttributesScore = lastDefinition.GetProducts();
            //var attributes = GetAttributeScores();
            
            return false;
        }

        /// <summary>
        /// Récupère la liste des produits potentiels
        /// </summary>
        /// <returns></returns>
        private Dictionary<ProductType, TagValue> GetProducts()
        {
            TagValue tagValue;
            var productTypes = new Dictionary<ProductType, TagValue>();

            // Pour chaque produit dans chaque mot clé
            foreach (var word in Words)
                foreach (var product in word.Tag.Products)
                {
                    // Si la valeur du produit potentielle est inférieure à celui déjà trouvé
                    // pour le même type de produit
                    if (productTypes.TryGetValue(product.Key, out tagValue) &&
                        product.Value.Score < tagValue.Score)
                        continue;

                    // Ajoute le produit
                    productTypes[product.Key] = product.Value;
                }

            return productTypes;
        }

        /// <summary>
        /// Récupère la liste des produits potentiels
        /// </summary>
        /// <returns></returns>
        private Dictionary<ProductType, int> GetAttributeScores()
        {
            int score;
            var productTypes = new Dictionary<ProductType, int>();

            // Pour chaque produit dans chaque mot clé
            foreach (var word in Words)
                foreach (var attr in word.Tag.Attributes.Values)
                {
                    // Incrémente le score pour le type de produit
                    if (productTypes.TryGetValue(attr.ProductType, out score))
                    {
                        productTypes[attr.ProductType] = score + attr.Value.Score;
                        continue;
                    }

                    // Ajoute le score pour le type de produit
                    productTypes[attr.ProductType] = attr.Value.Score;
                }

            return productTypes;
        }

        /// <summary>
        /// Ajoute la phrase à la suite
        /// </summary>
        /// <param name="current"></param>
        public void Add(Definition current)
        {
            foreach (var word in current.Words)
                Words.Add(word);
        }
    }
}
