using System.Collections.Generic;

namespace JSON_Circular
{
	public class Author
	{
		public int ID { get; set; }
		public string ORCID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public uint CitationCount { get; set; }
		public bool FacultyMember { get; set; }
		public List<Article> Articles { get; set; }

		public Author()
		{
			Articles=new List<Article>();
		}
	}
}