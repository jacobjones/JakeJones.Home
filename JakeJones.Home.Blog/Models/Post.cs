using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JakeJones.Home.Blog.Models
{
	public class Post : IPost
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Segment { get; set; }

		[Required]
		public string Excerpt { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTimeOffset? PublishDate { get; set; }

		public DateTimeOffset LastModified { get; set; }

		public bool IsPublished { get; set; }

		public IList<string> Tags { get; set; } = new List<string>();


		//public bool AreCommentsOpen(int commentsCloseAfterDays)
		//{
		//	return PublishDate.AddDays(commentsCloseAfterDays) >= DateTime.UtcNow;
		//}
	}
}