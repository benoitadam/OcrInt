using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OcrInt
{
    public static class StringExtends
    {
        private static string[] ASCII = new string[]
        {
            /*   0-15  */"\\0","\\SOH","\\STX","\\ETX","\\EOT","\\ENQ","\\ACK","\\a","\\b","\\t","\\n","\\v","\\f","\\r","\\SO","\\SI",
            /*  16-31  */"\\DLE","\\DC1","\\DC2","\\DC3","\\DC4","\\NAK","\\SYN","\\ETB","\\CAN","\\EM","\\SUB","\\e","\\FS","\\GS","\\RS","\\US",
            /*  32-47  */" ","!","\"","#","$","%","&","\'","(",")","*","+",",","-",".","/",
            /*  48-57  */"0","1","2","3","4","5","6","7","8","9",
            /*  58-64  */":",";","<","=",">","?","@",
            /*  65-90  */"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
            /*  91-96  */"[","\\\\","]","^","_","`",
            /*  97-122 */"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
            /* 123-127 */"{","|","}","~","\\DEL",
            /* 128-143 */"\\PAD","\\HOP","\\BPH","\\NBH","\\IND","\\NEL","\\SSA","\\ESA","\\HTS","\\HTJ","\\VTS","\\PLD","\\PLU","\\RI","\\SS2","\\SS3",
            /* 144-151 */"\\DCS","\\PU1","\\PU2","\\STS","\\CCH","\\MW","\\SPA","\\EPA","\\SOS","\\SGCI","\\SCI","\\CSI","\\ST","\\OSC","\\PM","\\APC",
        };
        public const string DIACRITICS     = "                                                                 ABCDEFGHIJKLMNOPQRSTUVWXYZ      abcdefghijklmnopqrstuvwxyz                                               a               o     AAAAAAACEEEEIIIIDNOOOOO OUUUUY  aaaaaaaceeeeiiii nooooo ouuuuy yAaAaAaCcCcCcCcDdDdEeEeEeEeEeGgGgGgGgHhHhIiIiIiIiIi  JjKk LlLlLl  LlNnNnNn   OoOoOoOoRrRrRrSsSsSsSsTtTtTtUuUuUuUuUuUuWwYyYZzZzZz b        D       Ff    I  l    OOo         t  TUu     z                      AaIiOoUuUuUuUuUu Aa    GgGgKkOoOo  j               ";
        public const string DIACRITICS_EXT = "                                                                 ABCDEFGHIJKLMNOPQRSTUVWXYZ      abcdefghijklmnopqrstuvwxyz                                       cEoY S Oa   o o 23 uJ  1o     AAAAAAACEEEEIIIIDNOOOOOxOUUUUYpBaaaaaaaceeeeiiiionooooo ouuuuypyAaAaAaCcCcCcCcDdDdEeEeEeEeEeGgGgGgGgHhHhIiIiIiIiIiIiJjKkkLlLlLlLlLlNnNnNnnNnOoOoOoOoRrRrRrSsSsSsSsTtTtTtUuUuUuUuUuUuWwYyYZzZzZzRbBbbbbCCcDDaaQEEEFfGVhlIKklAUNnOOoNnPpRSsELtTtTUuUUYyZz33332555p    DDdLLlNNnAaIiOoUuUuUuUuUuaAaAaAaGgGgKkOoOo33jDDdGgHPNnAaAaOo";
        public const string SIMPLIFY       = "          \n                      |'#$%&'()++.-.|0123456789::<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ(|)'-'abcdefghijklmnopqrstuvwxyz(|)- ";
        public const string FIRST_127      = "0SSEEEAabtnvfrSSDDDDDNSECESeFGRU !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~ ";
        private static Encoding ISO88598 = Encoding.GetEncoding("ISO-8859-8");
        private static Encoding UTF8 = Encoding.UTF8;

        /// <summary>
        /// Displays the ACII characters in a string
        /// </summary>
        /// <example>
        /// ShowAscii("l1\r\nl2\0") => "l1\\r\\nl2\\0"
        /// ShowAscii(((char)2).ToString()) => "\\STX"
        /// </example>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ShowAscii(this string str)
        {
            int length = str.Length, i = 0;
            var sb = new StringBuilder(length);
            
            for (; i < length; i++)
            {
                if (str[i] <= 151)
                    sb.Append(ASCII[str[i]]);
                else
                    sb.Append(str[i]);
            }

            string result = sb.ToString();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="array"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string Replace(string str, string array, char replace)
        {
            int length = str.Length, i = 0, max = array.Length;
            var chars = new char[length];
            char ch;

            for (; i < length; i++)
            {
                ch = str[i];
                chars[i] = ch >= max || array[ch] == ' ' ? replace : array[ch];
            }

            return new string(chars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string Replace(string str, string array)
        {
            int length = str.Length, i = 0, max = array.Length;
            var chars = new char[length];
            char ch;

            for (; i < length; i++)
            {
                ch = str[i];
                chars[i] = ch >= max || array[ch] == ' ' ? ch : array[ch];
            }

            return new string(chars);
        }
        
        /// <summary>
        /// Supprimer les accents des caractères accentués
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveDiacriticsIso(this string str)
        {
            var result = UTF8.GetString(ISO88598.GetBytes(str));
            return result;
        }

        /// <summary>
        /// Supprimer les accents des caractères accentués
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string str)
        {
            str = Replace(str, DIACRITICS);
            return str;
        }

        /// <summary>
        /// Supprimer les accents des caractères accentués
        /// </summary>
        /// <example>
        /// Simplify("HéLlô! (you)!") => "hello you"
        /// Simplify("HéLlô! (you)!", "_") => "hello_ _you__"
        /// </example>
        /// <param name="str"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string RemoveDiacriticsExt(this string str)
        {
            str = Replace(str, DIACRITICS_EXT);
            return str;
        }

        /// <summary>
        /// Supprimer les accents des caractères accentués
        /// </summary>
        /// <param name="str"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string Simplify(this string str,
            string array1 = DIACRITICS_EXT, string array2 = SIMPLIFY)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            if (array1 == null || array1.Length == 0)
                throw new ArgumentException("array1 is empty");

            if (array2 == null || array2.Length == 0)
                throw new ArgumentException("array2 is empty");

            int length = str.Length,
                i = 0,
                j = 0,
                max1 = array1.Length,
                max2 = array2.Length;
            
            var chars = new char[length + 1];
            char ch;

            for (; i < length; i++)
            {
                ch = str[i];
                ch = ch >= max1 || array1[ch] == ' ' ? ch : array1[ch];
                ch = ch >= max2 || array2[ch] == ' ' ? ' ' : array2[ch];

                if(ch == ' ')
                {
                    // If the previous character was a space or character of newline
                    if (chars[j] == ' ' || chars[j] == '\n')
                        continue;
                }
                else if (ch == '\n')
                {
                    // If the previous character was a space
                    if (chars[j] == ' ')
                    {
                        chars[j] = ch;
                        continue;
                    }
                }

                chars[++j] = ch;
            }
            
            if (chars[j] == ' ')
                j--;

            if (chars[1] == ' ')
            {
                if (j < 2)
                    return string.Empty;

                return new string(chars, 2, j - 1);
            }

            if (j < 1)
                return string.Empty;

            return new string(chars, 1, j);
        }
    }
}
