using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Managers
{
	public interface IBlogManager
	{
		Task<IEnumerable<IPost>> GetAsync(int count, int skip = 0);

		Task<IEnumerable<IPost>> GetAllAsync(bool isPublished);

		Task<IPost> GetBySegmentAsync(string segment);

		Task<IPost> GetByIdAsync(int id);

		Task AddOrUpdateAsync(IPost post);

		Task DeleteAsync(int id);

		bool IsVisibleToUser(IPost post);
	}
}
