using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Models;

namespace JakeJones.Home.Music.DataAccess.LastFm.Connector
{
	public interface ILastFmApiConnector
	{
		Task<RecentTracksResult> GetRecentTracks(string user, int limit);
	}
}