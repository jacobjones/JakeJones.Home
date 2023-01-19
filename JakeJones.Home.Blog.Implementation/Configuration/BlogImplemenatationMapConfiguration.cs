using System;
using AutoMapper;
using JakeJones.Home.Blog.Implementation.Models;
using JakeJones.Home.Blog.Models;

namespace JakeJones.Home.Blog.Implementation.Configuration
{
	public class BlogImplementationMapConfiguration : Profile
	{
		public BlogImplementationMapConfiguration()
		{
			CreateMap<DateTimeOffset, DateTime>().ConvertUsing(new DateTimeOffsetConverter());
			CreateMap<DateTimeOffset?, DateTime?>().ConvertUsing(new NullableDateTimeOffsetConverter());

			CreateMap<IPost, PostViewModel>();
			CreateMap<IPost, PostListItemViewModel>();
			CreateMap<IPost, PostEditViewModel>();
			
			CreateMap<PostEditViewModel, Post>();
			CreateMap<PostEditViewModel, IPost>().As<Post>();

			CreateMap<IComment, CommentViewModel>();
			CreateMap<IComment, CommentEditViewModel>();
		}
	}
}