using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt.Tests
{
    public static class Data
    {
        public static TagFlyweight GetTagFlyweight(ProductTypeFlyweight productTypes = null)
        {
            var tags = new TagFlyweight();

            #region ADD NUMBER TAGS

            var numberError = "115";
            var numberErrorReplace = "lIS";

            for (int i = 0; i < 100; i++)
            {
                var str = i.ToString();
                tags[str].Number = new TagValue(i, TagValue.SIMPLE_NUMBER_SCORE);

                for (int j = 0; j < str.Length; j++)
                {
                    var chr = str[j];
                    if (numberError.IndexOf(chr) >= 0)
                    {
                        var strReplaceBuilder = new StringBuilder(str);
                        strReplaceBuilder[j] = numberErrorReplace[numberError.IndexOf(chr)];
                        var strReplace = strReplaceBuilder.ToString();
                        tags[strReplace].Number = new TagValue(i, TagValue.SIMPLE_NUMBER_SCORE);
                    }
                }
            }

            #endregion

            #region ADD SEPARATOR TAGS

            tags["-"].IsSeparator = true;
            tags["."].IsSeparator = true;
            tags["\n"].IsSeparator = true;
            tags["|"].IsSeparator = true;
            tags["!"].IsSeparator = true;
            tags["?"].IsSeparator = true;
            tags[":"].IsSeparator = true;

            #endregion

            #region PRODUIT 1

            var p1 = productTypes[1];
            tags.AddProducts(p1, new Dictionary<string, string>{
                { "Cahier", "Cahier(s)" },
                { "Carnet", "Carnet(s)" },
                { "Repertoire", "Repertoire(s)" },
                { "Bloc", "Bloc" },
                { "Koverbook", "Koverbook" },
            });
            tags.AddAttributes(p1, "nature", new Dictionary<string, string>{
                { "TP", "TP" },
                { "texte", "(de) texte(s)" },
                { "brouillon", "(de) brouillon(s)" },
                { "poésie", "(de) poésie(s)" },
                { "maternelle", "(de) maternelle(s)" },
                { "musique", "(de) musique(s)" },
                { "bureau", "(de) bureau(x)" },
                { "cours", "(de) cours" },
                { "dessin", "(de) dessin(s)" },
                { "microperforée", "microperforé(e)(s)" },
            });
            tags.AddAttributes(p1, "format", new Dictionary<string, string>{
                { "petit", "petit(s)" },
                { "moyen", "moyen(s)" },
                { "grand", "grand(s)" },
                { "maxi", "maxi(s)" },
                { "17x22", "17x22" },
                { "A4", "A4" },
                { "21x29.7", "21x29.7" },
                { "24x32", "24x32" },
                { "14.8x21", "14.8x21" },
                { "A4+", "A4+" },
                { "210x315", "210x315" },
            });
            tags.AddAttributes(p1, "nombre de pages", new Dictionary<string, string>{
                { "32", "32 (pages)" },
                { "48", "48 (pages)" },
                { "64", "64 (pages)" },
                { "96", "96 (pages)" },
                { "100", "100 (pages)" },
                { "140", "140 (pages)" },
                { "180", "180 (pages)" },
                { "192", "192 (pages)" },
                { "200", "200 (pages)" },
                { "240", "240 (pages)" },
            });
            tags.AddAttributes(p1, "reliure", new Dictionary<string, string>{
                { "piqure", "piqure(s)" },
                { "piqué", "piqué(e)(s)" },
                { "agrafé", "agrafé(e)(s)" },
                { "spirale", "spirale(s)" },
                { "reliure intégrale", "reliure(s) intégrale(s)" },
                { "brochure", "brochure(s)" },
                { "encollé", "encollé(e)(s)" },
            });
            tags.AddAttributes(p1, "réglure", new Dictionary<string, string>{
                { "séyès", "séyè(s)" },
                { "5x5", "5x5" },
                { "grands carreaux", "grands carreaux" },
                { "petits carreaux", "petits carreaux" },
                { "ligné", "ligné(s)" },
                { "uni", "uni(s)" },
                { "travers", "travers" },
                { "double ligne", "double ligne" },
                { "portée", "portée" },
            });
            tags.AddAttributes(p1, "couverture", new Dictionary<string, string>{
                { "polypropylène", "polypropylène" },
                { "carte", "carte" },
                { "carte pelliculée", "carte pelliculée" },
                { "rigide", "rigide" },
                { "souple", "souple" },
            });
            tags.AddAttributes(p1, "couleur", new Dictionary<string, string>{
                { "bleu", "bleu" },
                { "rouge", "rouge" },
                { "vert", "vert" },
                { "noir", "noir" },
                { "orange", "orange" },
                { "jaune", "jaune" },
                { "rose", "rose" },
                { "violet", "violet" },
            });
            tags.AddAttributes(p1, "grammage (g)", new Dictionary<string, string>{
                { "90g", "90;90g;90gr;90 g;90 gr" },
                { "80g", "80;80g;80gr;80 g;80 gr" },
                { "60g", "60;60g;60gr;60 g;60 gr" },
                { "55g", "55;55g;55gr;55 g;55 gr" },
                { "70g", "70;70g;70gr;70 g;70 gr" },
            });
            tags.AddAttributes(p1, "grammage (g/m²)", new Dictionary<string, string>{
                { "90g/m²", "90g/m²;90 g/m²;90gr/m²;90 gr/m²" },
                { "80g/m²", "80g/m²;80 g/m²;80gr/m²;80 gr/m²" },
                { "60g/m²", "60g/m²;60 g/m²;60gr/m²;60 gr/m²" },
                { "55g/m²", "55g/m²;55 g/m²;55gr/m²;55 gr/m²" },
                { "70g/m²", "70g/m²;70 g/m²;70gr/m²;70 gr/m²" },
            });

            #endregion

            tags.AddAttributes(p1, "reliure", new Dictionary<string, string>{
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
            });

            tags["grand"].Attributes[1, "format"] = "grand";
            tags["grand format"].Attributes[1, "format"] = "grand";
            tags["24x32"].Attributes[1, "format"] = "grand";
            tags["24 x 32"].Attributes[1, "format"] = "24x32";
            tags["grands carreaux"].Attributes[1, "réglure"] = "grands carreaux";
            tags["96"].Attributes[1, "pages"] = "96";
            tags["96 pages"].Attributes[1, "pages"] = "96";

            // 2
            tags["pochette"].Products[3] = "Pochette";
            tags["pochette papier"].Products[3] = "Pochette Papier";
            tags["pochette de papier"].Products[3] = "Pochette Papier";
            tags["pochettes de papier"].Products[3] = "Pochette Papier";
            tags["calque"].Attributes[3, "type de papier"] = "Calque";
            tags["50 feuilles"].Attributes[3, "nb de feuilles"] = "50 feuilles";

            // 3
            tags["compa"].Products[4] = "Compas";
            tags["compas"].Products[4] = "Compas";
            tags["qualite"].Attributes[4, "qualité"] = "bonne";
            tags["de qualite"].Attributes[4, "qualité"] = "bonne";
            tags["20 cm"].Attributes[4, "ouverture"] = "20 cm";
            tags["ouverture 20 cm"].Attributes[4, "ouverture"] = "20 cm";

            return tags;
        }
    }
}
