using System.Collections.Generic;
using System.Security.Claims;
using ThinkBridge.Shop.Core.Customer;

namespace ThinkBridge.Shop.Services.Customer
{
    public interface ITokenService
    {
        ThinkBridgeToken GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}