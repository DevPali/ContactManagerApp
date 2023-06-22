using ContactManagerApp.Models;

namespace ContactManagerApp.Api.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(decimal id);
        User GetUserByUsernameAndPass(string username, string pass);
    }
}
