using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Repositories
{
	public interface IPostRepository
	{
		Task<IEnumerable<IPost>> GetAsync(bool isPublished, int count, int skip = 0);

		Task<IEnumerable<IPost>> GetByTagAsync(string tag);

		Task<IPost> GetBySegmentAsync(string segment);

		Task<IPost> GetByIdAsync(int id);

		Task AddAsync(IPost post);

		Task UpdateAsync(IPost post);

		Task DeleteAsync(int id);
	}
}