using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcrInt;
using Xunit;

namespace OcrInt.Tests
{
    public class ParserTest
    {
        private TagFlyweight GetTagFlyweight(ProductTypeFlyweight productTypes = null)
        {
            var tags = new TagFlyweight();
            tags["2"].Number = 2;

            var p1 = productTypes[1];
            tags["cahier"].Products[p1] = "Cahier";
            tags["cahiers"].Products[p1] = "Cahier";
            tags["grand"].Attributes[p1, "format"] = "grand";
            tags["grand format"].Attributes[p1, "format"] = "grand";
            tags["24x32"].Attributes[p1, "format"] = "grand";
            tags["24 x 32"].Attributes[p1, "format"] = "24x32";
            tags["grands carreaux"].Attributes[p1, "réglure"] = "grands carreaux";
            tags["96"].Attributes[p1, "pages"] = "96";
            tags["96 pages"].Attributes[p1, "pages"] = "96";

            var p3 = productTypes[3];
            tags["pochette"].Products[p3] = "Pochette";
            tags["pochette de papier"].Products[p3] = "Pochette Papier";
            tags["pochettes de papier"].Products[p3] = "Pochette Papier";
            tags["calque"].Attributes[p3, "reliure"] = "Calque";
            tags["50 feuilles"].Attributes[p3, "nb de feuilles"] = "50 feuilles";

            return tags;
        }

        [Fact(DisplayName = "Words_Nbr")]
        public void Words_Nbr()
        {
            var tags = new TagFlyweight();
            var words = tags.ExtractWords("a b c\nd e f\ng h i");

            // Test WordNbr
            Assert.Equal(words[0].WordNbr, 0);
            Assert.Equal(words[1].WordNbr, 1);
            Assert.Equal(words[2].WordNbr, 2);
            Assert.Equal(words[3].WordNbr, 0);
            Assert.Equal(words[4].WordNbr, 1);
            Assert.Equal(words[5].WordNbr, 2);
            Assert.Equal(words[6].WordNbr, 0);
            Assert.Equal(words[7].WordNbr, 1);
            Assert.Equal(words[8].WordNbr, 2);

            // Test LineNbr
            Assert.Equal(words[0].LineNbr, 0);
            Assert.Equal(words[1].LineNbr, 0);
            Assert.Equal(words[2].LineNbr, 0);
            Assert.Equal(words[3].LineNbr, 1);
            Assert.Equal(words[4].LineNbr, 1);
            Assert.Equal(words[5].LineNbr, 1);
            Assert.Equal(words[6].LineNbr, 2);
            Assert.Equal(words[7].LineNbr, 2);
            Assert.Equal(words[8].LineNbr, 2);
        }

        [Fact(DisplayName = "Words_Tags")]
        public void Words_Tags()
        {
            var tags = new TagFlyweight();
            var productTypes = new ProductTypeFlyweight();

            var p1 = productTypes[1];

            tags["2"].Number = 2;
            tags["cahier"].Products[p1] = "_Cahier_";
            tags["grand"].Attributes[p1, "format"] = "_Grand_";

            var text = "2 grand cahier";
            var words = tags.ExtractWords(text);
            
            Assert.Equal(words[0].Tag.Name, "2");
            Assert.Equal(words[1].Tag.Name, "grand");
            Assert.Equal(words[2].Tag.Name, "cahier");

            Assert.Equal(words[0].Tag.Number, 2);
            Assert.Equal(words[1].Tag.Attributes[p1, "format"], "_Grand_");
            Assert.Equal(words[2].Tag.Products[p1], "_Cahier_");
        }

        [Fact(DisplayName = "Words_CompoundTags")]
        public void Words_CompoundTags()
        {
            var productTypes = new ProductTypeFlyweight();
            var p1 = productTypes[1];
            var p3 = productTypes[3];
            var tags = GetTagFlyweight();

            var text = @"2 cahiers grand format 24 x 32 grands carreaux 96 pages
                         1 pochette de papier calque 50 feuilles";
            
            for (int i = 0; i < 1000; i++)
            {
                var doc = new Doc(text).Compute(tags);

                // Test CompoundTags
                Assert.NotNull(doc.Words[2].CompoundTags["grand format"]);
                Assert.NotNull(doc.Words[2].CompoundTags["grand format"].Attributes[p1, "format"]);
                Assert.NotNull(doc.Words[4].CompoundTags["24 x 32"]);
                Assert.NotNull(doc.Words[4].CompoundTags["24 x 32"].Attributes[p1, "format"]);
                Assert.NotNull(doc.Words[12].CompoundTags["pochette de papier"]);
                Assert.NotNull(doc.Words[12].CompoundTags["pochette de papier"].Products[p1]);
                Assert.NotNull(doc.Words[16].CompoundTags["50 feuilles"]);
                Assert.NotNull(doc.Words[16].CompoundTags["50 feuilles"].Attributes[p3, "nb de feuilles"]);
            }
        }

        [Fact(DisplayName = "Words_IsolateProduct")]
        public void Words_IsolateProduct()
        {
            var productTypes = new ProductTypeFlyweight();
            var p1 = productTypes[1];
            var p3 = productTypes[3];
            var tags = GetTagFlyweight();
            
            var text = @"1 cahier grand format d'environ 96 pages grand carreaux 4 x 32
                         2 cahiers grand format 24 x 32 grands carreaux 96 pages + 2 protège cahier
                         1 pochette de papier calque 50 feuilles";

            for (int i = 0; i < 1000; i++)
            {
                var doc = new Doc(text).Compute(tags);

                // Test CompoundTags
                Assert.NotNull(doc.Words[2].CompoundTags["grand format"]);
                Assert.NotNull(doc.Words[2].CompoundTags["grand format"].Attributes[p1, "format"]);
                Assert.NotNull(doc.Words[4].CompoundTags["24 x 32"]);
                Assert.NotNull(doc.Words[4].CompoundTags["24 x 32"].Attributes[p1, "format"]);
                Assert.NotNull(doc.Words[12].CompoundTags["pochette de papier"]);
                Assert.NotNull(doc.Words[12].CompoundTags["pochette de papier"].Products[p1]);
                Assert.NotNull(doc.Words[16].CompoundTags["50 feuilles"]);
                Assert.NotNull(doc.Words[16].CompoundTags["50 feuilles"].Attributes[p3, "nb de feuilles"]);
            }
        }
    }
}











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




//    public class ParserTest
//{
//    [Fact(DisplayName = "Test")]
//    public void Test()
//    {


//        if (1 == 1) { }



//        //// Recherche les mots clés associés aux mots
//        //int wordsLength = words.Length;
//        //for (int i = 0; i < wordsLength; i++)
//        //{
//        //    var group = new Group();

//        //    for (int j = i; j < wordsLength; j++)
//        //    {
//        //        if (!words[j].CanGroup)
//        //            break;

//        //        group.Add(words[j]);
//        //    }


//        //}



//        //var doc = (
//        //    from line in text.Split('\n')
//        //    select (
//        //            from word in line.Split(' ')
//        //            select Word.Get(word)
//        //        ).ToArray()
//        //    ).ToArray();

//        //int i = 0, length = text.Length;
//        //for (; i < length; i++)
//        //{

//        //}


//        if (1 == 1) { }




//        //var tags = new List<Tag>();

//        //int len = words.Length - 2;

//        //var tag = TagFactory.From(words, 0).FindLink(2);

//        //for (int i = 0; i < len; i += tag.WordsNumber)
//        //{
//        //    tags.Add(tag);
//        //}

//        //tags.Add(TagFactory.From(words, len).FindLink(1));
//        //tags.Add(TagFactory.From(words, len + 1));

//        //var tagsByPosition

//        // 2:NUMBER
//        // cahiers:NOM
//        // grand:ADJECTIF
//        // format:NOM
//        // 24:NUMBER
//        // x:
//        // 

//        //var grm = "2:NUMBER cahiers:S grand format 24 x 32 grands carreaux 96 pages 1 compas de qualité ouverture 20 cm 1 règle plate graduée 30 cm 1 rapporteur d’angles 1 équerre 1 pochette de papier calque";


//        //Assert.Equal(a, "ceoy s");
//        //Assert.Equal(b, "hello");
//        //Assert.Equal(c, "html html");
//        //Assert.Equal(d, "hello");
//    }
//}