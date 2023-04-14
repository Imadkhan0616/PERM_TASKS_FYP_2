using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PERM_TASKS.Data;
using PERM_TASKS.Models.Menus;
using PERM_TASKS.Models.RolesPermission;

namespace PERM_TASKS.Controllers.RolesManager
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbcontext;

        public RoleManagerController(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbcontext)
        {
            _roleManager = roleManager;
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string roleId)
        {
            ViewBag.roleId = roleId;
            ViewBag.RoleName = (await _roleManager.FindByIdAsync(roleId))?.Name ?? "N/A";
            var Permissionroles =  await  _dbcontext.RolesPermissions.Where(x => x.RoleId == roleId).ToListAsync();
            if(Permissionroles == null) 
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<RolesPermissionViewModel>();
            var menus = await _dbcontext.Menus.ToListAsync();

            foreach (MenuModel menu in menus)
            {
                var rolesViewModel = new RolesPermissionViewModel
                {
                    RoleId = roleId,
                    MenuName = menu.MenuName
                };

                var DbPermissions = Permissionroles.FirstOrDefault(x => x.MenuName == menu.MenuName);
                if (DbPermissions == null)
                {
                    rolesViewModel.Selected = false;
                }
                else
                {
                    rolesViewModel.Selected = true;
                }
                model.Add(rolesViewModel);
            }

            return View(model);    
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<RolesPermissionsModel> requestModel,string roleId)
        {
            var DbPermissionroles = await _dbcontext.RolesPermissions.Where(x => x.RoleId == roleId).ToListAsync();
            if(DbPermissionroles == null)
            {
                return NotFound("Not found");
            }

            if(DbPermissionroles.Count == 0)
            {
                foreach (RolesPermissionsModel? role in requestModel)
                {
                    if (!role.Selected)
                        continue;

                    role.RolePermissionsId = Guid.NewGuid().ToString();
                    role.RoleId = roleId;
                    await _dbcontext.RolesPermissions.AddAsync(role);
                }
            }
            else
            { 

                foreach (RolesPermissionsModel? role in DbPermissionroles) {
                    var DbPermissions = DbPermissionroles.FirstOrDefault(x=> x.MenuName == role.MenuName);
                    if (DbPermissions == null)
                    {
                        role.RolePermissionsId = Guid.NewGuid().ToString();
                       await _dbcontext.RolesPermissions.AddAsync(role);
                    
                    }
                }
            }
            await _dbcontext.SaveChangesAsync();
               return RedirectToAction("Index");

        }
    }
}
