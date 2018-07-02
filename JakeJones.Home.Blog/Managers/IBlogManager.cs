using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Managers
{
	public interface IBlogManager
	{
		Task<IEnumerable<IPost>> Get(int count, int skip = 0);

		Task<IPost> GetBySegment(string segment);

		Task<IPost> GetById(int id);

		Task AddOrUpdate(IPost post);

		Task Delete(int id);

		bool IsVisibleToUser(IPost post);
	}
}
