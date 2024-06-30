using Microsoft.AspNetCore.Mvc;
using ProjectBase.Application.DTOs.Requests;
using ProjectBase.Application.Services.AuthService;

namespace ProjectBase.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequestDTO data) 
        {
            var res = await _authService.Login(data, GetIpAddress());
            return Ok(res);
        }
    }
}
