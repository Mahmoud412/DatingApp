using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Properties.Data;
using DatingApp.API.Properties.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            //valideate requset

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _repo.UserExisit(userForRegisterDto.UserName))
                return BadRequest("User Name already exist");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };

            var CreatedUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {


            var UserFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            if (UserFromRepo == null)
                return Unauthorized();
            // create Token
            var claims = new[]{

                    new Claim(ClaimTypes.NameIdentifier,UserFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, UserFromRepo.UserName)
                };

            // key to sign our token and seve it in the app setting cuz we going to use in a lot of places
            var Key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));
            // createing Creadentials
            var creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha512Signature);


            // create security  token  descriptor which is gonig to contain our claims our expiry data for the token and singing credentils.
            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
                // saving our token 
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new{
                    token = tokenHandler.WriteToken(token)
                });
        }
    }
}