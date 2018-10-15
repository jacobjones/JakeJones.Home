using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;

namespace JakeJones.Home.Website.Infrastructure.Rewrite
{
	public class RedirectToNonWwwRule : IRule
	{
		public virtual void ApplyRule(RewriteContext context)
		{
			var request = context.HttpContext.Request;

			if (request.Host.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
			{
				context.Result = RuleResult.ContinueRules;
				return;
			}

			// Host doesn't begin with www., no redirect necessary
			if (!request.Host.Value.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
			{
				context.Result = RuleResult.ContinueRules;
				return;
			}

			// Create the URL, removing the first four characters from the host
			var redirectUrl = UriHelper.BuildAbsolute(request.Scheme, new HostString(request.Host.Value.Substring(4)),
				request.PathBase, request.Path, request.QueryString);

			var response = context.HttpContext.Response;
			response.StatusCode = (int)HttpStatusCode.MovedPermanently;
			response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location] = redirectUrl;
			context.Result = RuleResult.EndResponse;
		}
	}
}