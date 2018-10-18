using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Models
{
	internal class CommentEntity
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public bool IsAdmin { get; set; }

		[Required]
		public string Author { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTimeOffset PublishDate { get; set; }

		public virtual PostEntity Post { get; set; }
	}
}
