using AutoMapper;
using JakeJones.Home.Blog.Implementation.Models;
using JakeJones.Home.Blog.Models;
using System;

namespace JakeJones.Home.Blog.Implementation.Bootstrappers
{
	public class BlogImplemenatationMapConfiguration : Profile
	{
		public BlogImplemenatationMapConfiguration()
		{
			CreateMap<DateTimeOffset, DateTime>().ConvertUsing(new DateTimeOffsetConverter());
			CreateMap<DateTimeOffset?, DateTime?>().ConvertUsing(new NullableDateTimeOffsetConverter());

			CreateMap<IPost, PostViewModel>();
			CreateMap<IPost, PostListItemViewModel>();
			CreateMap<IPost, PostEditViewModel>();
			CreateMap<PostEditViewModel, IPost>().As<Post>();

			CreateMap<IComment, CommentViewModel>();
			CreateMap<IComment, CommentEditViewModel>();
		}
	}
}