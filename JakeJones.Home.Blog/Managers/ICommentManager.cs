using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Managers
{
	public interface ICommentManager
	{
		Task<ICollection<IComment>> GetByPostId(int id);

		Task<int> Add(IComment comment);

		//Task Delete(int id);
	}
}
