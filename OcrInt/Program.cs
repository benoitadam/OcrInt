
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Tesseract;

namespace OcrInt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Charge les mots clés dans le poids-mouche (https://fr.wikipedia.org/wiki/Poids-mouche_(patron_de_conception)
            ProgramData.LoadData();
            
            var text = ProgramData.SampleText;
            var doc = new Doc(text);

            // Récupère les mots dans le texte
            doc.Compute();
            
            Console.ReadKey();
        }
    }
}






//public static string CleanText(string value)
//{
//    value = value.ToLowerInvariant().Replace("é", "e").Replace("è", "e");

//    // 21 X 29,7 => 21x29,7
//    value = Regex.Replace(value, @"([0-9]+) ?[x /] ?([0-9]+)", "$1x$2");
//    value = Regex.Replace(value, @"([0-9]+)[,_\-]([0-9]+)", "$1.$2");

//    return value;
//}



//const string INDEX_DIR = @"C:\Index";
//static readonly Net.Util.Version LUCENE_VERSION = Net.Util.Version.LUCENE_29;



//var files = System.IO.Directory.GetFiles("./images");

//foreach (var file in files)
//{
//    if (file.EndsWith(".txt") && !file.EndsWith(".results.txt"))
//    {
//        // Read line
//        var lines = File.ReadAllLines(file);

//        var sb = new StringBuilder();

//        // Read all product
//        var lastProduct = new Product();
//        foreach (var line in lines)
//        {
//            var isInvert = false;
//            double? lastNumber = 1.0;
//            var text = CleanText(line);

//            sb.AppendLine(text);

//            var products = new List<Product>();
//            var words = text.Split(' ');
//            foreach (var word in words)
//            {
//                var tag = TagFlyweight.Get(word);
//                if (tag != null)
//                {
//                    if (tag.Product == ProductType.None)
//                    {
//                        lastProduct = new Product();
//                        lastProduct.Name = tag.Name;
//                        lastProduct.Number = lastNumber;
//                        products.Add(lastProduct);
//                    }

//                    else if (tag.Number != null)
//                    {
//                        lastNumber = tag.Number.Value;
//                    }

//                    else if (tag.IsInvert)
//                    {
//                        isInvert = true;
//                    }

//                    else if (tag.Attributes)
//                    {
//                        lastNumber = 1;
//                        lastProduct.AddAttribute(tag, isInvert);
//                        isInvert = false;
//                    }
//                }
//            }

//            foreach (var product in products)
//            {
//                if (String.IsNullOrWhiteSpace(product.Name))
//                    continue;

//                sb.Append(" > find: ");
//                product.Append(sb);
//                sb.AppendLine();
//            }
//        }

//        var contents = sb.ToString();
//        var fileLa = file.Replace(".txt", ".results.txt");
//        File.WriteAllText(fileLa, contents);
//    }
//}






//string text = FastRand.Default.LoremIpsum() + FastRand.Default.LoremIpsum();
//var searchText = FastRand.Default.LoremIpsum(128);

//// Version parameter is used for backward compatibility.
//// Stop words can also be passed to avoid indexing certain words
//StandardAnalyzer analyzer = new StandardAnalyzer(LUCENE_VERSION);

//// Provide the directory where index is stored
//FSDirectory indexDirectory = FSDirectory.Open(new DirectoryInfo(INDEX_DIR));

/////// ADD INDEX /////

////Create an Index writer object.
//using (var indexWriter = new IndexWriter(indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
//{
//    var doc = new Document();
//    var fldText = new Field("text", "", Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
//    doc.Add(fldText);

//    // Write the document to the index
//    indexWriter.AddDocument(doc);

//    // Optimize and close the writer
//    indexWriter.Optimize();
//}

/////// SEARCH TEXT /////

//var parser = new QueryParser(LUCENE_VERSION, "text", analyzer);
//var qry = parser.Parse(searchText);

//// True opens the index in read only mode
//var searcher = new IndexSearcher(IndexReader.Open(indexDirectory, true));

//// Sorts the results based on athe number of occurrences in each document.
//var collector = TopScoreDocCollector.Create(100, true);

//// Perform the search and get the results from the collector in Hits array
//var hits = collector.TopDocs().ScoreDocs;
//Console.WriteLine("hits.Length:{0}", hits.Length);
//for (int i = 0; i < hits.Length; i++)
//{
//    var doc = searcher.Doc(hits[i].Doc);
//    Console.WriteLine("score:{0}, text:{1}", hits[i].Score, doc.Get("text"));
//}


//   public class Program
//   {
//       static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

//       public static double Round(double value)
//       {
//           return Math.Round(value * 100.0) / 100.0;
//       }

//       public static void Test()
//       {
//           var files = Directory.GetFiles("./images");

//           var reader = new ImageReader();

//           int i = 0;
//           var groups = files.ToLookup(p => i++ % 8);

//           foreach (var group in groups)
//           {
//               new Thread(new ThreadStart(() =>
//               {
//                   foreach (var file in group)
//                   {
//                       try
//                       {
//                           reader.ReadText(file);
//                       }
//                       catch (Exception ex)
//                       {
//                           log.Error(ex.Message);
//                       }
//                   }
//               }))
//               { IsBackground = false }.Start();
//           }
//       }

//       public static void Main(string[] args)
//       {
//           Test();

//           Console.Write("Press any key to continue . . . ");
//           Console.ReadKey(true);
//       }
//}