using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Models.Department;
using PERM.Models.EmployeeMasterData;
using PERM_TASKS.Data;

namespace PERM_TASKS.Controllers.Department
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        string[] lowerGrades = { "L1", "L2", "L3" };

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            List<DepartmentModel> departments = await _context.Departments.ToListAsync();
            return View(departments);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departmentModel = await _context.Departments.Include(x=> x.ParentDepartment).Include(x=> x.LeaveBlockList)
                .FirstOrDefaultAsync(m => m.DeptID == id);
            if (departmentModel == null)
            {
                return NotFound();
            }

            return View(departmentModel);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["ParentDepartmentID"] = new SelectList(departments, "DeptID", "DeptName");

            //var employees = _context.employeeMasterData.ToList();

            ////var UppergradeEmp = employees.Where(x => x.Grade == ); 

            //employees.Insert(0,
            //    new EmployeeMasterDataModel
            //    { EmployeeID = "0", FullName = "---SELECT---" });

            //var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();

            //ViewData["LR_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["St_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["Exp_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");

            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptID,DeptName,ParentDepartmentID,DeptPayrollCostCenter,LeaveBlockList,LR_Approver,St_Approver,Exp_Approver")] DepartmentModel departmentModel)
        {
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["ParentDepartmentID"] = new SelectList(departments, "DeptID", "DeptName", departmentModel.ParentDepartmentID);

            //var employees = _context.employeeMasterData.ToList();

            ////var UppergradeEmp = employees.Where(x => x.Grade == ); 

            //employees.Insert(0,
            //    new EmployeeMasterDataModel
            //    { EmployeeID = "0", FullName = "---SELECT---" });

            //var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            //ViewData["LR_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["St_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["Exp_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            if (departmentModel.ParentDepartmentID is "0")
            { 
                ModelState.AddModelError("ParentDepartmentID", "The field Parent Department is required.");
                return View(departmentModel);

            }
            
            if (ModelState.IsValid)
            {
                departmentModel.DeptID = Guid.NewGuid().ToString();
                departmentModel.ParentDepartmentID = departmentModel.ParentDepartmentID is "0" ? null : departmentModel.ParentDepartmentID;
                _context.Add(departmentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

            

            return View(departmentModel);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departmentModel = await _context.Departments.FindAsync(id);
            if (departmentModel == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["ParentDepartmentID"] = new SelectList(departments, "DeptID", "DeptName", departmentModel.ParentDepartmentID);

            //var employees = _context.employeeMasterData.ToList();

            ////var UppergradeEmp = employees.Where(x => x.Grade == ); 

            //employees.Insert(0,
            //    new EmployeeMasterDataModel
            //    { EmployeeID = "0", FullName = "---SELECT---" });

            //var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            //ViewData["LR_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["St_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["Exp_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");

            return View(departmentModel);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DeptID,DeptName,ParentDepartmentID,DeptPayrollCostCenterID,LeaveBlockList,LR_Approver,St_Approver,Exp_Approver")] DepartmentModel departmentModel)
        {
            if (id != departmentModel.DeptID)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["ParentDepartmentID"] = new SelectList(departments, "DeptID", "DeptName", departmentModel.ParentDepartmentID);
            //var employees = _context.employeeMasterData.ToList();

            ////var UppergradeEmp = employees.Where(x => x.Grade == ); 

            //employees.Insert(0,
            //    new EmployeeMasterDataModel
            //    { EmployeeID = "0", FullName = "---SELECT---" });

            //var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            //ViewData["LR_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["St_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["Exp_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            if (departmentModel.ParentDepartmentID is "0")
            {
                ModelState.AddModelError("ParentDepartmentID", "The field Parent Department is required.");
                return View(departmentModel);

            }

           
            if (ModelState.IsValid)
            {
                try
                {
                    departmentModel.ParentDepartmentID = departmentModel.ParentDepartmentID is "0" ? null : departmentModel.ParentDepartmentID;
                    _context.Update(departmentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentModelExists(departmentModel.DeptID))
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
            

            return View(departmentModel);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departmentModel = await _context.Departments.Include(x => x.ParentDepartment).Include(x => x.LeaveBlockList)
                .FirstOrDefaultAsync(m => m.DeptID == id);
            if (departmentModel == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();

            departments.Insert(0,
                new DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["ParentDepartmentID"] = new SelectList(departments, "DeptID", "DeptName", departmentModel.ParentDepartmentID);

            //var employees = _context.employeeMasterData.ToList();

            ////var UppergradeEmp = employees.Where(x => x.Grade == ); 

            //employees.Insert(0,
            //    new EmployeeMasterDataModel
            //    { EmployeeID = "0", FullName = "---SELECT---" });

            //var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();


            //ViewData["LR_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["St_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            //ViewData["Exp_ApproverID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            return View(departmentModel);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DepartmentModel'  is null.");
            }
            var departmentModel = await _context.Departments.FindAsync(id);
            if (departmentModel != null)
            {
                _context.Departments.Remove(departmentModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentModelExists(string id)
        {
          return (_context.Departments?.Any(e => e.DeptID == id)).GetValueOrDefault();
        }
    }
}
