namespace Szakdolgozat.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        int GetUserId();
        string GetUserEmail();
        Task<ServiceResponse<string>> Login(string email, string password);
    }
}
    