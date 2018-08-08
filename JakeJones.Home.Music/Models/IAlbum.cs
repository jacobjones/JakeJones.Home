using System.Collections.Generic;

namespace JakeJones.Home.Music.Models
{
	public interface IAlbum
	{
		string Artist { get; }
		string Title { get; }
		int Year { get; }
		string ImageUrl { get; }
	}
}