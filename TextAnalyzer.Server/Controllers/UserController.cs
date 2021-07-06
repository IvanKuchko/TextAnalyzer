using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextAnalyzer.Server.Models.Responses;
using TextAnalyzer.Server.Services.UserService;

namespace TextAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(nameof(GetUsers))]
        [Authorize]
        public ActionResult<UsersResponse> GetUsers()
        {
            var message = Messages.Success;
            var users = _userService.GetUsers();
            return new UsersResponse()
            {
                Message = message,
                Users = users
            };
        }
    }
}
