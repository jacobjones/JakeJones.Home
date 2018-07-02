using System;
using System.ComponentModel.DataAnnotations;

namespace JakeJones.Home.Blog.Implementation.Models
{
	public class PostEditViewModel
	{
		public int Id { get; set; }
		public string Segment { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Excerpt { get; set; }
		[Required]
		public string Content { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime LastModified { get; set; }
		public bool IsPublished { get; set; }
		public bool IsNew { get; set; }
	}
}