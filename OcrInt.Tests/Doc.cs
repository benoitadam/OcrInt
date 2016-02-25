using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace OcrInt.Tests
{
    public class Doc
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
"1 grand cahier 24x32.                                                      ", };

        [Fact]
        public void CompoundTags()
        {
            var tags = Data.GetTagFlyweight();

            var text = @"2 cahiers grand format 24 x 32 grands carreaux 96 pages
1 pochette de papier calque 50 feuilles";

            for (int i = 0; i < 1000; i++)
            {
                var doc = new OcrInt.Doc(text).Compute(tags);

                // Test CompoundTags
                Assert.NotNull(doc.Words[2].CompoundTags["grand format"]);
                Assert.NotNull(doc.Words[2].CompoundTags["grand format"].Attributes[1, "format"]);
                Assert.NotNull(doc.Words[4].CompoundTags["24 x 32"]);
                Assert.NotNull(doc.Words[4].CompoundTags["24 x 32"].Attributes[1, "format"]);
                Assert.NotNull(doc.Words[12].CompoundTags["pochette de papier"]);
                Assert.NotNull(doc.Words[12].CompoundTags["pochette de papier"].Products[1]);
                Assert.NotNull(doc.Words[16].CompoundTags["50 feuilles"]);
                Assert.NotNull(doc.Words[16].CompoundTags["50 feuilles"].Attributes[3, "nb de feuilles"]);
            }
        }

        [Fact]
        public void InterpretedProducts()
        {
            var tags = Data.GetTagFlyweight();

            var text = @"2 cahiers grand format 24x32 - grands carreaux - 96 pages.
1 compas de qualité ouverture 20 cm - 1 pochette de papier calque 50 feuilles";

            var doc = new OcrInt.Doc(text).Compute(tags);

            // Test CompoundTags
            Assert.Equal(doc.ExtractedProducts[0].Text, "2 cahiers grand format 24x32 - grands carreaux - 96 pages");
            Assert.Equal(doc.ExtractedProducts[1].Text, "1 compas de qualité ouverture 20 cm");
            Assert.Equal(doc.ExtractedProducts[2].Text, "1 pochette de papier calque 50 feuilles");
        }
    }
}