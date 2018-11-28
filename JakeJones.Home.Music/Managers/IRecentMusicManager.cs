using System.Threading.Tasks;
using JakeJones.Home.Music.Models;

namespace JakeJones.Home.Music.Managers
{
	public interface IRecentMusicManager
	{
		Task<ITrack> GetRecentTrackAsync();
	}
}