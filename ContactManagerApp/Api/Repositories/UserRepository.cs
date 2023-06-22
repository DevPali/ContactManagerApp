using ContactManagerApp.Models;
using System.Security.Cryptography;
using System.Linq;

namespace ContactManagerApp.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public User GetUserByUsernameAndPass(string username, string password)
        {
            var hashedPass = GetHashedPass(password.ToLower());
            return _context.Users.FirstOrDefault((user) => user.Username == username && user.HashedPassword == hashedPass);
        }

        private static string GetHashedPass(string password)
        {
            using var md5Hasher = new MD5CryptoServiceProvider();
            var encoder = new System.Text.UTF8Encoding();
            return BitConverter.ToString(md5Hasher.ComputeHash(encoder.GetBytes(password))).Replace("-", "");
        }
        public User GetUserById(decimal id)
        {
            return _context.Users.FirstOrDefault((user) => user.ID == id);
        }
    }
}
