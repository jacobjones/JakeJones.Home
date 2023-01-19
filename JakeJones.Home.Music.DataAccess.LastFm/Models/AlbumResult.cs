﻿using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class AlbumResult
	{
		[JsonPropertyName("mbid")]
		public string Mbid { get; set; }

		[JsonPropertyName("#text")]
		public string Name { get; set; }
	}
}