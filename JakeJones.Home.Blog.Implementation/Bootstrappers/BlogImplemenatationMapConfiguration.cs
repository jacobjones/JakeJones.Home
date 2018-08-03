using AutoMapper;
using JakeJones.Home.Blog.Implementation.Models;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Implementation.Bootstrappers
{
	public class BlogImplemenatationMapConfiguration : Profile
	{
		public BlogImplemenatationMapConfiguration()
		{
			CreateMap<IPost, PostViewModel>();
			CreateMap<IPost, PostListItemViewModel>();
			CreateMap<IPost, PostEditViewModel>();
			CreateMap<PostEditViewModel, IPost>().As<Post>();
		}
	}
}