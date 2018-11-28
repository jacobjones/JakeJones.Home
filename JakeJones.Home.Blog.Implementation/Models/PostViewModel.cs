using System;
using System.Collections.Generic;

namespace JakeJones.Home.Blog.Implementation.Models
{
	public class PostViewModel
	{
		public int Id { get; set; }
		public string Segment { get; set; }
		public string AbsoluteUrl { get; set; }
		public string Title { get; set; }
		public string Excerpt { get; set; }
		public string Content { get; set; }
		public DateTime? PublishDate { get; set; }
		public DateTime LastModified { get; set; }
		public ICollection<CommentViewModel> Comments { get; set; }
	}
}