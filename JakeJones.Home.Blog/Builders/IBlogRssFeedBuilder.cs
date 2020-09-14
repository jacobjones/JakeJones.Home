using System.Threading.Tasks;

namespace JakeJones.Home.Blog.Builders
{
	public interface IBlogRssFeedBuilder
	{
		Task<string> BuildAsync();
	}
}