using Identity.Core.Models.Token;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}
