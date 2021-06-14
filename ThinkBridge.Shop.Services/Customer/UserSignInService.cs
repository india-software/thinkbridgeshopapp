using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ThinkBridge.Shop.Core.Customer;

namespace ThinkBridge.Shop.Services.Customer
{
    public class UserSignInService : SignInManager<ThinkBridgeUser>
    {
        public UserSignInService(UserManagerService userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ThinkBridgeUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<UserSignInService> logger, IAuthenticationSchemeProvider schemes,IUserConfirmation<ThinkBridgeUser> userConfirmation) : base(userManager,
            contextAccessor, claimsFactory, optionsAccessor, logger, schemes,userConfirmation)
        {
        }
    }
}
