using Microsoft.AspNetCore.Rewrite;

namespace JakeJones.Home.Website.Infrastructure.Rewrite
{
	public static class RewriteOptionsExtensions
	{
		public static RewriteOptions AddRedirectToNonWww(this RewriteOptions options)
		{
			options.Rules.Add(new RedirectToNonWwwRule());
			return options;
		}
	}
}