using System;

namespace JakeJones.Home.Music.Models
{
	public interface ITrack
	{
		string Artist { get; }
		string Title { get; }
		string AlbumTitle { get; }
		string AlbumMbid { get; }
	}
}