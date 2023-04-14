// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using PERM_TASKS.Models.ApplicationUsers;
using PERM_TASKS.Data;
using Microsoft.EntityFrameworkCore;
using PERM_TASKS.Models.Menus;
using PERM_TASKS.Models.RolesPermission;

namespace PERM_TASKS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email / Username")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userName = Input.Email;
                if (IsValidEmail(Input.Email))
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        userName = user.UserName;
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    // implement role permission
                    var menus = await _context.Menus.Include(x => x.ParentMenu).ToListAsync();

                    await GenerateMenu(menus, userName);

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private async Task GenerateMenu(List<MenuModel> menus, string userName)
        {
            // fetch user
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            // fetch user roles
            var userRoles = await _context.UserRoles.Where(s => s.UserId == user.Id).ToListAsync();

            // fetch role permission
            foreach (IdentityUserRole<string> userRole in userRoles)
            {
                List<RolesPermissionsModel> rolePermissions = _context.RolesPermissions.Where(s => s.RoleId == userRole.RoleId).ToList();

                foreach (RolesPermissionsModel permission in rolePermissions)
                {
                    // parent menus
                    MenuModel parentMenu = menus.FirstOrDefault(s => s.ParentMenuId == null && menus.Count(c => c.ParentMenuId == s.MenuId) > 0 && s.MenuName == permission.MenuName);

                    if (parentMenu != null)
                    {
                        List<MenuModel> childMenu = menus.Where(s => s.ParentMenuId == parentMenu.MenuId).ToList();
                        parentMenu.ChildMenu = childMenu;

                        if (!GlobalApplicationData.Menus.Any(s => s.MenuName == parentMenu.MenuName))
                            GlobalApplicationData.Menus.Add(parentMenu);
                    }
                }

                foreach (RolesPermissionsModel permission in rolePermissions)
                {
                    // flat menu
                    foreach (MenuModel flatMenu in menus.Where(s => s.ParentMenu is null && s.MenuName == permission.MenuName).ToList())
                    {
                        if (!GlobalApplicationData.Menus.Any(s => s.MenuName == flatMenu.MenuName))
                            GlobalApplicationData.Menus.Add(flatMenu);
                    }
                }
            }
        }

        private MenuModel GenerateChildMenu(MenuModel menu, List<MenuModel> allMenus)
        {
            var childMenus = allMenus.Where(s => s.ParentMenuId == menu.MenuId).ToList();
            menu.ChildMenu = childMenus;

            if (menu.ChildMenu is null or { Count: 0 }) return menu;

            return GenerateChildMenu(menu, allMenus);
        }
    }
}
