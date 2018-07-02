using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Resolvers
{
	public interface IBlogUrlResolver
	{
		string GetUrl(IPost post, bool absolute = false);
	}
}