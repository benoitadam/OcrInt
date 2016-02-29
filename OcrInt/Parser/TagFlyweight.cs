using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OcrInt
{
    /// <summary>
    /// Permets de récupérer les mots clés associés aux mots
    /// </summary>
    public class TagFlyweight : Flyweight<string, Tag>
    {
        private static Regex FORMAT_REGEX = new Regex(@"^([0-9\.]*[0-9]+) ?x ?([0-9\.]*[0-9]+) ?(m?)$", RegexOptions.Compiled);
        private static Regex GRAMMAGE_REGEX = new Regex(@"^([0-9\.]*[0-9]+) ?([gm].*)$", RegexOptions.Compiled);
        
        public static TagFlyweight Default = new TagFlyweight();
        public static Action[] Char2Actions;

        public enum Action : byte
        {
            None = 0,
            Space = 1,
            Line = 2,
            Dot = 3,
        }

        static TagFlyweight()
        {
            Char2Actions = new Action[127];
            Char2Actions[' '] = Action.Space;
            Char2Actions['.'] = Action.Dot;
            Char2Actions['!'] = Action.Dot;
            Char2Actions['?'] = Action.Dot;
            Char2Actions['-'] = Action.Dot;
            Char2Actions['+'] = Action.Dot;
            Char2Actions['|'] = Action.Dot;
            Char2Actions[':'] = Action.Dot;
            Char2Actions['('] = Action.Dot;
            Char2Actions[')'] = Action.Dot;
            Char2Actions['<'] = Action.Dot;
            Char2Actions['>'] = Action.Dot;
            Char2Actions['#'] = Action.Dot;
            Char2Actions['$'] = Action.Dot;
            Char2Actions['%'] = Action.Dot;
            Char2Actions['&'] = Action.Dot;
            Char2Actions['@'] = Action.Dot;
            //Char2Actions['\''] = Action.Dot;
            Char2Actions['\n'] = Action.Line;
        }

        /// <summary>
        /// Ajoute un nombre au format texte
        /// un, deux, trois...
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddNumberText(string key, int value)
        {
            this[key].Number = value;
            key = key.Substring(0, 1).ToUpperInvariant() + key.Substring(1);
            this[key].Number = value;
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
        /// Ajout un produit dans le dictionnaire de mot clé
        /// </summary>
        /// <param name="type"></param>
        /// <param name="synonymesByProduct"></param>
        public void AddProduct(ProductType type, string product, string synonymes)
        {
            AddProducts(type, new Dictionary<string, string> { { product, synonymes } });
        }

        /// <summary>
        /// Ajout des produits dans le dictionnaire de mot clé
        /// </summary>
        /// <param name="type"></param>
        /// <param name="synonymesByProduct"></param>
        public void AddProducts(ProductType type, Dictionary<string, string> synonymesByProduct)
        {
            try
            {
                // Pour chaque produit
                foreach (var pair in synonymesByProduct)
                {
                    var product = pair.Key;
                    var synonymes = pair.Value.Split(';');

                    // Pour chaque synonyme
                    foreach (var synonyme in synonymes)
                    {
                        if (String.IsNullOrEmpty(synonyme))
                            continue;

                        // Pour chaque déclinaison du synonyme
                        var declinations = GetDeclinations(synonyme.Simplify());
                        foreach (var declination in declinations)
                        {
                            this[declination].Products[type] = product;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddProducts({type}, ...)", ex);
            }
        }

        /// <summary>
        /// Ajout un attribut dans le dictionnaire de mot clé
        /// </summary>
        /// <param name="type"></param>
        /// <param name="synonymesByProduct"></param>
        public void AddAttribute(ProductType type, string attributeTypeName, string attribute, string synonymes)
        {
            AddAttributes(type, attributeTypeName, new Dictionary<string, string> { { attribute, synonymes } });
        }

        /// <summary>
        /// Ajout des attributs dans le dictionnaire de mot clé
        /// </summary>
        /// <param name="type"></param>
        /// <param name="synonymesByProduct"></param>
        public void AddAttributes(ProductType type, string attributeTypeName, Dictionary<string, string> synonymesByAttribute)
        {
            try
            {
                // Pour chaque attribut
                foreach (var pair in synonymesByAttribute)
                {
                    var attribute = pair.Key;

                    // Gestion des attributs inversés
                    var isInvert = attribute.StartsWith("!");
                    if(isInvert)
                        attribute = attribute.Substring(1);
                    
                    var synonymes = pair.Value.Split(';');

                    // Pour chaque synonyme
                    foreach (var synonyme in synonymes)
                    {
                        if (String.IsNullOrEmpty(synonyme))
                            continue;

                        // Pour chaque déclinaison du synonyme
                        var declinations = GetDeclinations(synonyme.Simplify());
                        foreach (var declination in declinations)
                        {
                            TagValue value = attribute;
                            value.IsInvert = isInvert;

                            this[declination].Attributes[type] = new ProductAttribute()
                            {
                                AttributeTypeName = attributeTypeName,
                                ProductType = type,
                                Value = value,
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddAttributes({type}, {attributeTypeName}, ...)", ex);
            }
        }

        /// <summary>
        /// Récupère toutes les déclinaisons possibles d’un synonyme
        /// - "(de )texte(s)" -> "de texte" + "de textes" + "texte" + "textes"
        /// - "12.3x4.5" -> "12.3x4.5" + "12.3 x 4.5"
        /// </summary>
        /// <param name="s">le synonyme</param>
        /// <returns></returns>
        public IEnumerable<string> GetDeclinations(string s)
        {
            try
            {
                var declinations = new List<string>();

                if (string.IsNullOrEmpty(s))
                    return declinations;

                // (de )texte(s) -> de texte + de textes + texte + textes
                var first = s.IndexOf('(');
                if (first >= 0)
                {
                    var next = s.IndexOf(')');
                    if (next > 0)
                    {
                        var s1 = s.Substring(0, first) + s.Substring(next + 1);
                        var s2 = s.Substring(0, first) + s.Substring(first + 1, next - first - 1) + s.Substring(next + 1);
                        declinations.AddRange(GetDeclinations(s1));
                        declinations.AddRange(GetDeclinations(s2));
                        return declinations;
                    }
                }

                // 12.3x4.5 -> 12.3x4.5 + 12.3 x 4.5
                var formatMatch = FORMAT_REGEX.Match(s);
                if (formatMatch.Success)
                {
                    var x = formatMatch.Groups[1].Value;
                    var y = formatMatch.Groups[2].Value;
                    var unite = formatMatch.Groups[3].Value;

                    if(String.IsNullOrEmpty(unite))
                    {
                        declinations.Add($"{x}x{y}");
                        declinations.Add($"{x} x {y}");
                        return declinations;
                    }
                    else
                    {
                        declinations.Add($"{x}x{y}{unite}");
                        declinations.Add($"{x} x {y}{unite}");
                        declinations.Add($"{x}x{y} {unite}");
                        declinations.Add($"{x} x {y} {unite}");
                        return declinations;
                    }
                }

                var grammageMatch = GRAMMAGE_REGEX.Match(s);
                if (grammageMatch.Success)
                {
                    var grammage = grammageMatch.Groups[1].Value;
                    var grammageUnite = grammageMatch.Groups[2].Value;

                    if (grammageUnite == "g")
                    {
                        //80; 80g; 80gr; 80 g; 80 gr
                        declinations.Add(grammage);
                        declinations.Add($"{grammage}g");
                        declinations.Add($"{grammage} g");
                        declinations.Add($"{grammage}gr");
                        declinations.Add($"{grammage} gr");
                        return declinations;
                    }
                    else if (grammageUnite == "g|m²")
                    {
                        //80g/m²; 80 g/m²; 80gr/m²; 80 gr/m²
                        declinations.Add($"{grammage}g | m2");
                        declinations.Add($"{grammage} g | m2");
                        declinations.Add($"{grammage}gr | m2");
                        declinations.Add($"{grammage} gr | m2");
                        return declinations;
                    }
                    else if (grammageUnite == "mm")
                    {
                        //80mm; 80 mm
                        declinations.Add($"{grammage}mm");
                        declinations.Add($"{grammage} mm");
                        return declinations;
                    }
                }

                declinations.Add(s);

                return declinations;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetDeclinations({s})", ex);
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
                    var action = chr > 127 ? Action.None : Char2Actions[chr];

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