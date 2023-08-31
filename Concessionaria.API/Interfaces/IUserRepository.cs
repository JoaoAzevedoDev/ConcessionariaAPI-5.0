using Concessionaria.API.Models;

namespace Concessionaria.API.Interfaces
{
    public interface IUserRepository
    {
        bool Authentication(string username, string pass);
        bool CreateUser(UserCredential user);
    }
}
