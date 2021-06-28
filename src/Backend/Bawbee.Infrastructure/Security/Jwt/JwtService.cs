﻿using Bawbee.Application.Services.Secutity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bawbee.Infrastructure.Security.Jwt
{
    public class JwtService : ISecurityTokenService
    {
        private readonly string _secret;
        private readonly string _expDate;

        public JwtService(IConfiguration configuration) // TODO: ctor(SecurityTokenConfiguration) { }
        {
            var jwtConfig = configuration.GetSection("JwtConfig");

            _secret = jwtConfig.GetSection("secret").Value;
            _expDate = jwtConfig.GetSection("expirationHours").Value;
        }

        public UserAcessTokenDto GenerateToken(int userId, string username, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var expiresIn = DateTime.UtcNow.AddHours(double.Parse(_expDate));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, username),
                }),
                Expires = expiresIn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new UserAcessTokenDto
            {
                AccessToken = accessToken,
                ExpiresIn = expiresIn
            };
        }
    }
}
