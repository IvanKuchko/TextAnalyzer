using TextAnalyzer.Server.Models;

namespace TextAnalyzer.Server.Services.RoleService
{
    public interface IRoleService
    {
        void AddRole(string roleName);
        Role GetUserRole(User user);
        void SetUserRole(User user, string roleName);
    }
}