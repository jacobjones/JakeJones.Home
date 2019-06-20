using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JakeJones.Home.Blog.Managers
{
	public interface IImageManager
	{
		Task<string> SaveAsync(IFormFile file);
	}
}