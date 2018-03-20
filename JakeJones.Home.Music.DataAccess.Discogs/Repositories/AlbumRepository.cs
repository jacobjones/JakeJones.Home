using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.Discogs.Connectors;
using JakeJones.Home.Music.Models;
using JakeJones.Home.Music.Repositories;

namespace JakeJones.Home.Music.DataAccess.Discogs.Repositories
{
	internal class AlbumRepository : IAlbumRepository
	{
		private readonly IDiscogsApiConnector _discogsApiConnector;

		public AlbumRepository(IDiscogsApiConnector discogsApiConnector)
		{
			_discogsApiConnector = discogsApiConnector;
		}

		public async Task<IAlbum> GetAlbum(string artist, string title)
		{
			// Should only return one result
			var searchResult = await _discogsApiConnector.SearchByAlbum(artist, title, 1, 1);

			var albumResult = searchResult?.Results.FirstOrDefault();

			if (albumResult == null)
			{
				return null;
			}

			return new Album(artist, title, albumResult.Year, albumResult.Genre.AsReadOnly(), albumResult.Style.AsReadOnly());
		}
	}
}