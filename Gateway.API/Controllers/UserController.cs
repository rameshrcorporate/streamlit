using Gateway.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GRO.SharedLibrary.ViewModel.Common;

namespace Gateway.API.Controllers
{
    [Route("api/iam/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginInfo loginInfo)
        {
            if (loginInfo != null)
            {
                var result = userService.Authenticate(loginInfo);
                if (result == null || result.UserId == 0) return Ok(new { SuccessCode = "Failure", message = "Username or password is incorrect" });
                {
                    return Ok(new { SuccessCode = "Success", message = result.Token });
                }
            }
            return Ok(new { SuccessCode = "Failure", message = "Username or password is incorrect" });
        }
    }
}
