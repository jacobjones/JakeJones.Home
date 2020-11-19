using System.Collections.Generic;
using System.Threading.Tasks;

namespace JakeJones.Home.Search.Managers
{
	public interface ISearchManager
	{
		Task UpdateAsync<T>(T item) where T : class;

		Task UpdateAsync<T>(IEnumerable<T> items) where T : class;

		Task<IReadOnlyCollection<T>> SearchAsync<T>(string query) where T : class;

		Task DeleteAsync<T>(int id) where T : class;

		Task ClearAsync();
	}
}