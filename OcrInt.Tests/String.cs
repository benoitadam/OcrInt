using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OcrInt.Tests
{
    public class String
    {
        [Fact]
        public void ShowAscii()
        {
            Assert.Equal("l1\\r\\nl2\\0", "l1\r\nl2\0".ShowAscii());
            var stx_etx = ((char)2).ToString() + ((char)3).ToString();
            Assert.Equal("\\STX\\ETX", stx_etx.ShowAscii());
        }
        
        [Fact]
        public void RemoveDiacriticsExt()
        {
            Assert.Equal("$@¡¢£¤¥¦§".RemoveDiacriticsExt(), "$@¡cEoY¦S");
            Assert.Equal("HeLlo^$".RemoveDiacriticsExt(), "HeLlo^$");
            Assert.Equal("<html></html>".RemoveDiacriticsExt(), "<html></html>");
            Assert.Equal("Héllô!".RemoveDiacriticsExt(), "Hello!");
            Assert.Equal(string.Empty.RemoveDiacriticsExt(), string.Empty);
        }

        [Fact]
        public void RemoveDiacritics()
        {
            Assert.Equal("HeLlo! (you)!", "HéLlô! (you)!".RemoveDiacritics());
        }

        [Fact]
        public void RemoveDiacriticsIso()
        {
            Assert.Equal("HeLlo! (you)!", "HéLlô! (you)!".RemoveDiacriticsIso());
        }

        [Fact]
        public void Simplify()
        {
            Assert.Equal("$@¡¢£¤¥¦§".Simplify(), "$@ ceoy s");
            Assert.Equal("HeLlo^$".Simplify(), "hello'$");
            Assert.Equal("<html></html>".Simplify(), "<html><|html>");
            Assert.Equal("Héllô!".Simplify(), "hello|");
            Assert.Equal("àé ~-(x)".Simplify(), "ae --(x)");
            Assert.Equal("a\r\n   \r\nb\r\n  ".Simplify(), "a\n\nb\n");
            Assert.Equal("  a    b   -  c  \r\n".Simplify(), "a b - c\n");
            Assert.Equal(" 12,34 ".Simplify(), "12.34");
            Assert.Equal("     ".Simplify(), "");
            Assert.Equal("  a  ".Simplify(), "a");
            Assert.Equal("  \r\n  ".Simplify(), "\n");
            Assert.Equal("\r\n  ".Simplify(), "\n");
            Assert.Equal("  \r\n".Simplify(), "\n");
            Assert.Equal("".Simplify(), string.Empty);
            Assert.Equal(((string)null).Simplify(), string.Empty);
        }

        [Fact]
        public void SimplifySpeedBig()
        {
            var chars = new char[10000];
            for (char ch = (char)0; ch < chars.Length; ch++)
                chars[ch] = ch;
            var text = new string(chars);
            
            for (int i = 0; i < 1000; i++)
                text = text.Simplify();
        }
        
        [Fact]
        public void SimplifySpeedShort()
        {
            var chars = new char[1020];
            for (char ch = (char)0; ch < chars.Length; ch++)
                chars[ch] = ch;
            
            for (int i = 0; i < 1000; i++)
            {
                var text = new string(chars, i, 10);
                for (int j = 0; j < 1000; j++)
                    text.Simplify();
            }
        }
    }
}
