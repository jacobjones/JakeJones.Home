using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Repositories
{
	public interface ITagRepositorya
	{
		Task<IEnumerable<ITag>> Get();

		Task<IEnumerable<ITag>> GetByPostId(Guid postId);
	}
}