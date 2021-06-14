using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ThinkBridge.Shop.Api.CatalogFactory;
using ThinkBridge.Shop.Api.Mapper;
using ThinkBridge.Shop.Core.Caching;
using ThinkBridge.Shop.Core.Customer;
using ThinkBridge.Shop.Data;
using ThinkBridge.Shop.Data.Store;
using ThinkBridge.Shop.Services;
using ThinkBridge.Shop.Services.Customer;
using ThinkBridge.Shop.Services.FileHelper;
using ThinkBridge.Shop.Services.Media;

namespace ThinkBridge.Shop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //dependency injection - Registering service
            services.AddScoped<IDbContext,ThinkBridgeDbContext>();
            DependencyRegisterar(services);
          
            //add Conection string 
            services.AddDbContext<ThinkBridgeDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ThinkBrigeConnectionString")));
            //For Identity
            services.AddIdentity<ThinkBridgeUser, ThinkBridgeUserRole>()
                .AddUserStore<CustomUserStore>().AddRoleStore<CustomUserRoleStore>()
                .AddUserManager<UserManagerService>()
                .AddRoleManager<RoleManagerService>()
                //.AddEntityFrameworkStores<ThinkBridgeDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddControllers();
            //Register Automappter Mapping
            AddAutoMapper(services);
            //Adding Authentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //adding jwt bearer
            .AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience=Configuration["JWT:ValidAudience"],
                    ValidIssuer= Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]))
                };
                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };

            });
            //for swagger
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (string.IsNullOrWhiteSpace(env.WebRootPath))
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload");

            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();               
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            

        }
        protected virtual void AddAutoMapper(IServiceCollection services)
        {
            //create AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(ThinkBridgeAutoMapper));
            });
            //register
            AutoMapperConfiguration.Init(config);
        }
        public void DependencyRegisterar(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<UserStore<ThinkBridgeUser, ThinkBridgeUserRole, ThinkBridgeDbContext, int, ThinkBridgeUserClaim, ThinkBridgeUserRoleMapping, ThinkBridgeUserLogin, ThinkBridgeUserToken, ThinkBridgeUserRoleClaim>, CustomUserStore>();
            services.AddScoped<UserManager<ThinkBridgeUser>, UserManagerService>();
            services.AddScoped<RoleStore<ThinkBridgeUserRole, ThinkBridgeDbContext, int, ThinkBridgeUserRoleMapping, ThinkBridgeUserRoleClaim>, CustomUserRoleStore>();
            services.AddScoped<RoleManager<ThinkBridgeUserRole>, RoleManagerService>();
            services.AddScoped<SignInManager<ThinkBridgeUser>, UserSignInService>();
            services.AddScoped<CustomUserStore>();
            services.AddScoped<CustomUserRoleStore>();
            services.AddScoped(typeof(IRepository<>), typeof(ThinkBridgeRepository<>));
            services.AddScoped<IFileHelperService, ThinkBridgeFileProvider>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductCatalogFactory, ProductCatalogFactory>();
            services.AddScoped<IManufacturerService, ManufacturerService>();           
        }
    }
}
