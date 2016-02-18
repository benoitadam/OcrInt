
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
        //const string INDEX_DIR = @"C:\Index";
        //static readonly Net.Util.Version LUCENE_VERSION = Net.Util.Version.LUCENE_29;

        public static string CleanText(string value)
        {
            value = value.ToLowerInvariant().Replace("é", "e").Replace("è", "e");

            // 21 X 29,7 => 21x29,7
            value = Regex.Replace(value, @"([0-9]+) ?[x /] ?([0-9]+)", "$1x$2");
            value = Regex.Replace(value, @"([0-9]+)[,_\-]([0-9]+)", "$1.$2");

            return value;
        }

        static void AddCsv(string csv)
        {
            csv = CleanText(csv);

            var lines = csv.Split(new char[] { "\r"[0], "\n"[0] }, StringSplitOptions.RemoveEmptyEntries);
            var columns = lines[0].Split("\t"[0]);

            var products = new List<Tag>();
            var attributes = new List<Tag>();

            for (int i = 1; i < lines.Length; i++)
            {
                var cells = lines[i].Split("\t"[0]);
                for (int j = 0; j < cells.Length; j++)
                {
                    if (j == 0)
                    {
                        var productName = cells[j];
                        var product = Tag.GetOrCreate(productName);
                        product.IsProduct = true;
                        products.Add(product);
                    }
                    else
                    {
                        var attr = Tag.GetOrCreate(cells[j]);
                        attr.AttributeName = columns[j];
                        attr.IsAttribute = true;
                        attributes.Add(attr);
                    }
                }
            }

            foreach (var a in attributes)
                foreach (var p in products)
                    a.AddLink(p);

            foreach (var p in products)
                foreach (var a in attributes)
                    p.AddLink(a);
        }

        static void Main(string[] args)
        {
            #region DATA

            for (int i = 0; i < 10; i++)
                Tag.GetOrCreate(i.ToString()).Number = i;

            Tag.GetOrCreate("S").Number = 5;
            Tag.GetOrCreate("L").Number = 1;
            Tag.GetOrCreate("i").Number = 1;

            Tag.GetOrCreate("un").Number = 1;
            Tag.GetOrCreate("deux").Number = 2;
            Tag.GetOrCreate("trois").Number = 3;
            Tag.GetOrCreate("quatre").Number = 4;
            Tag.GetOrCreate("cinq").Number = 5;
            Tag.GetOrCreate("six").Number = 6;
            Tag.GetOrCreate("sept").Number = 7;
            Tag.GetOrCreate("huit").Number = 8;
            Tag.GetOrCreate("neuf").Number = 9;
            Tag.GetOrCreate("dix").Number = 10;

            Tag.GetOrCreate("sans").IsInvert = true;
            Tag.GetOrCreate("non").IsInvert = true;
            Tag.GetOrCreate("pas").IsInvert = true;
            Tag.GetOrCreate("0").IsInvert = true;

            AddCsv(
@"Produit	nature	Format	nombre de pages	reliure	réglure	couverture	couleur	grammage	unité
cahier	TP	petit	32	piqure	séyès	polypropylène	bleu	90	g
carnet	de texte	moyen	48	piqué	5x5	carte	rouge	80	g/m²
répertoire	de brouillon	grand	64	agrafé	grands carreaux	carte pelliculée	vert	60	gr
Bloc	de poésie	maxi	96	spirale	petits carreaux	rigide	noir	55	
Koverbook	maternelle	17x22	100	reliure intégrale	ligné	souple	orange	70	
	de musique	A4	140	brochure	uni		jaune		
	bureau	21x29.7	180	encollé	travers		rose		
	de cours	24x32	192		double ligne		violet		
	de dessin	14.8x21	200		portée				
	microperforée	A4+	240						
		210x315							");

            AddCsv(
@"Produit	nature	nature 2	nature 3	Format	réglure	grammage	unité
feuilles	Simples	blanche	perforées	10.5x14.8	séyès	70	g
Copies	Doubles	couleurs assorties	non perforées	14.8x21	grands carreaux	80	g/m²
Fiches	Bristol			17x22	petits carreaux	90	gr
				A4	5x5	205	
				24x32	ligné		
					uni		
					travers		");

            AddCsv(
@"Produit	reliure	Format	nb de feuilles	grammage	unité
Pochette	dessin	24x32	8 feuilles	90	g
bloc	Calque	A4	12 feuilles	125	g/m²
Pochette Papier	Millémétré	21x297.7	24 feuilles	160	gr
Bloc croquis 	dessin à grain	A3	50 feuilles	180	
	dessin C à grain	raisin	100 pages	200	
	dessin couleur pastel	16x21		224	
	dessin couleur vive				
	technique				
	papier bristol				
	buvard				
	mi-teinte				");

            AddCsv(
@"Produit	Nature	Matière	Dos	anneaux	mécanique	Format extérieur	format contenu
Classeur	souple	Carton	petit	2	19mm	240x320	A4
cahier-classeur	rigide	rembordé	grand	4	20mm	A4	17x22
Classeur à Levier		polypropylène	21mm		25mm		105x148
		plastique	25mm		30mm		148x210
		carte	30mm		35mm		pour bristol
			35mm		40mm		
			40mm		50mm		
			50mm		70mm		
			60mm		75mm		
			70mm		80mm		
			80mm				");

            AddCsv(
@"Produit	nature 1	nature 2	matière	Dos	Format extérieur	format contenu	Couleur
Chemise	avec rabats	avec élastique	Carte	petit		17x22	
Chemise dossier	sans rabats	sans élastique	Carton	grand		A4	
sous-chemise	de classement		cartonnée	25mm		24x32	
pochette	d'archive		plastique	40mm		pour fiche bristol	
Boîte 			polypropylène	60mm		raisin	
carton à dessin				80mm			
				100mm			");

            AddCsv(
@"Produit	Matière	Nb de positions	Matière	Format extérieur
trieur	Carte	6 positions	à rivet	240x320
trieur familial	Carton	7 positions	dos extensibles	A4
	cartonnée	8 positions		
	plastique	9 positions		
	polypropylène	10 positions		
		12 positions		
		14 positions		");

            AddCsv(
@"Produit		Matière	Matière	paquet de 	format contenu
Pochettes	perforées	plastique	lisse	20	17x22
	coins		grainée	50	A4
				100	24x32
				150	
				200	");

            AddCsv(
@"Produit	Matière	Nb de positions	format contenu	touches
Intercalaires	Carte	6 positions	A4	imprimées
	Carton	12 positions	A4+	neutres
	cartonnée		24x32	
	plastique		17x22	
	polypropylène		maxi	
			pour fiche bristol	");

            AddCsv(
@"Produit	reliure	Couverture 	Nb de vues / Pochettes	format contenu
Protège-documents	soudée	opaque	20 vues	A4
porte vues	à anneaux	translucide	40 vues	A4+
lutin		personnalisable	60 vues	24x32
		avec porte étiquette	80 vues	17x22
			100 vues	
			120 vues	
			140 vues	
			160 vues	
			180 vues	
			200 vues	
			10 pochettes	
			20 pochettes	
			30 pochettes	
			40 pochettes	
			50 pochettes	
			60 pochettes	
			70 pochettes	
			80 pochettes	
			90 pochettes	
			100 pochettes	");

            AddCsv(
@"Produit	nature	nature 2	nature 3	format	
rouleau	couvre livres	plastique	crystal	0.4x4m	
	papier kraft	adhésif	lisse	0.4x5m	
		adhésif repositionnable	grainé	0.7x2m	
			incolore		");

            AddCsv(
@"Produit	nature	nature 2	matière	format	couleur
protège-cahier	translucide	avec rabats	plastique	17x22	rouge
	opaque	sans rabats	PVC	A4	bleu
			polypropylène	24x32	vert
			cristal		jaune
					incolore
					noir
					violet
					rose
					orange");

            AddCsv(
@"Produit	nature	Format	fermeture
enveloppe	blanche	visite	autoadhésive
pochette administrative	Kraft	C6	gommée
	à bulle	DL	
	kraft à soufflet	carré	
		160x220	");

            #endregion

            var files = System.IO.Directory.GetFiles("./images");

            foreach (var file in files)
            {
                if (file.EndsWith(".txt") && !file.EndsWith(".results.txt"))
                {
                    // Read line
                    var lines = File.ReadAllLines(file);

                    var sb = new StringBuilder();

                    // Read all product
                    var lastProduct = new Product();
                    foreach (var line in lines)
                    {
                        var isInvert = false;
                        var lastNumber = 1;
                        var text = CleanText(line);

                        sb.AppendLine(text);

                        var products = new List<Product>();
                        var words = text.Split(' ');
                        foreach (var word in words)
                        {
                            var tag = Tag.Get(word);
                            if (tag != null)
                            {
                                if (tag.IsProduct)
                                {
                                    lastProduct = new Product();
                                    lastProduct.Name = tag.Name;
                                    lastProduct.Number = lastNumber;
                                    products.Add(lastProduct);
                                }

                                else if (tag.IsNumber)
                                {
                                    lastNumber = tag.Number;
                                }

                                else if (tag.IsInvert)
                                {
                                    isInvert = true;
                                }

                                else if (tag.IsAttribute)
                                {
                                    lastNumber = 1;
                                    lastProduct.AddAttribute(tag, isInvert);
                                    isInvert = false;
                                }
                            }
                        }

                        foreach (var product in products)
                        {
                            if (String.IsNullOrWhiteSpace(product.Name))
                                continue;

                            sb.Append(" > find: ");
                            product.Append(sb);
                            sb.AppendLine();
                        }
                    }

                    var contents = sb.ToString();
                    var fileLa = file.Replace(".txt", ".results.txt");
                    File.WriteAllText(fileLa, contents);
                }
            }






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

            Console.ReadKey();
        }
    }



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
}