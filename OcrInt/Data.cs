using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public static class Data
    {
        public static TagFlyweight GetTagFlyweight(ProductTypeFlyweight productTypes = null)
        {
            if (productTypes == null)
                productTypes = new ProductTypeFlyweight();

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
                        tags[strReplace].Number = new TagValue(i, TagValue.NUMBER_ERROR_SCORE);
                    }
                }
            }

            tags.AddNumberText("un", 1);
            tags.AddNumberText("un", 2);
            tags.AddNumberText("deux", 3);
            tags.AddNumberText("trois", 4);
            tags.AddNumberText("quatre", 5);
            tags.AddNumberText("cinq", 6);
            tags.AddNumberText("six", 7);
            tags.AddNumberText("sept", 8);
            tags.AddNumberText("huit", 9);
            tags.AddNumberText("neuf", 10);
            tags.AddNumberText("dix", 11);
            tags.AddNumberText("onze", 12);
            tags.AddNumberText("douze", 13);
            tags.AddNumberText("treize", 14);
            tags.AddNumberText("quatorze", 15);
            tags.AddNumberText("quinze", 16);
            tags.AddNumberText("seize", 17);

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

            #region PRODUIT 1 - Cahier

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
                { "!spirale", "sans spirale" },
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
                { "90g", "90g" },
                { "80g", "80g" },
                { "60g", "60g" },
                { "55g", "55g" },
                { "70g", "70g" },
            });
            tags.AddAttributes(p1, "grammage (g/m²)", new Dictionary<string, string>{
                { "90g/m²", "90g/m²" },
                { "80g/m²", "80g/m²" },
                { "60g/m²", "60g/m²" },
                { "55g/m²", "55g/m²" },
                { "70g/m²", "70g/m²" },
            });

            #endregion

            #region PRODUIT 2 - Feuilles

            var p2 = productTypes[2];
            tags.AddProducts(p2, new Dictionary<string, string>{
                { "Feuilles", "Feuilles" },
                { "Copies", "Copies" },
                { "Fiches", "Fiches" },
            });
            tags.AddAttributes(p2, "nature", new Dictionary<string, string>{
                { "Simples", "Simples" },
                { "Doubles", "Doubles" },
                { "Bristol", "Bristol" },
            });
            tags.AddAttributes(p2, "nature 2", new Dictionary<string, string>{
                { "blanche", "blanche" },
                { "couleurs assorties", "couleurs assorties;couleur assortie" },
            });
            tags.AddAttributes(p2, "nature 3", new Dictionary<string, string>{
                { "perforées", "perforé(e)(s)" },
                { "!perforées", "non perforé(e)(s)" },
            });
            tags.AddAttributes(p2, "format", new Dictionary<string, string>{
                { "10.5x14.8", "10.5x14.8" },
                { "14.8x21", "14.8x21" },
                { "17x22", "17x22" },
                { "A4", "A4" },
                { "24x32", "24x32" },
            });
            tags.AddAttributes(p2, "réglure", new Dictionary<string, string>{
                { "séyès", "séyè(s)" },
                { "5x5", "5x5" },
                { "grands carreaux", "grands carreaux" },
                { "petits carreaux", "petits carreaux" },
                { "ligné", "ligné(s)" },
                { "uni", "uni(s)" },
                { "travers", "travers" },
            });
            tags.AddAttributes(p2, "grammage (g)", new Dictionary<string, string>{
                { "70g", "70g" },
                { "80g", "80g" },
                { "90g", "90g" },
                { "205g", "205g" },
            });
            tags.AddAttributes(p2, "grammage (g/m²)", new Dictionary<string, string>{
                { "70g/m²", "70g/m²" },
                { "80g/m²", "80g/m²" },
                { "90g/m²", "90g/m²" },
                { "205g/m²", "205g/m²" },
            });

            #endregion

            #region PRODUIT 3 - Pochette

            var p3 = productTypes[3];
            tags.AddProducts(p3, new Dictionary<string, string>{
                { "Pochette", "Pochette" },
                { "Bloc", "Bloc" },
                { "Pochette Papier", "Pochette Papier" },
                { "Bloc croquis", "Bloc croquis" },
            });
            tags.AddAttributes(p3, "reliure", new Dictionary<string, string>{
                { "Dessin", "Dessin(s)" },
                { "Calque", "Calque(s)" },
                { "Millémétré", "Millémétré(e)(s)" },
                { "Dessin à grain", "(Dessin) (à) grain" },
                { "Dessin C à grain", "(Dessin) C (à) grain" },
                { "Dessin couleur pastel", "(Dessin) (couleur) pastel" },
                { "Dessin couleur vive", "(Dessin) (couleur) vive" },
                { "Technique", "Technique(s)" },
                { "Papier bristol", "Papier(s) bristol(s)" },
                { "Buvard", "Buvard(s)" },
                { "Mi-teinte", "Mi-teint(e)(s)" },
            });
            tags.AddAttributes(p3, "format", new Dictionary<string, string>{
                { "24x32", "24x32" },
                { "A4", "A4" },
                { "21x29.7", "21x29.7" },
                { "A3", "A3" },
                { "raisin", "raisin" },
                { "16x21", "16x21" },
            });
            tags.AddAttributes(p3, "nb de feuilles", new Dictionary<string, string>{
                { "8 feuilles", "8 (feuilles)" },
                { "12 feuilles", "12 (feuilles)" },
                { "24 feuilles", "24 (feuilles)" },
                { "50 feuilles", "50 (feuilles)" },
                { "100 feuilles", "100 (feuilles)" },
            });
            tags.AddAttributes(p3, "grammage (g)", new Dictionary<string, string>{
                { "90g", "90g" },
                { "125g", "125g" },
                { "160g", "160g" },
                { "180g", "180g" },
                { "200g", "200g" },
                { "224g", "224g" },
            });
            tags.AddAttributes(p3, "grammage (g/m²)", new Dictionary<string, string>{
                { "90g/m²", "90g/m²" },
                { "125g/m²", "125g/m²" },
                { "160g/m²", "160g/m²" },
                { "180g/m²", "180g/m²" },
                { "200g/m²", "200g/m²" },
                { "224g/m²", "224g/m²" },
            });

            #endregion

            #region PRODUIT 4 - Classeur

            var p4 = productTypes[4];
            tags.AddProducts(p4, new Dictionary<string, string>{
                { "Classeur", "Classeur(s)" },
                { "Cahier-classeur", "Cahier-classeur;Cahier classeur" },
                { "Classeur à Levier", "Classeur(s) à levier" },
            });
            tags.AddAttributes(p4, "Nature", new Dictionary<string, string>{
                { "Souple", "Souple(s)" },
                { "Rigide", "Rigide(s)" },
            });
            tags.AddAttributes(p4, "Matière", new Dictionary<string, string>{
                { "Carton", "Carton(s)" },
                { "Rembordé", "Rembordé(e)(s)" },
                { "Polypropylène", "Polypropylène(s)" },
                { "Plastique", "Plastique(s)" },
                { "Carte", "Carte(s)" },
            });
            tags.AddAttributes(p4, "Dos", new Dictionary<string, string>{
                { "petit", "petit" },
                { "grand", "grand" },
                { "21mm", "21mm" },
                { "25mm", "25mm" },
                { "30mm", "30mm" },
                { "35mm", "35mm" },
                { "40mm", "40mm" },
                { "50mm", "50mm" },
                { "60mm", "60mm" },
                { "70mm", "70mm" },
                { "80mm", "80mm" },
            });
            tags.AddAttributes(p4, "Anneaux", new Dictionary<string, string>{
                { "2", "2" },
                { "4", "4" },
            });
            tags.AddAttributes(p4, "Mécanique", new Dictionary<string, string>{
                { "19mm", "19mm" },
                { "20mm", "20mm" },
                { "25mm", "25mm" },
                { "30mm", "30mm" },
                { "35mm", "35mm" },
                { "40mm", "40mm" },
                { "50mm", "50mm" },
                { "70mm", "70mm" },
                { "75mm", "75mm" },
                { "80mm", "80mm" },
            });
            tags.AddAttributes(p4, "Format extérieur", new Dictionary<string, string>{
                { "240x320", "240x320" },
                { "A4", "A4" },
            });
            tags.AddAttributes(p4, "Format contenu", new Dictionary<string, string>{
                { "A4", "A4" },
                { "17x22", "17x22" },
                { "105x148", "105x148" },
                { "148x210", "148x210" },
                { "pour bristol", "(pour) bristol" },
            });

            #endregion

            #region PRODUIT 5 - Chemise

            var p5 = productTypes[5];
            tags.AddProducts(p5, new Dictionary<string, string>{
                { "Chemise", "Chemise(s)" },
                { "Chemise dossier", "Chemise(s) dossier" },
                { "Sous-chemise", "Sous-chemise(s);Sous chemise(s)" },
                { "Pochette", "Pochette(s)" },
                { "Boîte", "Boîte(s)" },
                { "Carton à dessin", "Carton (à) dessin" },
            });
            tags.AddAttributes(p5, "nature 1", new Dictionary<string, string>{
                { "rabats", "(avec) rabat(s)" },
                { "!rabats", "sans rabat(s)" },
                { "de classement", "(de) classement" },
                { "d'archive", "(d')archive" },
            });
            tags.AddAttributes(p5, "nature 2", new Dictionary<string, string>{
                { "élastique", "(avec) (un) élastique" },
                { "!élastique", "sans (un) élastique" },
            });
            tags.AddAttributes(p5, "Matière", new Dictionary<string, string>{
                { "Carte", "Carte(s)" },
                { "Carton", "(en) carton" },
                { "Cartonnée", "Cartonné(e)(s)" },
                { "plastique", "plastique(s)" },
                { "polypropylène", "polypropylène" },
            });
            tags.AddAttributes(p5, "Dos", new Dictionary<string, string>{
                { "Petit", "petit" },
                { "Grand", "grand" },
                { "25mm", "25mm" },
                { "40mm", "40mm" },
                { "60mm", "60mm" },
                { "80mm", "80mm" },
                { "100mm", "100mm" },
            });
            tags.AddAttributes(p5, "Format contenu", new Dictionary<string, string>{
                { "17x22", "17x22" },
                { "A4", "A4" },
                { "24x32", "24x32" },
                { "pour fiche bristol", "(pour) (fiche) bristol" },
                { "raisin", "raisin" },
            });

            #endregion

            #region PRODUIT 6 - Trieur

            var p6 = productTypes[6];
            tags.AddProducts(p6, new Dictionary<string, string>{
                { "trieur", "trieur(s)" },
                { "trieur familial", "trieur(s) familial" },
            });
            tags.AddAttributes(p6, "Matière", new Dictionary<string, string>{
                { "Carte", "Carte" },
                { "Carton", "Carton" },
                { "cartonnée", "cartonné(e)(s)" },
                { "plastique", "plastique" },
                { "polypropylène", "polypropylène" },
            });
            tags.AddAttributes(p6, "Nb de positions", new Dictionary<string, string>{
                { "6 positions", "6 (positions)" },
                { "7 positions", "7 (positions)" },
                { "8 positions", "8 (positions)" },
                { "9 positions", "9 (positions)" },
                { "10 positions", "10 (positions)" },
                { "12 positions", "12 (positions)" },
                { "14 positions", "14 (positions)" },
            });
            tags.AddAttributes(p6, "Matière", new Dictionary<string, string>{
                { "à rivet", "(à) rivet" },
                { "dos extensibles", "(dos) extensibles" },
            });
            tags.AddAttributes(p6, "Format extérieur", new Dictionary<string, string>{
                { "240x320", "240x320" },
                { "A4", "A4" },
            });

            #endregion

            #region PRODUIT 7 - Pochettes

            var p7 = productTypes[7];
            tags.AddProducts(p7, new Dictionary<string, string>{
                { "Pochettes perforées", "Pochette(s) perforée(s)" },
                { "Pochettes coins", "Pochette(s) coin(s)" },
            });
            tags.AddAttributes(p7, "Matière", new Dictionary<string, string>{
                { "plastique", "plastique(s)" },
            });
            tags.AddAttributes(p7, "paquet de", new Dictionary<string, string>{
                { "20", "(paquet de) 20" },
                { "50", "(paquet de) 50" },
                { "100", "(paquet de) 100" },
                { "150", "(paquet de) 150" },
                { "200", "(paquet de) 200" },
            });
            tags.AddAttributes(p7, "format contenu", new Dictionary<string, string>{
                { "17x22", "17x22" },
                { "A4", "A4" },
                { "24x32", "24x32" },
            });

            #endregion

            #region PRODUIT 8 - Intercalaires

            var p8 = productTypes[8];
            tags.AddProducts(p8, new Dictionary<string, string>{
                { "Intercalaires", "Intercalaire(s)" },
            });
            tags.AddAttributes(p8, "Matière", new Dictionary<string, string>{
                { "Carte", "carte" },
                { "Carton", "carton" },
                { "Cartonnée", "cartonné(e)(s)" },
                { "Plastique", "plastique" },
                { "Polypropylène", "polypropylène" },
            });
            tags.AddAttributes(p8, "Nb de positions", new Dictionary<string, string>{
                { "6", "6 (positions)" },
                { "12", "12 (positions)" },
            });
            tags.AddAttributes(p8, "Format contenu", new Dictionary<string, string>{
                { "A4", "A4" },
                { "A4+", "A4+" },
                { "24x32", "24x32" },
                { "17x22", "17x22" },
                { "maxi", "maxi" },
                { "pour fiche bristol", "(pour) (fiche) bristol" },
            });
            tags.AddAttributes(p8, "Touches", new Dictionary<string, string>{
                { "imprimées", "imprimé(e)(s)" },
                { "neutres", "neutre(s)" },
            });

            #endregion

            #region PRODUIT 9 - Protège-documents

            var p9 = productTypes[9];
            tags.AddProducts(p9, new Dictionary<string, string>{
                { "Protège-documents", "Protège-document(s)" },
                { "Porte vues", "porte vue(s)" },
                { "Lutin", "lutin" },
            });
            tags.AddAttributes(p9, "Reliure", new Dictionary<string, string>{
                { "soudée", "soudé(e)(s)" },
                { "à anneaux", "(à) anneau(x)" },
            });
            tags.AddAttributes(p9, "Couverture", new Dictionary<string, string>{
                { "opaque", "opaque" },
                { "translucide", "translucide" },
                { "personnalisable", "personnalisable" },
                { "avec porte étiquette", "(avec) (porte) étiquette" },
            });
            tags.AddAttributes(p9, "Nombre", new Dictionary<string, string>{
                { "20", "20" },
                { "40", "40" },
                { "60", "60" },
                { "80", "80" },
                { "100", "100" },
                { "120", "120" },
                { "140", "140" },
                { "160", "160" },
                { "180", "180" },
                { "200", "200" },
                { "20 vues", "20 vues" },
                { "40 vues", "40 vues" },
                { "60 vues", "60 vues" },
                { "80 vues", "80 vues" },
                { "100 vues", "100 vues" },
                { "120 vues", "120 vues" },
                { "140 vues", "140 vues" },
                { "160 vues", "160 vues" },
                { "180 vues", "180 vues" },
                { "200 vues", "200 vues" },
                { "10 pochettes", "10 pochettes" },
                { "20 pochettes", "20 pochettes" },
                { "30 pochettes", "30 pochettes" },
                { "40 pochettes", "40 pochettes" },
                { "50 pochettes", "50 pochettes" },
                { "60 pochettes", "60 pochettes" },
                { "70 pochettes", "70 pochettes" },
                { "80 pochettes", "80 pochettes" },
                { "90 pochettes", "90 pochettes" },
                { "100 pochettes", "100 pochettes" },
            });
            tags.AddAttributes(p9, "Format", new Dictionary<string, string>{
                { "A4", "A4" },
                { "A4+", "A4+" },
                { "24x32", "24x32" },
                { "17x22", "17x22" },
            });

            #endregion

            #region PRODUIT 10 - Rouleau

            var p10 = productTypes[10];
            tags.AddProducts(p10, new Dictionary<string, string>{
                { "Rouleau", "rouleau" },
            });
            tags.AddAttributes(p10, "Nature", new Dictionary<string, string>{
                { "couvre livres", "couvre livre(s)" },
                { "papier kraft", "papier kraft" },
            });
            tags.AddAttributes(p10, "Nature 2", new Dictionary<string, string>{
                { "plastique", "plastique" },
                { "adhésif", "adhésif" },
                { "adhésif repositionnable", "adhésif repositionnable" },
            });
            tags.AddAttributes(p10, "Nature 3", new Dictionary<string, string>{
                { "crystal", "crystal" },
                { "lisse", "lisse" },
                { "grainé", "grainé" },
                { "incolore", "incolore" },
            });
            tags.AddAttributes(p10, "Format", new Dictionary<string, string>{
                { "0.4x4m", "0.4x4m" },
                { "0.4x5m", "0.4x5m" },
                { "0.7x2m", "0.7x2m" },
            });

            #endregion

            #region PRODUIT 11 - Protège-cahier

            var p11 = productTypes[11];
            tags.AddProducts(p11, new Dictionary<string, string>{
                { "Protège-cahier", "protege - cahier;protege-cahier;protege cahier" },
            });
            tags.AddAttributes(p11, "Nature", new Dictionary<string, string>{
                { "translucide", "translucide" },
                { "opaque", "opaque" },
            });
            tags.AddAttributes(p11, "Nature 2", new Dictionary<string, string>{
                { "rabats", "(avec) rabats" },
                { "!rabats", "sans rabats" },
            });
            tags.AddAttributes(p11, "Matière", new Dictionary<string, string>{
                { "plastique", "plastique" },
                { "PVC", "PVC" },
                { "polypropylène", "polypropylène" },
                { "cristal", "cristal" },
            });
            tags.AddAttributes(p11, "Format", new Dictionary<string, string>{
                { "17x22", "17x22" },
                { "A4", "A4" },
                { "24x32", "24x32" },
            });
            tags.AddAttributes(p11, "Couleur", new Dictionary<string, string>{
                { "rouge", "rouge" },
                { "bleu", "bleu" },
                { "vert", "vert" },
                { "jaune", "jaune" },
                { "incolore", "incolore" },
                { "noir", "noir" },
                { "violet", "violet" },
                { "rose", "rose" },
                { "orange", "orange" },
            });

            #endregion

            #region PRODUIT 12 - Enveloppe

            var p12 = productTypes[12];
            tags.AddProducts(p12, new Dictionary<string, string>{
                { "enveloppe", "enveloppe" },
                { "pochette administrative", "pochette administrative" },
            });
            tags.AddAttributes(p12, "", new Dictionary<string, string>{
                { "blanche", "blanche" },
                { "Kraft", "Kraft" },
                { "à bulle", "(a) bulle" },
                { "kraft à soufflet", "kraft (a) soufflet" },
            });

            #endregion

            #region AUTRES



            #endregion

            //tags["grand"].Attributes[1, "format"] = "grand";
            //tags["grand format"].Attributes[1, "format"] = "grand";
            //tags["24x32"].Attributes[1, "format"] = "grand";
            //tags["24 x 32"].Attributes[1, "format"] = "24x32";
            //tags["grands carreaux"].Attributes[1, "réglure"] = "grands carreaux";
            //tags["96"].Attributes[1, "pages"] = "96";
            //tags["96 pages"].Attributes[1, "pages"] = "96";

            //// 2
            //tags["pochette"].Products[3] = "Pochette";
            //tags["pochette papier"].Products[3] = "Pochette Papier";
            //tags["pochette de papier"].Products[3] = "Pochette Papier";
            //tags["pochettes de papier"].Products[3] = "Pochette Papier";
            //tags["calque"].Attributes[3, "type de papier"] = "Calque";
            //tags["50 feuilles"].Attributes[3, "nb de feuilles"] = "50 feuilles";

            //// 3
            //tags["compa"].Products[4] = "Compas";
            //tags["compas"].Products[4] = "Compas";
            //tags["qualite"].Attributes[4, "qualité"] = "bonne";
            //tags["de qualite"].Attributes[4, "qualité"] = "bonne";
            //tags["20 cm"].Attributes[4, "ouverture"] = "20 cm";
            //tags["ouverture 20 cm"].Attributes[4, "ouverture"] = "20 cm";

            return tags;
        }
    }
}
