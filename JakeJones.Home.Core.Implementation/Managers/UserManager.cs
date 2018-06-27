using System;
using System.Text;
using JakeJones.Home.Core.Implementation.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JakeJones.Home.Core.Implementation.Managers
{
    internal class UserManager : IUserManager
    {
        private readonly ILoginOptions _loginOptions;

        public UserManager(ILoginOptions loginOptions)
        {
            _loginOptions = loginOptions;
        }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return username == _loginOptions.Username && VerifyHashedPassword(password);
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
