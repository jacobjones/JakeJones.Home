using System;

namespace JakeJones.Home.Core
{
    public interface IUserManager
    {
        bool ValidateUser(string username, string password);
    }
}
