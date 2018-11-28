using System;
using System.ComponentModel.DataAnnotations;

namespace JakeJones.Home.Blog.Models
{
	public class Comment : IComment
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public int PostId { get; set; }

		public bool IsAdmin { get; set; }

		[Required]
		public string Author { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTimeOffset PublishDate { get; set; }
	}
}