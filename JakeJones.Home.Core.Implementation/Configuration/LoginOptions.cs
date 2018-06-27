namespace JakeJones.Home.Core.Implementation.Configuration
{
	internal class LoginOptions : ILoginOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}