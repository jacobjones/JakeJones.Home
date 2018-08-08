using System.Collections.Generic;

namespace JakeJones.Home.Books.Models
{
	public class Book : IBook
	{
		public Book(string author, string title, string link, string imageUrl)
		{
			Author = author;
			Title = title;
			Link = link;
			ImageUrl = imageUrl;
		}

		public string Author { get; }
		public string Title { get; }
		public string Link { get; }
		public string ImageUrl { get; }
	}
}