using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using ThinkBridge.Shop.Core.Customer;
using ThinkBridge.Shop.Data.Store;

namespace ThinkBridge.Shop.Services
{
    public class RoleManagerService : RoleManager<ThinkBridgeUserRole>
    {
        public RoleManagerService(CustomUserRoleStore roleStore,
            IEnumerable<IRoleValidator<ThinkBridgeUserRole>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<RoleManagerService> logger) : base(roleStore,
            roleValidators,
            keyNormalizer, errors, logger)
        {
        }
    }
}
