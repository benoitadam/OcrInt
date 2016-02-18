using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OcrInt.Tests
{
    public class StringExtendsTest
    {
        [Fact(DisplayName = "StringExtends.ShowAscii")]
        public void ShowAscii()
        {
            Assert.Equal("l1\\r\\nl2\\0", "l1\r\nl2\0".ShowAscii());
            var stx_etx = ((char)2).ToString() + ((char)3).ToString();
            Assert.Equal("\\STX\\ETX", stx_etx.ShowAscii());
        }
        
        [Fact(DisplayName = "StringExtends.RemoveDiacriticsExt")]
        public void RemoveDiacriticsExt()
        {
            var a = "$@¡¢£¤¥¦§".RemoveDiacriticsExt();
            var b = "HeLlo^$".RemoveDiacriticsExt();
            var c = "<html></html>".RemoveDiacriticsExt();
            var d = "Héllô!".RemoveDiacriticsExt();

            Assert.Equal(a, "$@|cEoY|S");
            Assert.Equal(b, "HeLlo^$");
            Assert.Equal(c, "<html></html>");
            Assert.Equal(d, "Hello!");
        }

        [Fact(DisplayName = "StringExtends.RemoveDiacritics")]
        public void RemoveDiacritics()
        {
            Assert.Equal("HeLlo! (you)!", "HéLlô! (you)!".RemoveDiacritics());
        }

        [Fact(DisplayName = "StringExtends.RemoveDiacriticsIso")]
        public void RemoveDiacriticsIso()
        {
            Assert.Equal("HeLlo! (you)!", "HéLlô! (you)!".RemoveDiacriticsIso());
        }

        [Fact(DisplayName = "StringExtends.Simplify")]
        public void Simplify()
        {
            var a = "$@¡¢£¤¥¦§".Simplify();
            var b = "HeLlo^$".Simplify();
            var c = "<html></html>".Simplify();
            var d = "Héllô!".Simplify();

            Assert.Equal(a, "ceoy s");
            Assert.Equal(b, "hello");
            Assert.Equal(c, "html html");
            Assert.Equal(d, "hello");
        }
    }
}
