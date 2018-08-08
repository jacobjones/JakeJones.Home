﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Clients;
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

		internal async Task<List<ITrack>> GetRecentlyListenedTracksAsync(int limit, DateTime? from)
		{
			var tracksResult = await _lastFmApiConnector.GetRecentTracksAsync(Username, limit, from);

			if (tracksResult?.RecentTracks?.Tracks == null)
			{
				return null;
			}

			// TODO: Handle exceptions

			var tracks = new List<ITrack>();

			foreach (var track in tracksResult.RecentTracks.Tracks)
			{
				tracks.Add(new Track(track.Artist.Name, track.Name, track.Album.Name, track.Album.Mbid));
			}

			return tracks;
		}

		public virtual async Task<IReadOnlyCollection<ITrack>> GetAsync(int limit)
		{
			return (await GetRecentlyListenedTracksAsync(limit, null)).AsReadOnly();
		}
	}
}