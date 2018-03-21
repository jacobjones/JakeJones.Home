using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.Discogs.Models;

namespace JakeJones.Home.Music.DataAccess.Discogs.Clients
{
	internal interface IDiscogsClient
	{
		Task<SearchResult> SearchByAlbum(string artist, string title, int perPage, int currentPage);
	}
}