using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Models.Attendance;
using PERM.Models.Department;
using PERM_TASKS.Data;

namespace PERM.Controllers.Attendance
{
    public class EmployeeAttendanceRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAttendanceRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeAttendanceRequestModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AttendanceRequest.Include(e => e.Department).Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeAttendanceRequestModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AttendanceRequest == null)
            {
                return NotFound();
            }

            var employeeAttendanceRequestModel = await _context.AttendanceRequest
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeAttendanceRequestModel == null)
            {
                return NotFound();
            }

            return View(employeeAttendanceRequestModel);
        }

        // GET: EmployeeAttendanceRequestModels/Create
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();
            departments.Insert(0,
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DeptID", "DeptName");

            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(_context.employeeMasterData, "EmployeeID", "FullName");
            return View();
        }

        // POST: EmployeeAttendanceRequestModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeID,DepartmentID,FromDate,ToDate,HalfDayDate,HalfDay,Reason")] EmployeeAttendanceRequestModel employeeAttendanceRequestModel)
        {
            if (ModelState.IsValid)
            {
                employeeAttendanceRequestModel.ID = Guid.NewGuid().ToString();
                _context.Add(employeeAttendanceRequestModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var departments = _context.Departments.ToList();
            departments.Insert(0,
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DeptID", "DeptName");

            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(_context.employeeMasterData, "EmployeeID", "FullName");
            return View(employeeAttendanceRequestModel);
        }

        // GET: EmployeeAttendanceRequestModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AttendanceRequest == null)
            {
                return NotFound();
            }

            var employeeAttendanceRequestModel = await _context.AttendanceRequest.FindAsync(id);
            if (employeeAttendanceRequestModel == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();
            departments.Insert(0,
                new Models.Department.DepartmentModel
                { DeptID = "0", DeptName = "---SELECT---" });
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DeptID", "DeptName", employeeAttendanceRequestModel.DepartmentID);

            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(_context.employeeMasterData, "EmployeeID", "FullName", employeeAttendanceRequestModel.EmployeeID);
            return View(employeeAttendanceRequestModel);
        }

        // POST: EmployeeAttendanceRequestModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EmployeeID,DepartmentID,FromDate,ToDate,HalfDayDate,HalfDay,Reason")] EmployeeAttendanceRequestModel employeeAttendanceRequestModel)
        {
            if (id != employeeAttendanceRequestModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeAttendanceRequestModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAttendanceRequestModelExists(employeeAttendanceRequestModel.ID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DeptID", "DeptName", employeeAttendanceRequestModel.DepartmentID);

            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(_context.employeeMasterData, "EmployeeID", "FullName", employeeAttendanceRequestModel.EmployeeID);
            return View(employeeAttendanceRequestModel);
        }

        // GET: EmployeeAttendanceRequestModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AttendanceRequest == null)
            {
                return NotFound();
            }

            var employeeAttendanceRequestModel = await _context.AttendanceRequest
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeAttendanceRequestModel == null)
            {
                return NotFound();
            }

            return View(employeeAttendanceRequestModel);
        }

        // POST: EmployeeAttendanceRequestModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AttendanceRequest == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AttendanceRequestModel'  is null.");
            }
            var employeeAttendanceRequestModel = await _context.AttendanceRequest.FindAsync(id);
            if (employeeAttendanceRequestModel != null)
            {
                _context.AttendanceRequest.Remove(employeeAttendanceRequestModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAttendanceRequestModelExists(string id)
        {
            return (_context.AttendanceRequest?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
