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
            
            tags["cahier"].Products[1] = "Cahier";
            tags["cahiers"].Products[1] = "Cahier";
            tags["grand"].Attributes[1, "format"] = "grand";
            tags["grand format"].Attributes[1, "format"] = "grand";
            tags["24x32"].Attributes[1, "format"] = "grand";
            tags["24 x 32"].Attributes[1, "format"] = "24x32";
            tags["grands carreaux"].Attributes[1, "réglure"] = "grands carreaux";
            tags["96"].Attributes[1, "pages"] = "96";
            tags["96 pages"].Attributes[1, "pages"] = "96";

            tags["pochette"].Products[3] = "Pochette";
            tags["pochette de papier"].Products[3] = "Pochette Papier";
            tags["pochettes de papier"].Products[3] = "Pochette Papier";
            tags["calque"].Attributes[3, "reliure"] = "Calque";
            tags["50 feuilles"].Attributes[3, "nb de feuilles"] = "50 feuilles";

            return tags;
        }
    }
}
