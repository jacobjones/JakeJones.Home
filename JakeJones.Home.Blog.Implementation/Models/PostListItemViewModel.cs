using System;

namespace JakeJones.Home.Blog.Implementation.Models
{
	public class PostListItemViewModel
	{
		public int Id { get; set; }
		public string AbsoluteUrl { get; set; }
		public string Title { get; set; }
		public string Excerpt { get; set; }
		public DateTime? PublishDate { get; set; }
	}
}