using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.Discogs.Clients;
using JakeJones.Home.Music.Models;
using JakeJones.Home.Music.Repositories;

namespace JakeJones.Home.Music.DataAccess.Discogs.Repositories
{
	internal class AlbumRepository : IAlbumRepository
	{
		private readonly IDiscogsClient _discogsClient;

		public AlbumRepository(IDiscogsClient discogsClient)
		{
			_discogsClient = discogsClient;
		}

		public virtual async Task<IAlbum> GetAsync(string artist, string title)
		{
			// Should only return one result
			var searchResult = await _discogsClient.SearchByAlbum(artist, title, 1, 1);

			var albumResult = searchResult?.Results.FirstOrDefault();

			if (albumResult == null)
			{
				return null;
			}

			return new Album(artist, title, albumResult.Year, albumResult.ImageUrl);
		}
	}
}