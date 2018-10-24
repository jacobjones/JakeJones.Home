using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Models;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers
{
	public class BlogDataAccessMapConfiguration : Profile
	{
		public BlogDataAccessMapConfiguration()
		{
			CreateMap<IPost, PostEntity>();
			CreateMap<PostEntity, IPost>().As<Post>();

			//You can use ForPath, a custom resolver on the child type or the AfterMap option instead.'
			CreateMap<IComment, CommentEntity>()
				.ForPath(x => x.Post.Id, s => s.MapFrom(x => x.PostId));
			CreateMap<CommentEntity, IComment>()
				.ForMember(x => x.PostId, s => s.MapFrom(x => x.Post.Id))
				.As<Comment>();
		}
	}
}