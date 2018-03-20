using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Music.Models;

namespace JakeJones.Home.Music.Repositories
{
	public interface IRecentTracksRepository
	{
		Task<IList<ITrack>> GetRecentlyListenedTracks(int limit);
	}
}