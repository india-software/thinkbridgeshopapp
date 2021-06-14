
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Customer;

namespace ThinkBridge.Shop.Data.Store
{
    public class CustomUserStore : UserStore<ThinkBridgeUser, ThinkBridgeUserRole, ThinkBridgeDbContext, int,ThinkBridgeUserClaim,ThinkBridgeUserRoleMapping,ThinkBridgeUserLogin,ThinkBridgeUserToken,ThinkBridgeUserRoleClaim>
    {
        public CustomUserStore(ThinkBridgeDbContext context) : base(context)
        {
        }
    }

    public class CustomUserRoleStore : RoleStore<ThinkBridgeUserRole, ThinkBridgeDbContext, int,ThinkBridgeUserRoleMapping,ThinkBridgeUserRoleClaim>
    {
        public CustomUserRoleStore(ThinkBridgeDbContext context):base(context)
        {
        }
    }
}
