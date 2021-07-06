using System.Collections.Generic;
using TextAnalyzer.Server.Models;

namespace TextAnalyzer.Server.Services.UserService
{
    public interface IUserService
    {
        public User AddUser(string login, string email, string password, string firstName, string lastName);
        IEnumerable<User> GetUsers();
        User GetUserById(string userId);
        User PasswordVerify(string login, string password);
    }
}