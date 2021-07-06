using System;

namespace TextAnalyzer.Server.Services.UserService
{
    public class UserExistException : Exception
    {
        public UserExistException(string message) 
            : base(message)
        { 
        }
    }
}
