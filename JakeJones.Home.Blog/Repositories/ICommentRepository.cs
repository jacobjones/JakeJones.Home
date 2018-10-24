using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Repositories
{
	public interface ICommentRepository
	{
		Task<ICollection<IComment>> GetByPostId(int postId);

		Task<int> Add(IComment comment);
	}
}