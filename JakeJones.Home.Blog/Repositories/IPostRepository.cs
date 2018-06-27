using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Repositories
{
	public interface IPostRepository
	{
		Task<IEnumerable<IPost>> Get(int count, int skip = 0);

		Task<IEnumerable<IPost>> GetByTag(string tag);

		Task<IPost> GetBySegment(string segment);

		Task<IPost> GetById(int id);

		Task Add(IPost post);

		Task Update(IPost post);

		Task Delete(Guid id);
	}
}