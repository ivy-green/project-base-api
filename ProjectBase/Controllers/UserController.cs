using Microsoft.AspNetCore.Mvc;
using ProjectBase.Application.Services.UserService;

namespace ProjectBase.Controllers
{
    public class UserController : BaseController
    {
        IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("file")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            await _userService.UploadProfileImage(file, "thaomy310702@gmail.com"); // fix
            return Ok();
        }
    }
}
