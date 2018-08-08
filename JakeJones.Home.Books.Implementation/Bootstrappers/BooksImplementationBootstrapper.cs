using JakeJones.Home.Books.Implementation.Managers;
using JakeJones.Home.Books.Implementation.Managers.Caching;
using JakeJones.Home.Books.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Books.Implementation.Bootstrappers
{
	public static class BooksImplementationBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			services.AddSingleton<IBookManager, BookManagerCachingProxy>();
		}
	}
}