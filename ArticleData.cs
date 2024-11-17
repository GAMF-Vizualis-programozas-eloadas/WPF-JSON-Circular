using System.Collections.Generic;

namespace JSON_Circular
{
	public class ArticleData
	{
		public ArticleData()
		{
			Authors=new List<Author>();
			Articles=new List<Article>();
		}

		public List<Author> Authors { get; set; }
		public List<Article> Articles { get; set; }
	}
}