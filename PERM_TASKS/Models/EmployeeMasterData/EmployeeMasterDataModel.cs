using PERM.Models.Department;
using PERM.Models.Employee_Master_Data;
using PERM.Models.Validation;
using PERM_TASKS.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models.EmployeeMasterData
{
    [Table("EmployeeMasterData")]
    public class EmployeeMasterDataModel : ModelBase
    {
        //Master Data

        [Key]
        public string EmployeeID { get; set; }

        //[Required]
        [DisplayName("First Name")]
        public string EmployeeFirstName { get; set; }

        
        [DisplayName("Middle Name")]
        public string? EmployeeMiddleName { get; set; }

        //[Required]
        [DisplayName("Last Name")]
        public string? EmployeeLastName { get; set; }

        //[Required]
        [DisplayName("Status")]
        public bool EmployeeStatus { get; set; }

        //[Required]
        [DisplayName("Gender")]
        public string? EmployeeGender { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime? EmployeeDob { get; set; }

        
        [DisplayName("Date of Joining")]
        public DateTime? EmployeeDoj { get; set; }

        
        [DisplayName("Employeement Type")]
        public string EmployeementType { get; set; }

        //----------------------------
        //Emergency Contact Details

        
        [DisplayName("Emergency Contact Name")]
        public string? EmergencyPhoneNo { get; set; }

        
        [DisplayName("Relation")]
        public string? EmployeeRelationToEmergence { get; set; }

        //-----------------------------

        //Joining Details

       
        [DisplayName("Job Applicant")]
        public string? JobApplicant { get; set; }

       
        [DisplayName("Contract End Date")]
        public DateTime? ContractEndDate { get; set; }

       
        [DisplayName("Offer Date")]
        public DateTime? OfferDate { get; set; }

       
        [DisplayName("Notice Days")]
        public int? NoticeDays { get; set; }

       
        [DisplayName("Confirmation Date")]
        public DateTime? ConfirmationDate { get; set; }

        
        [DisplayName("Date of Retirement")]
        public DateTime? DateOfRetirement { get; set; }

        //--------------------------------

        //Department & Grade
        
        [DisplayName("Department")]
        public string? DepartmentID { get; set; }

        //[Required]
        [DisplayName("Grade")]
        public string? Grade { get; set; }

        //[Required]
        [DisplayName("Designation")]
        public string? Designation { get; set; }

       
        [DisplayName("Branch")]
        public string? Branch { get; set; }

        //[Required]
        [DisplayName("Report To")]
        [ForeignKey(nameof(ReportTo))]

        public string? ReportToID { get; set; }

        //Attendance & Leave Details
        
        [DisplayName("Attendance Device ID")]
        public string? AttendanceDeviceId { get; set; }

        
        [DisplayName("Holidays")]
        public string? Holidays { get; set; }

        
        [DisplayName("Default Shift")]
        public string? DefualtShift { get; set; }
        //-----------------------------

        //Salary Details

        
        [DisplayName("Salary Mode")]
        public string? SalaryMode { get; set; }

        
        [DisplayName("Bank Name")]
        public string? BankName { get; set; }

        ////[Required]
        [DisplayName("Bank A/C Number")]
        public string? BankAccNumber { get; set; }

        
        [DisplayName("Payroll Cost Center")]
        public string? PayrollCostCenter { get; set; }

        
        [DisplayName("Provident Fund Account")]
        public string? ProvidentFundAccount { get; set; }

        //---------------------------------

        //Health Insurrance

        
        [DisplayName("Health Insurrance Provider")]
        public string? HealthInsurranceProvider { get; set; }

        
        [DisplayName("Health Insurrance Number")]
        public string? HealthInsurranceNumber { get; set; }

        //----------------------------------

        //Contact Details

        ////[Required]
        [DisplayName("Mobile")]
        public string? MobileNumber { get; set; }

        
        [DisplayName("Preferred Email")]

        public string? PreferredEmail { get; set; }

       
        [DisplayName("Personal Email")]
        public string? PersonalEnmail { get; set; }

       
        [DisplayName("Company Email")]
        public string? CompanyEmail { get; set; }

        [DisplayName("Permanent Address")]
        public string? PermanentAddress { get; set; }

        //[Required]
        [DisplayName("Current Address")]
        public string? CurrentAddress { get; set; }

        //----------------------------------

        //Personal Bio
        [DisplayName("Personal Bio")]
        public string? PersonalBio { get; set; }


        //----------------------

        //Personal Details

        ////[Required]
        [DisplayName("Martial Status")]
        public string? MatrialStatus { get; set; }

        [DisplayName("Blood Group")]
        public string? BloodGroup { get; set; }

        [DisplayName("Family Background")]
        public string? FamilyBackground { get; set; }

        [DisplayName("Health Details")]

        public string? HealthDetails { get; set; }

        ////[Required]
        [DisplayName("CNIC")]
        [StringLength(13, MinimumLength = 13)]
        public string? CNIC { get; set; }

        [DisplayName("Date of Issue")]
        public DateTime? DateOfIssue { get; set; }

        [DisplayName("Valid Upto")]
        [DateRange(nameof(DateOfIssue))]
        public DateTime? ValidUpto { get; set; }

        //----------------------

        //Educational Qualification


        [DisplayName("School/University")]
        public string? School_University { get; set; }

        [DisplayName("Qualification/Program")]
        public string? Qualification_Program { get; set; }

        [DisplayName("Level")]
        public string? Level { get; set; }

        [DisplayName("Year of Passing")]
        public DateTime? YearOfPassing { get; set; }

        //Previous Work Experience

        [DisplayName("Company")]
        public string? PreviousCompany { get; set; }

        [DisplayName("Designation")]
        public string? PreviousDesignation { get; set; }

        [DisplayName("Salary")]
        [Range(10000,10000000)]
        public int? PreviousSalary { get; set; }

        [DisplayName("Address")]
        public string? Address { get; set; }

        //-----------------------

        //History In Company
        [DisplayName("Branch")]
        public string? HIC_Branch { get; set; }

        ////[Required]
        //[DisplayName("HIC Department")]
        //public string HIC_DepartmentID { get; }

        [DisplayName("Start Date")]
        
        public DateTime? HIC_FromDate { get; set; }

        [DisplayName("End Date")]
        [DateRange(nameof(HIC_FromDate))]
        public DateTime? HIC_ToDate { get; set; }
        //-----------------------

        //Employee Grade

        [DisplayName("Salary Structure(Default)")]
        [Range(10000, 10000000)]

        public string? EmployeeSalary { get; set; }

        [DisplayName("Name")]

        public string? EmployeeNameGrade { get; set; }



        //----------------------

        //Designation & Skills

        [DisplayName("Designation ")]
        public string? EmployeeDesignation { get; set; }

        [DisplayName("Required Skills")]
        public string? RequiredSkills { get; set; }

        public string? Description { get; set; }
        //----------------------




        //---------------------

        //Exit

        [DisplayName("Resignation Letter Date")]
        [DateRange(nameof(EmployeeDoj))]

        public DateTime? ResignationLetterDate { get; set; }

        
        [DisplayName("Exit in to Date")]
        public DateTime? ExitInTo { get; set; }

        
        [DisplayName("Relieving Date")]
        public DateTime? RelievingDate { get; set; }

        
        [DisplayName("Feedback")]
        public string? Feedback { get; set; }

        
        [DisplayName("New Workplace")]
        public string? NewWorkplace { get; set; }

        
        [DisplayName("Reason For Leaving")]
        public string? ReasonForLeaving { get; set; }
        //---------------------

        public virtual EmployeeMasterDataModel ReportTo { get; set; }
        public virtual DepartmentModel Department { get; set; }
        //public DepartmentModel HIC_Department { get; set; }

        [NotMapped]
        public string FullName { get { return string.IsNullOrEmpty(EmployeeFirstName) ? "---SELECT---" : $"{EmployeeFirstName} {EmployeeMiddleName} {EmployeeLastName}"; } set { } }
    }
}
