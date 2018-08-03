using System.Collections.Generic;

namespace JakeJones.Home.Blog.Implementation.Models
{
	public class PostListViewModel
	{
		public ICollection<PostListItemViewModel> Posts { get; set; }

		public PaginationViewModel Pagination { get; set; }
	}
}