using System;
using System.Collections.Generic;
using JakeJones.Home.Music.Models;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	internal class RecentTracks
	{
		public RecentTracks(DateTime fetchedDate, IReadOnlyCollection<ITrack> tracks)
		{
			FetchedDate = fetchedDate;
			Tracks = tracks;
		}

		public DateTime FetchedDate { get; }

		public IReadOnlyCollection<ITrack> Tracks { get; }
	}
}