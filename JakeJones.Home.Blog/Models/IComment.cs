using System;

namespace JakeJones.Home.Blog.Models
{
	public interface IComment
	{
		int Id { get; set; }

		int PostId { get; set; }

		bool IsAdmin { get; set; }

		string Author { get; set; }

		string Email { get; set; }

		string Content { get; set; }

		DateTimeOffset PublishDate { get; set; }
	}
}