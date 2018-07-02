namespace JakeJones.Home.Blog.Configuration
{
	public class BlogOptions : IBlogOptions
	{
		public int PostsPerPage { get; } = 5;
		public int CommentsCloseAfterDays { get; } = 999;
	}
}