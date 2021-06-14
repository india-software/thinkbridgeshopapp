using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ThinkBridge.Shop.Core.Domain;
using ThinkBridge.Shop.Data.Mapping;

namespace ThinkBridge.Shop.Data.Customer
{    
    public class ThinkBridgeUser : IdentityUser<int>
    {
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
       
    }
    public class ThinkBridgeUserRoleMapping : IdentityUserRole<int>
    {
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
    
    public class ThinkBridgeUserClaim : IdentityUserClaim<int>
    {
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
    public class ThinkBridgeUserToken : IdentityUserToken<int>
    {
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }

    
    public class ThinkBridgeUserRoleClaim : IdentityRoleClaim<int>
    {
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }

    public class ThinkBridgeUserLogin : IdentityUserLogin<int>
    {
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }

    public class ThinkBridgeUserRole : IdentityRole<int>
    {
        public ThinkBridgeUserRole() { }
        public ThinkBridgeUserRole(string name) { Name = name; }
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
    public static class UserRoles
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }
}
