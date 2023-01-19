using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Models;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Configuration
{
	public class BlogDataAccessMapConfiguration : Profile
	{
		public BlogDataAccessMapConfiguration()
		{
			CreateMap<IPost, PostEntity>();
			
			CreateMap<PostEntity, Post>();
			CreateMap<PostEntity, IPost>().As<Post>();
			
			CreateMap<IComment, CommentEntity>();

			CreateMap<CommentEntity, Comment>();
			CreateMap<CommentEntity, IComment>()
				.As<Comment>();
		}
	}
}