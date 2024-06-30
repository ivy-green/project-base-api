namespace ProjectBase.Domain.Entities
{
    public class User
    {
        public string Username {  get; set; }
        public string PasswordHash { get; set; } = "";
        public string PasswordSalt { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Bio { get; set; } = "";
        public bool IsAccountBlock { get; set; } = false;
        public string? ResetPasswordToken { get; set; } = null;
        public DateTime? ResetPasswordTokenExpiredAt { get; set; } = null;
        public string VerifyToken { get; set; }
        public DateTime? TokenExpiredTime { get; set; }


        public bool IsEmailConfirmed { get; set; } = false;
        public bool IsAccountBlocked { get; set; } = false;

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
