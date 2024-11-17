using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Windows;

namespace JSON_Circular
{
	public partial class MainWindow : Window
	{
		private ArticleData AD;
		public MainWindow()
		{
			InitializeComponent();
			AD = new ArticleData();
			CreateLists();
			ShowList();
			var options = new System.Text.Json.JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.LatinExtendedA, UnicodeRanges.Latin1Supplement),
				WriteIndented = true,
				ReferenceHandler = ReferenceHandler.Preserve
			};
			// Serialize all into a string. To avoid infinite loops some
			// objects are replaced by references. 
			string json = JsonSerializer.Serialize(AD, options);
			tbJSON.Text += "JSON:\n\n"+json+"\n";
			// Serialization of an object list into a text file in JSON format.
			File.WriteAllText("Articles.json", json);

			ArticleData DeserializedAD;
			string jsonr = File.ReadAllText("Articles.json");
			tbJSON.Text += "\n-------------------------------------------------------\n\n" + "JSON:\n"+jsonr+"\n\n";
			DeserializedAD = JsonSerializer.Deserialize<ArticleData>(jsonr,options);
			ShowList();
		}

		private void ShowList()
		{
			tbJSON.Text += "Articles:\n\n";
			//var s = "";
			//foreach (var ar in AD.Articles)
			//{
			//	s+= ar.Authors.Aggregate("",
			//					(a, c) => a + (a.Length > 0 ? ", " : "") + c.FirstName+" "+c.LastName) +
			//					": " + ar.Title + ", " + ar.DateofPublication.Year+
			//					"\n";

			//}
			//tbJSON.Text += s;

			var res = from x in AD.Articles
								select x.Authors.Aggregate("", 
								(a, c) => a + (a.Length > 0 ? ", " : "") + c.FirstName + " " + c.LastName) +
								": " + x.Title + ", " + x.DateofPublication.Year;
			tbJSON.Text += res.Aggregate("", (a, c) => a + c + "\n")+"\n";

		}

		public void CreateLists()
		{
			var au1 = new Author
			{
				ID = 1,
				FirstName = "Zsolt Csaba",
				LastName = "Johanyák",
				ORCID = "0000-0001-9285-9178",
				CitationCount = 396,
				FacultyMember = true
			};
			var au2 = new Author
			{
				ID = 2,
				FirstName = "Csaba",
				LastName = "Fábián",
				ORCID = "0000-0002-9446-1566",
				CitationCount = 248,
				FacultyMember = true
			};
			var au3 = new Author
			{
				ID = 3,
				FirstName = "Rafael Pedro",
				LastName = "Alvarez Gil",
				ORCID = "0000-0002-1065-4679",
				CitationCount = 55,
				FacultyMember = true
			};

			var ar1 = new Article
			{
				ID = 1,
				DateofPublication = new DateTime(2018, 10, 15),
				Title = "Fuzzy Model for the Average Delay Time on a Road Ending with a Traffic Light"
			};
			var ar2 = new Article
			{
				ID = 2,
				DateofPublication = new DateTime(2008, 5, 15),
				Title = "Generalization of the single rule reasoning method SURE-LS for the case of arbitrary polygonal shaped fuzzy sets"
			};
			var ar3 = new Article
			{
				ID = 3,
				DateofPublication = new DateTime(2014, 12, 14),
				Title = "Solution methods for two-stage stochastic programming problems"
			};

			ar1.Authors.AddRange(new[] { au1, au3 });
			ar2.Authors.AddRange(new[] { au1, au3 });
			ar3.Authors.Add(au2);

			au1.Articles.AddRange(new[] { ar1, ar2 });
			au2.Articles.Add(ar3);
			au3.Articles.AddRange(new[] { ar1, ar2 });

			AD.Authors.AddRange(new[] { au1, au2, au3 });
			AD.Articles.AddRange(new[] { ar1, ar2, ar3 });
		}

	}
}
