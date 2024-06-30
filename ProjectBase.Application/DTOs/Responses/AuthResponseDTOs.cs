namespace ProjectBase.Application.DTOs.Responses
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<RoleResponseDTO> Roles { get; set; }
    }
    public class RoleResponseDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
