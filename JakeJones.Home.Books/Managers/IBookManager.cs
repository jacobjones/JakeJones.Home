using System.Threading.Tasks;
using JakeJones.Home.Books.Models;

namespace JakeJones.Home.Books.Managers
{
	public interface IBookManager
	{
		Task<IBook> GetCurrentBookAsync();
	}
}