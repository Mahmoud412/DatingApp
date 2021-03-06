using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Properties.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Properties.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;


        }
        public async Task<User> Login(string username, string password)
        {
                var user  = await _context.Users.FirstOrDefaultAsync(x=> x.UserName==username);

                if(user==null)
                return null;

                if (!VerifyPasswordHash(password,user.PasswordSalt,user.PasswordHash))
                return null;


                return user;


        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {

             using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                    for(int i =0; i <computedHash.Length; i++ )
                    {
                        if (computedHash[i]!=passwordHash[i]) return false;
                    }
            }

            return true;
        }

        public async Task<User> Register(User user, string Password)
        {

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(Password,out passwordHash, out passwordSalt);

            user.PasswordHash =passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return (user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {   
            
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // be cuz we using radnom gen key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            
        }

        public async Task<bool> UserExisit(string username)
        {
           if( await _context.Users.AnyAsync(x=>x.UserName==username))
           return true;
           else{
               return false;
           }
        }
    }
}