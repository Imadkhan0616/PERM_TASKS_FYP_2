using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PERM.Models.Attendance;
using PERM.Models.Department;
using PERM.Models.EmployeeMasterData;
using PERM.Models.Task;
using PERM_TASKS.Models.ApplicationUsers;
using PERM_TASKS.Models.Common;
using PERM_TASKS.Models.Menus;
using PERM_TASKS.Models.RolesPermission;

namespace PERM_TASKS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<IdentityUserLogin<string>>().HasKey(e => e.UserId);
            //builder.Entity<IdentityUserRole<string>>().HasKey(e => e.RoleId);
            //builder.Entity<IdentityUserToken<string>>().HasKey(e => e.UserId);

            

            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<RolesPermissionsModel>(entity =>
            {
                entity.ToTable("RolePermissions");
            });

            List<IMutableEntityType> entityTypes = builder.Model.GetEntityTypes().ToList();

            foreach (var entityType in entityTypes
                .Where(entity => entity.ClrType.IsSubclassOf(typeof(ModelBase)))
                .SelectMany(entity => entity.GetForeignKeys()))
            {
                entityType.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
        public DbSet<EmployeeMasterDataModel> employeeMasterData { get; set; }
        
        public DbSet<_TasksModel> _tasks { get; set; }
        public DbSet<EmployeeMarkAttendanceModel> MarkAttendance { get; set; } = default!;
        public DbSet<EmployeeAttendanceAssistantModel> EmployeeAttendanceAssistant { get; set; } = default!;
        public DbSet<EmployeeAttendanceRequestModel> AttendanceRequest { get; set; } = default!;
        public DbSet<AutomateAttendanceMarkingModel> AutomateAttendanceMarking { get; set; } = default!;
        public DbSet<DepartmentModel> Departments { get; set; } = default!;
        public DbSet<RolesPermissionsModel> RolesPermissions  { get; set; } = default!;

        public DbSet<MenuModel> Menus { get; set; } = default!;
    }
}