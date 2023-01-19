using JakeJones.Home.Books.Implementation.Managers.Caching;
using JakeJones.Home.Books.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Books.Implementation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBooks(this IServiceCollection services)
		{
			services.AddSingleton<IBookManager, BookManagerCachingProxy>();

			return services;
		}
	}
}