using JakeJones.Home.Blog.DataAccess.SqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace JakeJones.Home.Blog.DataAccess.SqlServer
{
	public class BlogContext : DbContext
	{
		public BlogContext() { }

		public BlogContext(DbContextOptions<BlogContext> options)
			: base(options)
		{ }

		internal DbSet<PostEntity> Posts { get; set; }

		internal DbSet<CommentEntity> Comments { get; set; }
	}
}