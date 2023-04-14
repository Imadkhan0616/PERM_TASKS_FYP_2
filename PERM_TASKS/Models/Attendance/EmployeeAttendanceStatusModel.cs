using PERM.Models.Employee_Master_Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models.Attendance
{
    public class EmployeeAttendanceStatusList
    {
        public static List<EmployementType> Types { get; set; } = new List<EmployementType>()
        {
            new EmployementType { ID = "1", Name = "---SELECT---" },
            new EmployementType { ID = "2", Name = "Present" },
            new EmployementType { ID = "3", Name = "Absent" },
            new EmployementType { ID = "4", Name = "OnLeave" }
        };
    }

    public class EmployeeAttendanceStatusModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
