using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Models
{
	internal class PostEntity
	{
		[Key]
		[Required]
		public string Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Slug { get; set; }

		[Required]
		public string Excerpt { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTime PublishDate { get; set; }

		public DateTime LastModified { get; set; }

		public bool IsPublished { get; set; }

		//TODO: Look into this
		[NotMapped]
		public IList<string> Categories { get; set; } = new List<string>();

		// TODO: Comments
		//public IList<Comment> Comments { get; } = new List<Comment>();
	}
}
