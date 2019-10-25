using System;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using newsSite.Areas.Identity.Data;
using newsSite.Models.ViewModels;

[assembly: HostingStartup(typeof(newsSite.Areas.Identity.IdentityHostingStartup))]
namespace newsSite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DBNews>(options =>
                options.UseSqlServer(
                    context.Configuration.GetConnectionString("DBNewsConnection")));

                //if (!context.Configuration.GetConnectionString("DBNewsConnection").StartsWith("Encrypted"))
                //{
                //    //Encrypt it 
                //    ConfigurationManager.ConnectionStrings["DBNewsConnection"].ConnectionString =
                //    DBConnectionStringEncryptor.Encrypt(context.Configuration.GetConnectionString("DBNewsConnection"));
                //}
                //services.AddDbContext<DBNews>(options =>
                //options.UseSqlServer(
                //    DBConnectionStringEncryptor.Decrypt(context.Configuration.GetConnectionString("DBNewsConnection"))));

                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                });

                services.AddIdentity<ApplicationUser, IdentityRole>().AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DBNews>();
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Account/Login";
                    options.LogoutPath = $"/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });

                services.AddAuthorization(x =>
                {
                    x.AddPolicy("AdminPolicy", y => y.RequireRole("admins"));
                    x.AddPolicy("BloggersPolicy", y => y.RequireRole("bloggers"));
                });
            });
        }
    }
}