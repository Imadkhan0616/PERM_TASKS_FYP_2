namespace PERM.Models.Employee_Master_Data
{
    public class EmployeementTypeList
    {
        public static List<EmployementType> Types { get; set; } = new List<EmployementType>()
        {
            new EmployementType { ID = "1", Name = "---SELECT---" },
            new EmployementType { ID = "2", Name = "FullTime" },
            new EmployementType { ID = "3", Name = "PartTime" },
            new EmployementType { ID = "4", Name = "Contract" },
            new EmployementType { ID = "5", Name = "Internship" },
            new EmployementType { ID = "6", Name = "Trainee" }
        };        
    }

    public class EmployementType
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }
    }