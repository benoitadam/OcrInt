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
        private Word[] GetSampleWords()
        {
            var tags = new TagFlyweight();
            var productTypes = new ProductTypeFlyweight();

            var p = productTypes[1];

            tags["2"].Number = 2;
            tags["cahier"].Products[p] = "cahier";
            tags["cahiers"].Products[p] = "cahier";
            tags["grand"].Attributes[p, "format"] = "grand";
            tags["grand format"].Attributes[p, "format"] = "grand";
            tags["24x32"].Attributes[p, "format"] = "grand";
            tags["24 x 32"].Attributes[p, "format"] = "24x32";
            tags["grands carreaux"].Attributes[p, "réglure"] = "grands carreaux";
            tags["96 pages"].Attributes[p, "pages"] = "96";

            var text = @"2 cahiers grand format 24 x 32 grands carreaux 96 pages
                         1 pochette de papier calque";

            text = Doc.CleanText(text);
            var words = Doc.ExtractWords(text);

            return words;
        }

        [Fact(DisplayName = "WordNbr")]
        public void WordNbr()
        {
            var words = GetSampleWords();
            
            Assert.Equal(words[0].LineNbr, 0);
            Assert.Equal(words[1].LineNbr, 0);
            Assert.Equal(words[2].LineNbr, 0);
        }
        
        [Fact(DisplayName = "LineNbr")]
        public void LineNbr()
        {
            var words = GetSampleWords();
            
            Assert.Equal(words[0].LineNbr, 0);
            Assert.Equal(words[1].LineNbr, 0);
            Assert.Equal(words[11].LineNbr, 1);
        }

        [Fact(DisplayName = "LineNbr")]
        public void WordsGroups()
        {
            var words = GetSampleWords();

            Assert.True(words[2].Tags.ContainsKey("grand"));
            Assert.True(words[2].Tags.ContainsKey("grand format"));
            Assert.True(words[4].Tags.ContainsKey("24"));
            Assert.True(words[4].Tags.ContainsKey("24 x 32"));
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