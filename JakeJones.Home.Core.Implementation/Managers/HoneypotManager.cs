using System;
using System.Linq;
using JakeJones.Home.Core.Implementation.Configuration;
using JakeJones.Home.Core.Managers;
using Microsoft.AspNetCore.Http;

namespace JakeJones.Home.Core.Implementation.Managers
{
	internal class HoneypotManager : IHoneypotManager
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHoneypotOptions _honeypotOptions;

		public HoneypotManager(IHttpContextAccessor httpContextAccessor, IHoneypotOptions honeypotOptions)
		{
			_httpContextAccessor = httpContextAccessor;
			_honeypotOptions = honeypotOptions;
		}

		public bool IsTrapped()
		{
			var httpContext = _httpContextAccessor.HttpContext;

			if (httpContext?.Request?.Form == null)
			{
				return false;
			}

			return httpContext.Request.Form.Any(x =>
				x.Key.StartsWith(_honeypotOptions.FieldNamePrefix, StringComparison.Ordinal) &&
				x.Value.Any(v => !string.IsNullOrEmpty(v)));
		}
	}
}