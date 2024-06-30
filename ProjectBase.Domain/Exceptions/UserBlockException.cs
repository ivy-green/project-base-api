using System.Net;

namespace ProjectBase.Domain.Exceptions
{
    public class UserBlockException : AppException
    {
        public static readonly HttpStatusCode Status = HttpStatusCode.BadRequest;
        public static readonly string Code = "USER_BLOCK";
        public static readonly string Message = "User has been blocked";
        public UserBlockException() : base(Status, Code, Message)
        {
        }
    }
}
