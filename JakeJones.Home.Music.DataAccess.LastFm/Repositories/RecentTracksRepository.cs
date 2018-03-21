using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Clients;
using JakeJones.Home.Music.DataAccess.LastFm.Connector;
using JakeJones.Home.Music.DataAccess.LastFm.Models;
using JakeJones.Home.Music.Models;
using JakeJones.Home.Music.Repositories;

namespace JakeJones.Home.Music.DataAccess.LastFm.Repositories
{
	public class RecentTracksRepository : IRecentTracksRepository
	{
		//TODO: Move to config?
		private const string Username = "ja-ke";
		private readonly ILastFmClient _lastFmApiConnector;

		public RecentTracksRepository(ILastFmClient lastFmApiConnector)
		{
			_lastFmApiConnector = lastFmApiConnector;
		}
		internal async Task<List<ITrack>> GetRecentlyListenedTracks(int limit, DateTime? from)
		{
			var tracksResult = await _lastFmApiConnector.GetRecentTracks(Username, limit, from);

			if (tracksResult?.RecentTracks?.Tracks == null)
			{
				return null;
			}

			// TODO: Handle exceptions

			List<ITrack> tracks = new List<ITrack>();

			foreach (var track in tracksResult.RecentTracks.Tracks)
			{
				tracks.Add(new Track(track.Artist.Name, track.Name, track.Album.Name));
			}

			return tracks;
		}

		public virtual async Task<IReadOnlyCollection<ITrack>> GetRecentlyListenedTracks(int limit)
		{
			return (await GetRecentlyListenedTracks(limit, null)).AsReadOnly();
		}
	}
}