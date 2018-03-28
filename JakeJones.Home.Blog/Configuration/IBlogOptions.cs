namespace JakeJones.Home.Blog.Configuration
{
	public interface IBlogOptions
	{
		int PostsPerPage { get; }
		int CommentsCloseAfterDays { get; }
	}
}