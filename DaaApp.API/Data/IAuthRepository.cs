using System.Threading.Tasks;
using DaaApp.API.Models;

namespace DaaApp.API.Data
{
    public interface IAuthRepository
    {
         // Register User
         Task<User> Register(User user, string password);
         
         // Login
         Task<User> Login(string username, string password);

         // User already exists?
         Task<bool> UserExists(string username);
    }
}