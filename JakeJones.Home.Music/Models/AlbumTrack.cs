namespace JakeJones.Home.Music.Models
{
	public class AlbumTrack : IAlbumTrack
	{
		public AlbumTrack(IAlbum album, ITrack track)
		{
			Album = album;
			Track = track;
		}

		public IAlbum Album { get; }
		public ITrack Track { get; }
	}
}