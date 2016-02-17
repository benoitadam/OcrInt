using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TeDotAd.Tests
{
    public class ImageReaderTest
    {
        static string[] tom = new string[] {
"2 cahiers grand format 24x32 - grands carreaux - 96 pages.                 ",
"1 compas de qualité ouverture 20 cm                                        ",
"1 règle plate graduée 30 cm - 1 rapporteur d’angles - 1 équerre(transpa    ",
"1 pochette de papier calque.                                               ",
"Calculatrice CASIO FX92 Collège.                                           ",
"2 cahiers 24x32 — 96 pages - grands carreaux sans spirale.                 ",
"Protège-cahiers (2).                                                       ",
"Feuilles simples et doubles grand format perforées.                        ",
"1 pochette plastifiée à rabats.                                            ",
"1 dictionnaire de poche.                                                   ",
"1 cahier grand format 24x32 - grands carreaux - 140 pages.                 ",
"1 protège-cahier pour le couvrir (à rabats).                               ",
"Copies grand format - grands carreaux.                                     ",
"Copies petit format — grands carreaux.                                     ",
"1 cahier de brouillon.                                                     ",
"1 carnet pour vocabulaire (pas de répertoire).                             ",
"1 grand classeur 4 anneaux.                                                ",
"1 classeur A4 + feuilles grands carreaux + feuilles blanches A4.           ",
"Crayons de couleur - crayons de papier - 1 gomme — colle.                  ",
"1 paire de ciseaux                                                         ",
"1 cahier de brouillon — Pochettes transparentes.                           ",
"1 blouse.                                                                  ",
"1 cahier grand format 24x32 - petits carreaux — sans spirale — 96 pages.   ",
"1 protège-cahiers.                                                         ",
"RIEN 1 cahier de TP est fourni.                                            ",
"1 grand cahier 24x32.                                                      ",
        };

        [Fact(DisplayName = "Read")]
        public void Compare()
        {
            #region TEXT
            var text = @"Isc '

Institution

Provins, le 6 juillet 2015

Sainte-Croix

École
Coﬂège
Lyc e

1 rue des Jacobins
77160 Provins

 

RENTREE 201 51201 6
FOURNITURES GEMES

 

 

 

téléphone 01 60 58 55 40
fax 0160 58 55 41

www.53intecroix77.fr

 

 

MATIERES

FOURNITURES

 

 

Mathématiques

* 2 cahiers grand format 24x32 - grands carreaux - 96 pages.

* 1 compas de qualité ouverture 20 cm

* 1 règle plate graduée 30 cm - 1 rapporteur d’angles - 1 équerre (transpa
* 1 pochette de papier calque.

* Calculatrice CASIO FX92 Collège.

 

Français

* 2 cahiers 24x32 — 96 pages - grands carreaux sans spirale.

T Protège-cahiers (2).

* Feuilles simples et doubles grand format perforées.

* 1 pochette plastifiée à rabats.

* 1 dictionnaire de poche.

115 : Des titres de livres (format poche) seront indiqués en cours d‘année
la lecture personnelle et suivie.

 

Anglais LV1

* 1 cahier grand format 24x32 - grands carreaux - 140 pages.
* 1 protège-cahier pour le couvrir (à rabats).

* Copies grand format - grands carreaux.

* Copies petit format — grands carreaux.

* 1 cahier de brouillon.

 

Allemand LV1

* A voir avec le professeur à la rentrée.
* 1 carnet pour vocabulaire (pas de répertoire).

 

Technologie

* 1 grand classeur 4 anneaux.

 

Arts Plastiques

* A voir avec le professeur à la rentrée.

 

SVT

* 1 classeur A4 + feuilles grands carreaux + feuilles blanches A4.
* Crayons de couleur - crayons de papier - 1 gomme — colle.

* 1 paire de ciseaux

* 1 cahier de brouillon — Pochettes transparentes.

* 1 blouse.

 

HistoireIGéographie

* 1 cahier grand format 24x32 - petits carreaux — sans spirale — 96 pages.
* 1 protège-cahiers.

 

Education Civique

RlEN 1 cahier de TP est fourni.

 

 

CDI * A voir avec le professeur à la rentrée.
[Il re CHII'CICCI. 5|. ) * A voir avec le professeur à la rentrée.

 

Education Musicale

 

* 1 grand cahier 24x32.

 

Établissement catholique d’enseignement sous contrat d’association avec l’État

";
            #endregion

            var sb = new StringBuilder(text);

            foreach(var line in tom)
            {
                //TextHelper.Clean(line);
            }
            sb[0] = '0';
            sb[1] = '1';
            sb[2] = '2';

            text = sb.ToString();
        }

        [Fact(DisplayName = "Read")]
        public void Read()
        {
            var imagePath = "./images/tom.jpg";

            try
            {
                //var ir = new ImageReader();
                //string text = ir.ReadText("./images/tom.jpg");

                //var sb = new StringBuilder(text);
                //foreach(var line in tom)
                //{

                //}
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ImageReader.Read({imagePath}) : {ex.Message}", ex);
            }
        }
    }
}
