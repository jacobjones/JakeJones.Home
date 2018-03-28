using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Services
{
	public interface IBlogService
	{
		Task<IEnumerable<IPost>> GetPosts(int count, int skip = 0);

		Task<IEnumerable<IPost>> GetPostsByCategory(string category);

		Task<IPost> GetPostBySlug(string slug);

		Task<IPost> GetPostById(string id);

		Task<IEnumerable<string>> GetCategories();

		Task SavePost(IPost post);

		Task DeletePost(IPost post);
	}
}