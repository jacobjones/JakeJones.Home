using System;
using RestSharp.Deserializers;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class DateResult
	{
		[DeserializeAs(Name = "uts")]
		public DateTime Played { get; set; }
	}
}