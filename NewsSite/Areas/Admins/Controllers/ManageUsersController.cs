using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using newsSite.Areas.Identity.Data;
using newsSite.Models.ViewModels;

namespace newsSite.Areas.Admins.Controllers
{
    //[Authorize(Roles ="admins")]
    [Authorize("AdminPolicy")]
    [Area("Admins")]
    [Route("admins/[controller]/[action]")]
    public class ManageUsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IOptions<SitePropertiesViewModel> options;

        public ManageUsersController(SignInManager<ApplicationUser> _signInManager,
                              UserManager<ApplicationUser> _usermanager,
                              RoleManager<IdentityRole> _rolemanager,
                              IOptions<SitePropertiesViewModel> _options)
        {
            signInManager = _signInManager;
            userManager = _usermanager;
            roleManager = _rolemanager;
            options = _options;
        }

        public IActionResult Index()
        {
            ViewData["defaultAdminUserName"] = options.Value.AdminInfo.adminusername; 
            return View();
        }
        public async Task<IActionResult> ChangeUserInRolesAsync(string userid, string rolename, bool status)
        {
            var user = await userManager.FindByIdAsync(userid);

            try
            {
                if (status)
                {
                    await userManager.AddToRoleAsync(user, rolename);
                }
                else
                {
                    if (user == await userManager.FindByNameAsync( //deafult site admin cannot be demoted
                        options.Value.AdminInfo.adminusername
                        ) && rolename == "admins")
                    {
                        return Json(false);
                    }
                    else
                    {
                        await userManager.RemoveFromRoleAsync(user, rolename);
                    }
                }
                return Json(true);

            }
            catch (Exception)
            {
                return Json(false);
            }
        }

    }
}
// add bloger role
// make a policy foreach role
