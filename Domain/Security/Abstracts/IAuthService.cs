using System.Threading.Tasks;

namespace Domain.Security
{
    public interface IAuthService
    {
        Task<AuthService.AuthenicationResult> AuthenticateAsync(string userName, string password);
    }
}
