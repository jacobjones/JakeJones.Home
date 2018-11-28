using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Managers
{
	public interface ICommentManager
	{
		Task<ICollection<IComment>> GetByPostIdAsync(int id);

		Task<int> AddAsync(IComment comment);

		Task DeleteAsync(int id);
	}
}
