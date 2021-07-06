namespace TextAnalyzer.Server.Models.Database
{
    public class UserRoleDto : BaseDto
    {
        public UserDto User { get; set; }
        public RoleDto Role { get; set; }
    }
}
