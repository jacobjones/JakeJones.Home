using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Repositories
{
	public interface ICommentRepository
	{
		Task<IComment> GetAsync(int id);

		Task<ICollection<IComment>> GetByPostIdAsync(int postId);

		Task<int> AddAsync(IComment comment);

		Task DeleteAsync(IComment comment);

		Task DeleteByPostIdAsync(int postId);
	}
}