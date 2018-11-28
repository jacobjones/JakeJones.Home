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
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Segment { get; set; }

		[Required]
		public string Excerpt { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTimeOffset? PublishDate { get; set; }

		public DateTimeOffset LastModified { get; set; }

		public bool IsPublished { get; set; }

		//TODO: Look into this
		[NotMapped]
		public ICollection<string> Tags { get; } = new List<string>();

		public virtual ICollection<CommentEntity> Comments { get; } = new List<CommentEntity>();
	}
}
