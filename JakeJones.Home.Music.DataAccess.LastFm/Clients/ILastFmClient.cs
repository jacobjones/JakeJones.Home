using System;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Models;

namespace JakeJones.Home.Music.DataAccess.LastFm.Clients
{
	public interface ILastFmClient
	{
		Task<RecentTracksResult> GetRecentTracks(string user, int limit, DateTime? from = null);
	}
}