
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Tesseract;

namespace TeDotAd
{
    public class Program
    {
        static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public static double Round(double value)
        {
            return Math.Round(value * 100.0) / 100.0;
        }
        
        public static void Test()
        {
            var files = Directory.GetFiles("./images");

            var reader = new ImageReader();

            int i = 0;
            var groups = files.ToLookup(p => i++ % 8);

            foreach (var group in groups)
            {
                new Thread(new ThreadStart(() =>
                {
                    foreach (var file in group)
                    {
                        try
                        {
                            reader.ReadText(file);
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Message);
                        }
                    }
                }))
                { IsBackground = false }.Start();
            }
        }

        public static void Main(string[] args)
        {
            Test();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
	}
}