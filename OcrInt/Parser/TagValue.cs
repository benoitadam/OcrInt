using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    /// <summary>
    /// Valeur d'un mot clé
    /// </summary>
    public struct TagValue
    {
        public const int PRODUCT_SCORE = 1500;
        public const int ATTRIBUTE_SCORE = 1000;
        public const int SIMPLE_NUMBER_SCORE = 1200;
        public const int NUMBER_SCORE = 500;
        public const int NUMBER_ERROR_SCORE = 100;

        /// <summary>
        /// Un mot clé est vide ou indéfini
        /// </summary>
        public static TagValue Empty = new TagValue(null, 0);

        /// <summary>
        /// Si le mot clé est vide ou indéfini
        /// </summary>
        public bool IsEmpty { get { return Value == null; } }

        /// <summary>
        /// Si le mot clé est un nombre
        /// </summary>
        public bool IsNumber { get { return Number != double.MinValue; } }

        /// <summary>
        /// Le nombre lié au mot clé si celui-ci est un nombre
        /// </summary>
        public double Number;

        /// <summary>
        /// Le mot lié au mot clé
        /// </summary>
        public string Value;

        /// <summary>
        /// Le score de correspondance du mot clé
        /// </summary>
        public int Score;

        /// <summary>
        /// Si le tag est inversé (ex "sans reliure")
        /// </summary>
        public bool IsInvert;

        /// <summary>
        /// Crée un mot clé pour un nombre
        /// </summary>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <param name="isInvert"></param>
        public TagValue(double value, int score, bool isInvert = false)
            : this(value.ToString(CultureInfo.InvariantCulture), score, isInvert)
        {
            Number = value;
        }

        /// <summary>
        /// Crée un mot clé pour un mot simple ou composé
        /// </summary>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <param name="isInvert"></param>
        public TagValue(string value, int score, bool isInvert = false)
        {
            Number = double.MinValue;
            Value = string.IsNullOrEmpty(value) ? null : value;
            Score = score;
            IsInvert = isInvert;
        }

        /// <summary>
        /// Récupère une clé de HASH pour le mot
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Si 2 mots clés sont égaux
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is TagValue) && Equals((TagValue)obj);
        }

        /// <summary>
        /// Si 2 mots clés sont égaux
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(TagValue obj)
        {
            return Value.Equals(obj.Value);
        }

        /// <summary>
        /// Implicit TagValue to string conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(TagValue value)
        {
            return value.Value;
        }

        /// <summary>
        /// Explicit string to TagValue conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TagValue(string value)
        {
            return new TagValue(value, ATTRIBUTE_SCORE);
        }

        /// <summary>
        /// Implicit TagValue to string conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator double(TagValue value)
        {
            return value.Number;
        }

        /// <summary>
        /// Explicit string to TagValue conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TagValue(double value)
        {
            return new TagValue(value, NUMBER_SCORE);
        }

        /// <summary>
        /// Affiche le texte pour le débogage
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
