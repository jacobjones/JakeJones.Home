using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Search.Managers;
using Nest;

namespace JakeJones.Home.Search.DataAccess.Elasticsearch.Managers
{
	internal class SearchManager : ISearchManager
	{
		private readonly IElasticClient _elasticClient;

		private const int MaxBatchSize = 50;

		public SearchManager(IElasticClient elasticClient)
		{
			_elasticClient = elasticClient;
		}

		public async Task UpdateAsync<T>(T item) where T : class
		{
			var descriptor = new BulkDescriptor();
			descriptor.Index<T>(i => i.Document(item));

			var response = await _elasticClient.BulkAsync(descriptor);

			if (!response.IsValid)
			{
				// Handle this.
			}
		}

		public async Task UpdateAsync<T>(IEnumerable<T> items) where T : class
		{
			var stack = new Stack<T>(items);

			while (stack.Any())
			{
				var batchItems = Enumerable.Range(0, Math.Min(stack.Count, MaxBatchSize)).Select(item => stack.Pop());

				var descriptor = new BulkDescriptor();
				descriptor.IndexMany(batchItems);

				var response = await _elasticClient.BulkAsync(descriptor);

				if (!response.IsValid)
				{
					// Handle this.
				}
			}
		}

		public async Task<IReadOnlyCollection<T>> SearchAsync<T>(string query) where T : class
		{
			var response = await _elasticClient.SearchAsync<T>(s => s
				.Query(q => q.Bool(b => b.Must(m => m.QueryString(qs => qs.DefaultField("_all").Query(query))))));

			if (!response.IsValid)
			{
				// Handle this.
			}

			return response.Documents.ToList();
		}

		public async Task DeleteAsync<T>(int id) where T : class
		{
			var response = await _elasticClient.DeleteAsync<T>(id);

			if (!response.IsValid)
			{
				// Handle this.
			}
		}

		public async Task ClearAsync()
		{
			var response = await _elasticClient.DeleteByQueryAsync<object>(
				_elasticClient.ConnectionSettings.DefaultIndex, Types.All,
				x => x.Query(q => q.QueryString(qs => qs.Query("*"))));

			if (!response.IsValid)
			{
				// Handle this.
			}
		}
	}
}