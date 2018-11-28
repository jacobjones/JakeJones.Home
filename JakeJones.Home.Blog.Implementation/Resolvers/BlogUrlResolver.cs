using System;
using JakeJones.Home.Blog.Implementation.Controllers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Resolvers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace JakeJones.Home.Blog.Implementation.Resolvers
{
	internal class BlogUrlResolver : IBlogUrlResolver
	{
		private readonly IActionContextAccessor _actionContextAccessor;
		private readonly IUrlHelperFactory _urlHelperFactory;

		public BlogUrlResolver(IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
		{
			_actionContextAccessor = actionContextAccessor;
			_urlHelperFactory = urlHelperFactory;
		}

		public string GetUrl(IPost post, bool absolute = false)
		{
			var actionContext = _actionContextAccessor.ActionContext;

			var urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);

			var controllerName = nameof(BlogController);
			controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller", StringComparison.Ordinal));

			var url = urlHelper.Action(nameof(Post), controllerName, new {segment = post.Segment});

			if (string.IsNullOrEmpty(url))
			{
				return null;
			}

			var uri = new Uri(url, UriKind.RelativeOrAbsolute);

			if (uri.IsAbsoluteUri)
			{
				return absolute ? url : uri.PathAndQuery;
			}

			if (!absolute)
			{
				return url;
			}

			// Make this url absolute
			var uriBuilder = new UriBuilder
			{
				Scheme = actionContext.HttpContext.Request.Scheme,
				Host = actionContext.HttpContext.Request.Host.Host,
				Path = url
			};

			if (actionContext.HttpContext.Request.Host.Port.HasValue)
			{
				uriBuilder.Port = actionContext.HttpContext.Request.Host.Port.Value;
			}

			return uriBuilder.ToString();
		}
	}
}