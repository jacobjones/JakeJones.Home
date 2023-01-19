using System;
using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class DateResult
	{
		[JsonPropertyName("uts")]
		public DateTime Played { get; set; }
	}
}