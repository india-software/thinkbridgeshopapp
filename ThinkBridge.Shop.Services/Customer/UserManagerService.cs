using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using ThinkBridge.Shop.Core.Customer;
using ThinkBridge.Shop.Data.Store;

namespace ThinkBridge.Shop.Services
{
    public class UserManagerService : UserManager<ThinkBridgeUser>
    {
        public UserManagerService(CustomUserStore userStore, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ThinkBridgeUser> passwordHasher,
        IEnumerable<IUserValidator<ThinkBridgeUser>> userValidators,
        IEnumerable<IPasswordValidator<ThinkBridgeUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManagerService> logger) :
        base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            services, logger)
        {
        }

    }
}
