using System;

namespace JakeJones.Home.Blog.Implementation.Models
{
	public class CommentViewModel
	{
		public int Id { get; set; }
		public bool IsAdmin { get; set; }
		public string Author { get; set; }
		public string Email { get; set; }
		public string Content { get; set; }
		public DateTimeOffset PublishDate { get; set; }
	}
}