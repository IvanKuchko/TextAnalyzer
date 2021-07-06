using Microsoft.EntityFrameworkCore;
using TextAnalyzer.Server.Models.Database;

namespace TextAnalyzer.Server.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<RoleDto> Roles { get; set; }
        public DbSet<UserRoleDto> UsersRoles { get; set; }
        public DbSet<UserTokensDto> UsersTokens { get; set; }
        public DbSet<AvatarDto> Avatars { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
