using PERM_TASKS.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM_TASKS.Models.Menus
{
    [Table("Menus",Schema = "Configuration")]
    public class MenuModel : ModelBase
    {
        [Key]
        public string MenuId { get; set; }
        public string MenuName { get; set; }

        [ForeignKey(nameof(ParentMenu))]
        public string? ParentMenuId { get; set; }

        public MenuModel ParentMenu { get; set; }

        [NotMapped]
        public List<MenuModel> ChildMenu { get; set; }

        public string? AreaName { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }

        public int? SortNo { get; set; }
    }
}
