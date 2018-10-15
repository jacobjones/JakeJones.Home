using System;

namespace JakeJones.Home.Books.Models
{
	public class Book : IBook
	{
		public Book(string author, string title, string link, string imageUrl, DateTime? read)
		{
			Author = author;
			Title = title;
			Link = link;
			ImageUrl = imageUrl;
			Read = read;
		}

		public string Author { get; }
		public string Title { get; }
		public string Link { get; }
		public string ImageUrl { get; }
		public DateTime? Read { get; }
	}
}