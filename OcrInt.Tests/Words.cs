using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcrInt;
using Xunit;

namespace OcrInt.Tests
{
    public class Words
    {
        [Fact]
        public void WordNbr()
        {
            var tags = new TagFlyweight();
            var words = tags.ExtractWords("a b c\nd e f\ng h i");
            
            Assert.Equal(words[0].WordNbr, 0);
            Assert.Equal(words[1].WordNbr, 1);
            Assert.Equal(words[2].WordNbr, 2);
            Assert.Equal(words[3].WordNbr, 0);
            Assert.Equal(words[4].WordNbr, 1);
            Assert.Equal(words[5].WordNbr, 2);
            Assert.Equal(words[6].WordNbr, 0);
            Assert.Equal(words[7].WordNbr, 1);
            Assert.Equal(words[8].WordNbr, 2);
        }

        [Fact]
        public void LineNbr()
        {
            var tags = new TagFlyweight();
            var words = tags.ExtractWords("a b c\nd e f\ng h i");
            
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

        [Fact]
        public void ExtractWords()
        {
            var tags = new TagFlyweight();
            var text = OcrInt.Doc.CleanText("a bc def. gh -ijk lm.no_pq");
            var words = tags.ExtractWords(text);

            Assert.Equal(words[0].Value, "a");
            Assert.Equal(words[1].Value, "bc");
            Assert.Equal(words[2].Value, "def");
            Assert.Equal(words[3].Value, ".");
            Assert.Equal(words[4].Value, "gh");
            Assert.Equal(words[5].Value, "-");
            Assert.Equal(words[6].Value, "ijk");
            Assert.Equal(words[7].Value, "lm");
            Assert.Equal(words[8].Value, ".");
            Assert.Equal(words[9].Value, "no");
            Assert.Equal(words[10].Value, "-");
            Assert.Equal(words[11].Value, "pq");
        }

        [Fact]
        public void Tags()
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
    }
}