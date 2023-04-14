using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Models.Attendance;
using PERM.Models.EmployeeMasterData;
using PERM_TASKS.Data;

namespace PERM.Controllers.Attendance
{
    public class EmployeeAttendanceAssistantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAttendanceAssistantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeAttendanceAssistantModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeAttendanceAssistant.Include(e => e.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeAttendanceAssistantModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeAttendanceAssistant == null)
            {
                return NotFound();
            }

            var employeeAttendanceAssistantModel = await _context.EmployeeAttendanceAssistant
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeAttendanceAssistantModel == null)
            {
                return NotFound();
            }

            return View(employeeAttendanceAssistantModel);
        }

        // GET: EmployeeAttendanceAssistantModels/Create
        public IActionResult Create()
        {
            
                  var departments = _context.Departments.ToList();
            departments.Insert(0,
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });

            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName");
            return View();
        }

        // POST: EmployeeAttendanceAssistantModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AT_Date,AT_Branch,DepartmentID")] EmployeeAttendanceAssistantModel employeeAttendanceAssistantModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeAttendanceAssistantModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var departments = _context.Departments.ToList();
            departments.Insert(0,
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeAttendanceAssistantModel.DepartmentID);
            return View(employeeAttendanceAssistantModel);
        }

        // GET: EmployeeAttendanceAssistantModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeAttendanceAssistant == null)
            {
                return NotFound();
            }
            var employeeAttendanceAssistantModel = await _context.EmployeeAttendanceAssistant.FindAsync(id);
            if (employeeAttendanceAssistantModel == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();
            departments.Insert(0,
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeAttendanceAssistantModel.DepartmentID);
            return View(employeeAttendanceAssistantModel);
        }

        // POST: EmployeeAttendanceAssistantModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AT_Date,AT_Branch,DepartmentID")] EmployeeAttendanceAssistantModel employeeAttendanceAssistantModel)
        {
            if (id != employeeAttendanceAssistantModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeAttendanceAssistantModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAttendanceAssistantModelExists(employeeAttendanceAssistantModel.ID))
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
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(departments, "DeptID", "DeptName", employeeAttendanceAssistantModel.DepartmentID);
            return View(employeeAttendanceAssistantModel);
        }

        // GET: EmployeeAttendanceAssistantModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeAttendanceAssistant == null)
            {
                return NotFound();
            }

            var employeeAttendanceAssistantModel = await _context.EmployeeAttendanceAssistant
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeAttendanceAssistantModel == null)
            {
                return NotFound();
            }

            return View(employeeAttendanceAssistantModel);
        }

        // POST: EmployeeAttendanceAssistantModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeAttendanceAssistant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeAttendanceAssistantModel'  is null.");
            }
            var employeeAttendanceAssistantModel = await _context.EmployeeAttendanceAssistant.FindAsync(id);
            if (employeeAttendanceAssistantModel != null)
            {
                _context.EmployeeAttendanceAssistant.Remove(employeeAttendanceAssistantModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAttendanceAssistantModelExists(int id)
        {
          return (_context.EmployeeAttendanceAssistant?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
