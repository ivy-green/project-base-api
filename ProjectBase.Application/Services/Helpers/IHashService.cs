namespace ProjectBase.Application.Services.Helpers
{
    public interface IHashService
    {
        (string, string) CreatePasswordHashAndSalt(string password);
        bool VerifyPasswordHash(string salt, string passwordFromDb, string inputPassword);
    }
}