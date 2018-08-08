namespace JakeJones.Home.Books.Models
{
	public interface IBook
	{
		string Author { get; }
		string Title { get; }
		string Link { get; }
		string ImageUrl { get; }
	}
}