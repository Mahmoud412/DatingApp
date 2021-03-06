using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Properties.Data;
using DatingApp.API.Properties.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers (DataContext context)
        {
            if(!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");

                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach(var user in users)
                {
                    byte[] Passwordhash, PasswordSalt;

                    CreatePasswordHash("password",out Passwordhash , out PasswordSalt);

                    user.PasswordHash = Passwordhash;
                    user.PasswordSalt = PasswordSalt;
                    user.UserName = user.UserName.ToLower();

                    context.Users.Add(user);

                    context.SaveChanges();
                }
            }
        }
         private  static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {   
            
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // be cuz we using radnom gen key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            
        }
    }
}