using System.Threading.Tasks;
using DatingApp.API.Properties.Models;

namespace DatingApp.API.Properties.Data
{
    public interface IAuthRepository
    {
         
         Task<User> Register(User user,string Password);


         Task<User> Login (string username , string password);


         Task<bool> UserExisit(string username);

    }
}