using PERM_TASKS.Models.Menus;
using System.Security.Claims;

namespace PERM_TASKS
{
    public static class ExtensionMethods
    {
        public static List<MenuModel> GetUserMenu(this ClaimsPrincipal user)
        {
            if (user.Identity?.IsAuthenticated is null or false)
            {
                return new List<MenuModel>();
            }
            return GlobalApplicationData.Menus;
        }

        public static void ClearUserMenu(this ClaimsPrincipal user)
        {
            GlobalApplicationData.Menus = new();
        }
    }
}
