using Concessionaria.API.Models;

namespace Concessionaria.API.Interfaces
{
    public interface IJwtTokenManager
    {
        string Authenticate(string userName, string password);
        string AuthenticateWhithoutExpiration(string userName, string password);
        bool CreateNewUser(UserCredential user);
    }
}
