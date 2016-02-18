using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcrInt;
using Xunit;

namespace OcrInt.Tests
{
    //public class TagFlyweight
    //{
    //    public static Tag Get(string word)
    //    {
    //        return new Tag();
    //    }
    //}

    //public class Tag
    //{


    //    internal IEnumerable<Tag> FindLink(int v)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class OneTag : Tag
    //{
    //    private string[] words;
    //    private int index;

    //    public OneTag(string[] words, int index)
    //    {
    //        this.words = words;
    //        this.index = index;
    //    }

    //    public override string ToString()
    //    {
    //        return words[index];
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        return words[index].Equals(obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return words[index].GetHashCode();
    //    }
    //}

    //public class MultiTag : Tag
    //{
    //    private string[] words;
    //    private int[] indexes;

    //    public MultiTag(string[] words, int[] indexes)
    //    {
    //        this.words = words;
    //        this.indexes = indexes;
    //    }

    //    public override string ToString()
    //    {
    //        var sb = new StringBuilder(40);
    //        foreach(var i in indexes)
    //            sb.Append(words[i]).Append(' ');
    //        sb.Length--;
    //        return sb.ToString();
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        return words[index].Equals(obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return words[index].GetHashCode();
    //    }
    //}


    

    public class ParserTest
    {
        [Fact(DisplayName = "Test")]
        public void Test()
        {
            var text = @"
isc  
institution
provins, le 6 juillet 2015
sainte-croix
ecole
coﬂege
lyc e
1 rue des jacobins
77160 provins
rentree 201x51201 6
fournitures genres
telephone 01x60 s8x55 40
fax 0160x58 55x41
www.saintecroix77.fr
 
matieres
fournitures
mathematiques
* 2 cahiers grand format 24 x 32 - grands carreaux - 96 pages.
* 1 compas de qualite ouverture 20 cm
* 1 regle plate graduee 30 cm - 1 rapporteur d’angles — 1 equerre (transpa
* 1 pochette de papier calque.
* calculatrice casio fx92 college.
français
* 2 cahiers 24 x 32 - 96 pages - grands carreaux sans spirale.
t protege-cahiers (2).
* feuilles simples et doubles grand fonnat perforees.
* 1 pochette plastifiee à rabats.
* 1 dictionnaire de poche.
1l5 : des titres de livres (format poche) seront indiques en cours d’annee
la lecture personnelle et suivie.
anglais lv1
* 1 cahier grand format 24 x 32 - grands carreaux - 140 pages.
* 1 protege-cahier pour le couvrir (à rabats).
* copies grand fonnat — grands carreaux.
* copies petit format — grands carreaux.
* 1 cahier de brouillon.
allemand lv1
* a voir avec le professeur à la rentree.
* 1 carnet pour vocabulaire (pas de repertoire).
technologie
arts. plastiques
* 1 grand classeur 4 anneaux.
* a voir avec le professeur à la rentree.
svt
* 1 classeur a4 + feuilles grands carreaux + feuilles blanches a4.
* crayons de couleur — crayons de papier - 1 gomme — colle.
* 1 paire de ciseaux
* 1 cahier de brouillon — pochettes transparentes.
* 1 blouse.
histoirelgeographie
* 1 cahier grand format 24 x 32 - petits carreaux — sans spirale - 96 pages.
* 1 protege-cahiers.
education civique
rlen 1 cahier de tp est fourni.
cdi * a voir avec le professeur à la rentree.
[h æ chiiqcci‘ h. ) * a voir avec le professeur à la rentree.
education musicale
* 1 grand cahier 24x32.
etablissement catholique d'enseignement sous contrat d'association avec l'etat";

            var doc = new Doc(text);
            
            // Récupère les mots dans le texte
            doc.ExtractWords(text);
            var words = Word.GetWords(text);

            // Recherche les mots clés associés aux mots
            int wordsLength = words.Length;
            for (int i = 0; i < wordsLength; i++)
            {
                var group = new Group();

                for (int j = i; j < wordsLength; j++)
                {
                    if (!words[j].CanGroup)
                        break;

                    group.Add(words[j]);
                }


            }



            //var doc = (
            //    from line in text.Split('\n')
            //    select (
            //            from word in line.Split(' ')
            //            select Word.Get(word)
            //        ).ToArray()
            //    ).ToArray();

            //int i = 0, length = text.Length;
            //for (; i < length; i++)
            //{

            //}


            if (1 == 1) { }




            //var tags = new List<Tag>();

            //int len = words.Length - 2;

            //var tag = TagFactory.From(words, 0).FindLink(2);

            //for (int i = 0; i < len; i += tag.WordsNumber)
            //{
            //    tags.Add(tag);
            //}

            //tags.Add(TagFactory.From(words, len).FindLink(1));
            //tags.Add(TagFactory.From(words, len + 1));

            //var tagsByPosition

            // 2:NUMBER
            // cahiers:NOM
            // grand:ADJECTIF
            // format:NOM
            // 24:NUMBER
            // x:
            // 

            //var grm = "2:NUMBER cahiers:S grand format 24 x 32 grands carreaux 96 pages 1 compas de qualité ouverture 20 cm 1 règle plate graduée 30 cm 1 rapporteur d’angles 1 équerre 1 pochette de papier calque";


            //Assert.Equal(a, "ceoy s");
            //Assert.Equal(b, "hello");
            //Assert.Equal(c, "html html");
            //Assert.Equal(d, "hello");
        }
    }
}
