using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public class ProgramData
    {
        public static string SampleText = @"
isc  
institution
provins, le 6 juillet 2015
sainte-croix
ecole
coﬂege
lyc e
1 rue des jacobins
77160 provins
rentree 201x51201 6
fournitures genres
telephone 01x60 s8x55 40
fax 0160x58 55x41
www.saintecroix77.fr
 
matieres
fournitures
mathematiques
* 2 cahiers grand format 24 x 32 - grands carreaux - 96 pages.
* 1 compas de qualite ouverture 20 cm
* 1 regle plate graduee 30 cm - 1 rapporteur d’angles — 1 equerre (transpa
* 1 pochette de papier calque.
* calculatrice casio fx92 college.
français
* 2 cahiers 24 x 32 - 96 pages - grands carreaux sans spirale.
t protege-cahiers (2).
* feuilles simples et doubles grand fonnat perforees.
* 1 pochette plastifiee à rabats.
* 1 dictionnaire de poche.
1l5 : des titres de livres (format poche) seront indiques en cours d’annee
la lecture personnelle et suivie.
anglais lv1
* 1 cahier grand format 24 x 32 - grands carreaux - 140 pages.
* 1 protege-cahier pour le couvrir (à rabats).
* copies grand fonnat — grands carreaux.
* copies petit format — grands carreaux.
* 1 cahier de brouillon.
allemand lv1
* a voir avec le professeur à la rentree.
* 1 carnet pour vocabulaire (pas de repertoire).
technologie
arts. plastiques
* 1 grand classeur 4 anneaux.
* a voir avec le professeur à la rentree.
svt
* 1 classeur a4 + feuilles grands carreaux + feuilles blanches a4.
* crayons de couleur — crayons de papier - 1 gomme — colle.
* 1 paire de ciseaux
* 1 cahier de brouillon — pochettes transparentes.
* 1 blouse.
histoirelgeographie
* 1 cahier grand format 24 x 32 - petits carreaux — sans spirale - 96 pages.
* 1 protege-cahiers.
education civique
rlen 1 cahier de tp est fourni.
cdi * a voir avec le professeur à la rentree.
[h æ chiiqcci‘ h. ) * a voir avec le professeur à la rentree.
education musicale
* 1 grand cahier 24x32.
etablissement catholique d'enseignement sous contrat d'association avec l'etat";

        static void AddCsv(ProductType productType, string csv)
        {
            var lines = csv.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var columns = lines[0].Split('\t');

            //var products = new List<Tag>();
            //var attributes = new List<Tag>();

            for (int i = 1; i < lines.Length; i++)
            {
                var cells = lines[i].Split("\t"[0]);
                for (int j = 0; j < cells.Length; j++)
                {
                    if (j == 0)
                    {
                        var productName = cells[j];
                        var tag = TagFlyweight.Default[productName];
                        tag.Products[productType] = productName;
                    }
                    else
                    {
                        var attributeTypeName = columns[j];
                        var attributeName = cells[j];
                        var tag = TagFlyweight.Default[attributeName];
                        tag.Attributes[productType, attributeTypeName] = attributeTypeName;
                        //attributes.Add(attr);
                    }
                }
            }

            //foreach (var a in attributes)
            //    foreach (var p in products)
            //        a.AddLink(p);

            //foreach (var p in products)
            //    foreach (var a in attributes)
            //        p.AddLink(a);
        }

        public static void LoadData()
        {
            for (int i = 0; i < 10; i++)
                TagFlyweight.Default.GetOrCreate(i.ToString()).Number = i;

            TagFlyweight.Default.GetOrCreate("S").Number = 5;
            TagFlyweight.Default.GetOrCreate("L").Number = 1;
            TagFlyweight.Default.GetOrCreate("i").Number = 1;

            TagFlyweight.Default.GetOrCreate("un").Number = 1;
            TagFlyweight.Default.GetOrCreate("deux").Number = 2;
            TagFlyweight.Default.GetOrCreate("trois").Number = 3;
            TagFlyweight.Default.GetOrCreate("quatre").Number = 4;
            TagFlyweight.Default.GetOrCreate("cinq").Number = 5;
            TagFlyweight.Default.GetOrCreate("six").Number = 6;
            TagFlyweight.Default.GetOrCreate("sept").Number = 7;
            TagFlyweight.Default.GetOrCreate("huit").Number = 8;
            TagFlyweight.Default.GetOrCreate("neuf").Number = 9;
            TagFlyweight.Default.GetOrCreate("dix").Number = 10;

            TagFlyweight.Default.GetOrCreate("sans").IsInvert = true;
            TagFlyweight.Default.GetOrCreate("non").IsInvert = true;
            TagFlyweight.Default.GetOrCreate("pas").IsInvert = true;
            TagFlyweight.Default.GetOrCreate("0").IsInvert = true;

            AddCsv(new ProductType(1),
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

            AddCsv(new ProductType(2),
@"Produit	nature	nature 2	nature 3	Format	réglure	grammage	unité
feuilles	Simples	blanche	perforées	10.5x14.8	séyès	70	g
Copies	Doubles	couleurs assorties	non perforées	14.8x21	grands carreaux	80	g/m²
Fiches	Bristol			17x22	petits carreaux	90	gr
				A4	5x5	205	
				24x32	ligné		
					uni		
					travers		");

            AddCsv(new ProductType(3),
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

            AddCsv(new ProductType(4),
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

            AddCsv(new ProductType(5),
@"Produit	nature 1	nature 2	matière	Dos	Format extérieur	format contenu	Couleur
Chemise	avec rabats	avec élastique	Carte	petit		17x22	
Chemise dossier	sans rabats	sans élastique	Carton	grand		A4	
sous-chemise	de classement		cartonnée	25mm		24x32	
pochette	d'archive		plastique	40mm		pour fiche bristol	
Boîte 			polypropylène	60mm		raisin	
carton à dessin				80mm			
				100mm			");

            AddCsv(new ProductType(6),
@"Produit	Matière	Nb de positions	Matière	Format extérieur
trieur	Carte	6 positions	à rivet	240x320
trieur familial	Carton	7 positions	dos extensibles	A4
	cartonnée	8 positions		
	plastique	9 positions		
	polypropylène	10 positions		
		12 positions		
		14 positions		");

            AddCsv(new ProductType(7),
@"Produit		Matière	Matière	paquet de 	format contenu
Pochettes	perforées	plastique	lisse	20	17x22
	coins		grainée	50	A4
				100	24x32
				150	
				200	");

            AddCsv(new ProductType(8),
@"Produit	Matière	Nb de positions	format contenu	touches
Intercalaires	Carte	6 positions	A4	imprimées
	Carton	12 positions	A4+	neutres
	cartonnée		24x32	
	plastique		17x22	
	polypropylène		maxi	
			pour fiche bristol	");

            AddCsv(new ProductType(9),
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

            AddCsv(new ProductType(10),
@"Produit	nature	nature 2	nature 3	format	
rouleau	couvre livres	plastique	crystal	0.4x4m	
	papier kraft	adhésif	lisse	0.4x5m	
		adhésif repositionnable	grainé	0.7x2m	
			incolore		");

            AddCsv(new ProductType(11),
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

            AddCsv(new ProductType(12),
@"Produit	nature	Format	fermeture
enveloppe	blanche	visite	autoadhésive
pochette administrative	Kraft	C6	gommée
	à bulle	DL	
	kraft à soufflet	carré	
		160x220	");
        }
    }
}
