using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjectBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public string GetControllerName()
        {
            return ControllerContext.ActionDescriptor.ControllerName;
        }

        [NonAction]
        public ClaimsPrincipal GetCurrentUser()
        {
            return HttpContext.User;
        }

        [NonAction]
        public string? GetCurrentUsername()
        {
            return GetCurrentUser()?.Identity?.IsAuthenticated == true ? GetCurrentUserNameFromClaim() : null;
        }

        [NonAction]
        public string? GetCurrentUserRole()
        {
            return HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        }

        [NonAction]
        public string GetIpAddress()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }

        [NonAction]
        public void SetCookie(Dictionary<string, string> cookieValues)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                //Expires = refreshToken.Expiry,
                Expires = DateTime.UtcNow.AddMinutes(30), // temp
                Secure = true,
                SameSite = SameSiteMode.None
            };

            foreach (var cookie in cookieValues)
            {
                HttpContext.Response.Cookies.Append(cookie.Key, cookie.Value, cookieOptions);
            }
        }

        [NonAction]
        private string? GetCurrentUserNameFromClaim()
        {
            return GetCurrentUser()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [NonAction]
        public string GetRequestScheme()
        {
            return HttpContext.Request.Scheme;
        }
    }
}
