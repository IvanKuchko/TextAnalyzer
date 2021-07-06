using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TextAnalyzer.Server.DataAccess;
using TextAnalyzer.Server.Helpers;
using TextAnalyzer.Server.Models;
using TextAnalyzer.Server.Models.Database;

namespace TextAnalyzer.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context) => _context = context;

        public User AddUser(string login, string email, string password, string firstName, string lastName)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Login == login || u.Email == email);
            if (existUser != null)
                throw new UserExistException(Messages.UserExist);
            var guid = Guid.NewGuid();
            _context.Users.Add(new UserDto()
            {
                Id = guid,
                Login = login,
                NormalizedLogin = login.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                PasswordHash = PasswordHash.Sha256(password),
                FirstName = firstName,
                LastName = lastName,
                CreationDate = DateTime.Now,
                IsActive = true
            });
            _context.SaveChanges();
            var user = _context.Users.FirstOrDefault(u => u.Id == guid && u.IsActive);
            var role = _context.UsersRoles.Include(r => r.Role).FirstOrDefault(r => r.User.Id == user.Id && r.IsActive);
            return new User()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = role?.Role?.Name
            };
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();
            var usersDto = _context.Users.Where(u => u.IsActive).ToList();
            foreach (var user in usersDto)
            {
                var role = _context.UsersRoles.Include(r => r.Role).FirstOrDefault(r => r.User.Id == user.Id && r.IsActive);
                users.Add(new User()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = role?.Role?.Name
                });
            }
            return users;
        }

        public User GetUserById(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId) && u.IsActive);
            if (user == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var role = _context.UsersRoles.Include(r => r.Role).FirstOrDefault(r => r.User.Id == user.Id && r.IsActive);
            return new User()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = role?.Role?.Name
            };
        }

        public User PasswordVerify(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => 
                u.Login == login && 
                u.PasswordHash == PasswordHash.Sha256(password) && 
                u.IsActive); 
            if (user == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var role = _context.UsersRoles.Include(r => r.Role).FirstOrDefault(r => r.User.Id == user.Id && r.IsActive);
            return new User()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = role?.Role?.Name
            };
        }
    }
}
