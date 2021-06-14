using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThinkBridge.Shop.Core.Customer;

namespace ThinkBridge.Shop.Data.Mapping.Customer
{
    public partial class UserMap : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUser>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUser> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUser));
            builder.HasKey(category => category.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).HasMaxLength(400).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(400);
            base.Configure(builder);
        }
        #endregion
    }
    public partial class UserRoleMappingMap : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUserRoleMapping>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUserRoleMapping> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUserRoleMapping));
            builder.HasKey(x => x.Id);
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
        #endregion
    }
    public partial class UserClaimMap : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUserClaim>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUserClaim> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUserClaim));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
        #endregion
    }
    public partial class UserTokenMap : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUserToken>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUserToken> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUserToken));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
        #endregion
    }
    public partial class UserRoleClaim : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUserRoleClaim>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUserRoleClaim> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUserRoleClaim));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
        #endregion
    }
    public partial class UserLoginClaim : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUserLogin>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUserLogin> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUserLogin));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
        #endregion
    }
    public partial class UserRoleMap : ThinkBridgeIdentityEntityConfiguration<ThinkBridgeUserRole>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ThinkBridgeUserRole> builder)
        {
            builder.ToTable(nameof(ThinkBridgeUserRole));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
        #endregion
    }



   
}
