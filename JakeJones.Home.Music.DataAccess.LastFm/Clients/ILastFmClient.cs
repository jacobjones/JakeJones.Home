using System;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Models;

namespace JakeJones.Home.Music.DataAccess.LastFm.Clients
{
	public interface ILastFmClient
	{
		Task<RecentTracksResult> GetRecentTracksAsync(string user, int limit, DateTime? from = null);
	}
}