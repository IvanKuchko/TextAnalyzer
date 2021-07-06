using System.Collections.Generic;
using System.Threading.Tasks;
using TextAnalyzer.Server.Models;

namespace TextAnalyzer.Server.Services.AnalyzerService
{
    public interface IAnalyzerService
    {
        void AddValue(User user, IEnumerable<Avatar> avatars);
        string CompareWithUsers(User user, IEnumerable<User> users);
        string CompareWithUser(User user, User userCompare);
<<<<<<< HEAD
        string CompareWithRandomUser(User user, IEnumerable<Avatar> avatars);
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
    }
}