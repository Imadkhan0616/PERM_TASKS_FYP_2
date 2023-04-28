using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Models.Employee_Master_Data;
using PERM.Models.EmployeeMasterData;
using PERM_TASKS.Data;
using PERM.Models.Department;
using PERM_TASKS.ViewModels;

namespace PERM_TASKS.Controllers.EmployeeMasterData
{
    public class EmployeeMasterDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        string[] lowerGrades = { "L1", "L2", "L3" };

        public EmployeeMasterDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeMasterData
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.employeeMasterData.ToListAsync();
            return View(applicationDbContext);
        }

        // GET: EmployeeMasterData/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.employeeMasterData == null)
            {
                return NotFound();
            }

            var employeeMasterDataModel = await _context.employeeMasterData
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeMasterDataModel == null)
            {
                return NotFound();
            }

            return View(employeeMasterDataModel);
        }
        //[Authorize(Roles="superadmin")]
        // GET: EmployeeMasterData/Create
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName");
            ViewData["HIC_DepartmentID"] = new SelectList(departments, "DeptID", "DeptName");
            ViewData["EmployementType"] = new SelectList(EmployeementTypeList.Types, "ID", "Name");

            var employees = _context.employeeMasterData.ToList();

            //var UppergradeEmp = employees.Where(x => x.Grade == ); 

            employees.Insert(0,
                new EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });

            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            ViewData["ReportToID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");

            return View();
        }
        //[Authorize(Roles = "superadmin")]

        // POST: EmployeeMasterData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,EmployeeFirstName,EmployeeMiddleName,EmployeeLastName,EmployeeStatus,EmployeeGender,EmployeeDob,EmployeeDoj,EmployeementType,EmergencyPhoneNo,EmployeeRelationToEmergence,JobApplicant,ContractEndDate,OfferDate,NoticeDays,ConfirmationDate,DateOfRetirement,DepartmentID,Grade,Designation,Branch,ReportToID,AttendanceDeviceId,Holidays,DefualtShift,SalaryMode,BankName,BankAccNumber,PayrollCostCenter,ProvidentFundAccount,HealthInsurranceProvider,HealthInsurranceNumber,MobileNumber,PreferredEmail,PersonalEnmail,CompanyEmail,PermanentAddress,CurrentAddress,PersonalBio,MatrialStatus,BloodGroup,FamilyBackground,HealthDetails,CNIC,DateOfIssue,ValidUpto,School_University,Qualification_Program,Level,YearOfPassing,PreviousCompany,PreviousDesignation,PreviousSalary,Address,HIC_Branch,HIC_FromDate,HIC_ToDate,EmployeeSalary,EmployeeNameGrade,EmployeeDesignation,RequiredSkills,Description,ResignationLetterDate,ExitInTo,RelievingDate,Feedback,NewWorkplace,ReasonForLeaving")] EmployeeMasterDataModel employeeMasterDataModel)
        {
            if (ModelState.IsValid)
            {
                employeeMasterDataModel.EmployeeID = Guid.NewGuid().ToString();
                _context.Add(employeeMasterDataModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeMasterDataModel.DepartmentID);
            //ViewData["HIC_DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeMasterDataModel.HIC_DepartmentID);
            ViewData["EmployementType"] = new SelectList(EmployeementTypeList.Types, "ID", "Name", employeeMasterDataModel.EmployeementType);

            var employees = _context.employeeMasterData.ToList();

            //var UppergradeEmp = employees.Where(x => x.Grade == ); 

            employees.Insert(0,
                new EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });

            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            ViewData["ReportToID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName", employeeMasterDataModel.ReportToID);

            return View(employeeMasterDataModel);
        }

        ////[Authorize(Roles = "superadmin")]

        // GET: EmployeeMasterData/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.employeeMasterData == null)
            {
                return NotFound();
            }

            var employeeMasterDataModel = await _context.employeeMasterData.FindAsync(id);
            if (employeeMasterDataModel == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeMasterDataModel.DepartmentID);
            //ViewData["HIC_DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeMasterDataModel.HIC_DepartmentID);
            ViewData["EmployementType"] = new SelectList(EmployeementTypeList.Types, "ID", "Name", employeeMasterDataModel.EmployeementType);

            var employees = _context.employeeMasterData.ToList();

            //var UppergradeEmp = employees.Where(x => x.Grade == ); 

            employees.Insert(0,
                new EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });

            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            ViewData["ReportToID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName", employeeMasterDataModel.ReportToID);

            return View(employeeMasterDataModel);
        }

        //[Authorize(Roles = "superadmin")]

        // POST: EmployeeMasterData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeID,EmployeeFirstName,EmployeeMiddleName,EmployeeLastName,EmployeeStatus,EmployeeGender,EmployeeDob,EmployeeDoj,EmployeementType,EmergencyPhoneNo,EmployeeRelationToEmergence,JobApplicant,ContractEndDate,OfferDate,NoticeDays,ConfirmationDate,DateOfRetirement,DepartmentID,Grade,Designation,Branch,ReportToID,AttendanceDeviceId,Holidays,DefualtShift,SalaryMode,BankName,BankAccNumber,PayrollCostCenter,ProvidentFundAccount,HealthInsurranceProvider,HealthInsurranceNumber,MobileNumber,PreferredEmail,PersonalEnmail,CompanyEmail,PermanentAddress,CurrentAddress,PersonalBio,MatrialStatus,BloodGroup,FamilyBackground,HealthDetails,CNIC,DateOfIssue,ValidUpto,School_University,Qualification_Program,Level,YearOfPassing,PreviousCompany,PreviousDesignation,PreviousSalary,Address,HIC_Branch,HIC_FromDate,HIC_ToDate,EmployeeSalary,EmployeeNameGrade,EmployeeDesignation,RequiredSkills,Description,ResignationLetterDate,ExitInTo,RelievingDate,Feedback,NewWorkplace,ReasonForLeaving")] EmployeeMasterDataModel employeeMasterDataModel)
        {
            if (id != employeeMasterDataModel.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeMasterDataModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeMasterDataModelExists(employeeMasterDataModel.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));


            }

            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeMasterDataModel.DepartmentID);
            //ViewData["HIC_DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeMasterDataModel.HIC_DepartmentID);
            ViewData["EmployementType"] = new SelectList(EmployeementTypeList.Types, "ID", "Name", employeeMasterDataModel.EmployeementType);

            var employees = _context.employeeMasterData.ToList();

            //var UppergradeEmp = employees.Where(x => x.Grade == ); 

            employees.Insert(0,
                new EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });

            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            ViewData["ReportToID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName", employeeMasterDataModel.ReportToID);

            return View(employeeMasterDataModel);
        }

        ////[Authorize(Roles = "superadmin")]


        // GET: EmployeeMasterData/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.employeeMasterData == null)
            {
                return NotFound();
            }

            var employeeMasterDataModel = await _context.employeeMasterData
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeMasterDataModel == null)
            {
                return NotFound();
            }
            var employees = _context.employeeMasterData.ToList();

            //var UppergradeEmp = employees.Where(x => x.Grade == ); 

            employees.Insert(0,
                new EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });

            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            ViewData["ReportToID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName", employeeMasterDataModel.ReportToID);
            return View(employeeMasterDataModel);
        }
        //[Authorize(Roles = "superadmin")]

        // POST: EmployeeMasterData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.employeeMasterData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.employeeMasterData'  is null.");
            }
            var employeeMasterDataModel = await _context.employeeMasterData.FindAsync(id);
            if (employeeMasterDataModel != null)
            {
                _context.employeeMasterData.Remove(employeeMasterDataModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //TasksAssignment

        //[Consumes("application/x-www-form-urlencoded")]
        //[HttpPost]
        public JsonResult GetReportToEmployees(string id)
        {
            var ReportToEmployess = _context.employeeMasterData.Where(e => e.ReportToID == id).ToList();
            return new JsonResult(ReportToEmployess);
        }

        private bool EmployeeMasterDataModelExists(string id)
        {
            return (_context.employeeMasterData?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        }
    }
}
