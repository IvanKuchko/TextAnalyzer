using Microsoft.AspNetCore.Mvc;
using TextAnalyzer.Server.Models;
using TextAnalyzer.Server.Models.Requests;
using TextAnalyzer.Server.Models.Responses;
using TextAnalyzer.Server.Services.RoleService;
using TextAnalyzer.Server.Services.TokenService;
using TextAnalyzer.Server.Services.UserService;

namespace TextAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private ITokenService _tokenService;

        public AccountController(
            IUserService userService, 
            IRoleService roleService, 
            ITokenService tokenService)
        {
           _userService = userService;
           _roleService = roleService;
           _tokenService = tokenService;
        }

        [HttpPost]
        [Route(nameof(SignIn))]
        public ActionResult<TokenResponse> SignIn([FromBody] SignInRequest model)
        {
            var message = string.Empty;
            var tokens = new Tokens();
            var user = _userService.PasswordVerify(model.Login, model.Password);
            if (user != null)
            {
                message = Messages.AuthSuccess;
                tokens = _tokenService.GenerateTokens(user);
            }
            else
            {
                message = Messages.AuthError;
            }
            return new TokenResponse()
            {
                Message = message,
                Tokens = tokens,
            };
        }

        [HttpPost]
        [Route(nameof(SignUp))]
        public ActionResult<TokenResponse> SignUp([FromBody] SignUpRequest model)
        {
            var message = string.Empty;
            var tokens = new Tokens();
            try
            {
                var user = _userService.AddUser(
                    model.Login,
                    model.Email,
                    model.Password,
                    model.FirstName,
                    model.LastName);
                if (user != null)
                {
                    _roleService.SetUserRole(user, "User");
                    message = Messages.RegistrationSuccess;
                    tokens = _tokenService.GenerateTokens(user);
                }
                else
                {
                    message = Messages.RegistrationError;
                }
            }
            catch(UserExistException e)
            {
                message = e.Message;
            }
            return new TokenResponse()
            {
                Message = message,
                Tokens = tokens,
            };
        }

        [HttpPost]
        [Route(nameof(SignOut))]
        public ActionResult<BaseResponse> SignOut([FromBody] SignUpRequest model)
        {
            return new TokenResponse();
        }
    }
}
