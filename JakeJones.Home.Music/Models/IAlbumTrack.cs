namespace JakeJones.Home.Music.Models
{
	public interface IAlbumTrack
	{
		IAlbum Album { get; }
		ITrack Track { get; }
	}
}