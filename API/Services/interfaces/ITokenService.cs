using System.Security.Claims;

namespace API.Services.interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims);
    }
}
