using Concessionaria.API.Interfaces;
using Concessionaria.API.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Concessionaria.API.Services
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public JwtTokenManager(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public string Authenticate(string userName, string password)
        {
            if (!_userRepository.Authentication(userName, password))
                return null;

            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string AuthenticateWhithoutExpiration(string userName, string password)
        {
            if (!_userRepository.Authentication(userName, password))
                return null;

            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public bool CreateNewUser(UserCredential user)
        {
            _userRepository.CreateUser(user);
            return true;
        }
    }
}
