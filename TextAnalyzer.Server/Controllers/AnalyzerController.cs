using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TextAnalyzer.Server.Extensions;
using TextAnalyzer.Server.Models;
using TextAnalyzer.Server.Models.Requests;
using TextAnalyzer.Server.Models.Responses;
using TextAnalyzer.Server.Services.AnalyzerService;
using TextAnalyzer.Server.Services.UserService;

namespace TextAnalyzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyzerController : ControllerBase
    {
        private IUserService _userService;
        private IAnalyzerService _analyzerService;

        public AnalyzerController(
            IUserService userService, 
            IAnalyzerService analyzerService)
        {
            _userService = userService;
            _analyzerService = analyzerService;
        }

        [HttpPost]
        [Route(nameof(AddValue))]
        [Authorize]
        public ActionResult AddValue(IEnumerable<Avatar> avatars)
        {
            var user = _userService.GetUserById(User.Claims.GetUserId());
            _analyzerService.AddValue(user, avatars);
            return Ok();
        }

        [HttpGet]
        [Route(nameof(CompareWithUsers))]
        [Authorize]
        public ActionResult<CompareResponse> CompareWithUsers()
        {
            var user = _userService.GetUserById(User.Claims.GetUserId());
            var users = _userService.GetUsers();
            return new CompareResponse()
            {
                Message = _analyzerService.CompareWithUsers(user, users)
            };
        }

        [HttpPost]
        [Route(nameof(CompareWithUser))]
        [Authorize]
        public ActionResult<CompareResponse> CompareWithUser([FromBody] CompareWithUserRequest model)
        {
            var user = _userService.GetUserById(User.Claims.GetUserId());
            var userCompare = _userService.GetUserById(model.UserId);
            return new CompareResponse()
            {
                Message = _analyzerService.CompareWithUser(user, userCompare)
            };
        }
<<<<<<< HEAD
        [HttpPost]
        [Route(nameof(CompareWithRandomUser))]
        [Authorize]
        public ActionResult<CompareResponse> CompareWithRandomUser(IEnumerable<Avatar> avatars)
        {
            var user = _userService.GetUserById(User.Claims.GetUserId());
            return new CompareResponse()
            {
                Message = _analyzerService.CompareWithRandomUser(user, avatars)
            };
        }

=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
    }
}
