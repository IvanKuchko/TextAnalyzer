using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using TextAnalyzer.Server.DataAccess;
using TextAnalyzer.Server.Models;
using TextAnalyzer.Server.Models.Database;

namespace TextAnalyzer.Server.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _context;

        public TokenService(ApplicationDbContext context) => _context = context;

        public bool AccessVerefy(string accessToken)
        {
            var token = _context.UsersTokens.FirstOrDefault(t => t.AccessToken == accessToken && t.IsActive);
            if (token != null)
            {
                if (DateTime.Now < token.CreationDate.AddMinutes(AuthOptions.LIFETIME))
                {
                    return true;
                }
            }
            return false;
        }

        public Tokens GenerateTokens(User user)
        {
            var userId = Guid.Parse(user.Id);
            return generateTokens(userId);
        }

        public Tokens RefreshTokens(string refreshToken)
        {
            var token = _context.UsersTokens.FirstOrDefault(t => t.RefreshToken == refreshToken && t.IsActive);
            if (token != null)
            {
                token.IsActive = false;
                _context.UsersTokens.Update(token);
                _context.SaveChanges(); 
                return generateTokens(token.User.Id);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private Tokens generateTokens(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId && u.IsActive);
            var role = _context.UsersRoles.Include(r  => r.Role).FirstOrDefault(r => r.User.Id == userId);
            foreach (var item in _context.UsersRoles)
            {
                var _role = item.Role.Name;
            }

            var now = DateTime.Now;
            var claims = new List<Claim>
            {
                new Claim("Sub", user.Id.ToString()),
                new Claim("Name", $"{user.FirstName} {user.LastName}"),
                new Claim("Email", user.Email),
                new Claim("Role", role?.Role?.Name)
            };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = Guid.NewGuid().ToString();
            _context.UsersTokens.Add(new UserTokensDto
            {
                Id = Guid.NewGuid(),
                User = user,
                AccessToken = encodedJwt,
                ExpireIn = AuthOptions.LIFETIME,
                RefreshToken = refreshToken,
                CreationDate = now,
                IsActive = true
            });
            _context.SaveChanges();
            return new Tokens()
            {
                AccessToken = encodedJwt,
                ExpireIn = AuthOptions.LIFETIME,
                RefreshToken = refreshToken
            };
        }
    }
}
