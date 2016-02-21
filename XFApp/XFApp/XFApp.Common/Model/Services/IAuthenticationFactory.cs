using System;
using System.Text;

namespace XFApp.Common.Model.Services
{
    public interface IAuthenticationFactory
    {
        string BasicAuthenticationCredentials();
    }

    class AuthenticationFactory : IAuthenticationFactory
    {
        private readonly string _login;
        private readonly string _token;

        public AuthenticationFactory()
        {
            _login = "";
            _token = "";
        }


        public string BasicAuthenticationCredentials()
        {
            var byteArray = Encoding.UTF8.GetBytes($"{_login}:{_token}");
            return "Basic " + Convert.ToBase64String(byteArray);
        }
    }
}