using System;
using System.Text;
using JakeJones.Home.Core.Implementation.Configuration;
using JakeJones.Home.Core.Managers;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;

namespace JakeJones.Home.Core.Implementation.Managers
{
	internal class UserManager : IUserManager
	{
		private readonly ILoginOptions _loginOptions;
		private readonly IHttpContextAccessor _contextAccessor;

		public UserManager(ILoginOptions loginOptions, IHttpContextAccessor contextAccessor)
		{
			_loginOptions = loginOptions;
			_contextAccessor = contextAccessor;
		}

		public bool ValidateUser(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				return false;
			}

			return username == _loginOptions.Username && VerifyHashedPassword(password);
		}

		public bool IsAdmin()
		{
			return _contextAccessor.HttpContext?.User?.Identity.IsAuthenticated == true;
		}

		private bool VerifyHashedPassword(string password)
		{
			var saltBytes = Encoding.UTF8.GetBytes(_loginOptions.Salt);
			var hashBytes = KeyDerivation.Pbkdf2(password, saltBytes, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8);

			var hashText = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
			return hashText == _loginOptions.Password;
		}
	}
}
