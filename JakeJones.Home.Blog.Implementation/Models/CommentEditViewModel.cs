using System.ComponentModel.DataAnnotations;

namespace JakeJones.Home.Blog.Implementation.Models
{
	public class CommentEditViewModel
	{
		[Required]
		public string Author { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Content { get; set; }
	}
}