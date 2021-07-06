using System;
using System.Linq;
using TextAnalyzer.Server.DataAccess;
using TextAnalyzer.Server.Models;
using TextAnalyzer.Server.Models.Database;

namespace TextAnalyzer.Server.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context) => _context = context;

        public void AddRole(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(r => r.NormalizedName == roleName.ToUpper());
            if (role != null)
            {
                if (!role.IsActive)
                {
                    role.IsActive = true;
                    _context.Roles.Update(role);
                    _context.SaveChanges();
                }
            }
            else
            {
                var guid = Guid.NewGuid();
                _context.Roles.Add(new RoleDto()
                {
                    Id = guid,
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                    CreationDate = DateTime.Now,
                    IsActive = true
                });
                _context.SaveChanges();
            }
        }

        public Role GetUserRole(User user)
        {
            var userDto = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) && u.IsActive);
            var role = _context.UsersRoles.FirstOrDefault(r => r.User.Id == userDto.Id && r.IsActive);
            return new Role()
            {
                Name = role.Role.Name
            };
        }

        public void SetUserRole(User user, string roleName)
        {
            AddRole(roleName);
            var userDto = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) && u.IsActive);
            var userRole = _context.UsersRoles.FirstOrDefault(r => r.User.Id == userDto.Id && r.IsActive);
            if (userRole != null)
            {
                userRole.IsActive = false;
                _context.UsersRoles.Update(userRole);
                _context.SaveChanges();
            }
            var guid = Guid.NewGuid(); 
            var role = _context.Roles.FirstOrDefault(r => r.NormalizedName == roleName.ToUpper() && r.IsActive);
            _context.UsersRoles.Add(new UserRoleDto() 
            {
                Id = guid,
                User = userDto,
                Role = role,
                CreationDate = DateTime.Now,
                IsActive = true
            });
            _context.SaveChanges();
        }
    }
}
