using System.Threading.Tasks;
using JakeJones.Home.Music.Models;

namespace JakeJones.Home.Music.Repositories
{
	public interface IAlbumRepository
	{
		Task<IAlbum> GetAsync(string artist, string title);
	}
}