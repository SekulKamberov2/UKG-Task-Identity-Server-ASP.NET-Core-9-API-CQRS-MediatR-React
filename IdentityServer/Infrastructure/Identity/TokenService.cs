﻿namespace IdentityServer.Infrastructure.Identity
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.IdentityModel.Tokens;

    using IdentityServer.Application.Interfaces;
    using IdentityServer.Application.Results;
    using IdentityServer.Domain.Models;

    public class TokenService : ITokenService
    {
        private readonly string _issuer; 
        private readonly string _audience; 
        private readonly string _secretKey; 

        public TokenService(IConfiguration configuration)
        {
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _secretKey = configuration["Jwt:Key"];
        }

        public IdentityResult<string> GenerateToken(string userId, User user, IEnumerable<string> roles)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId) || user == null)
                    return IdentityResult<string>.Failure("Invalid user data");

                var claims = new List<Claim>
                {
                    new Claim("userId", userId),
                    new Claim("userName", user.UserName ?? ""),
                    new Claim("email", user.Email ?? "")
                };

                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim("roles", role));
                    }
                }

                var keyBytes = Encoding.UTF8.GetBytes(_secretKey);
                if (keyBytes.Length < 32) // Validate key length for HMACSHA256
                    return IdentityResult<string>.Failure("Secret key is too short for secure signing");

                var key = new SymmetricSecurityKey(keyBytes);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                if (string.IsNullOrWhiteSpace(tokenString))
                    return IdentityResult<string>.Failure("Token generation failed");

                return IdentityResult<string>.Success(tokenString);
            }
            catch (Exception ex)
            {
                return IdentityResult<string>.Failure($"Token generation error: {ex.Message}");
            }
        }
    }
}
