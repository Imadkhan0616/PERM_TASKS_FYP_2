using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Models.Attendance;
using PERM.Models.Department;
using PERM.Models.Employee_Master_Data;
using PERM_TASKS.Data;

namespace PERM.Controllers.Attendance
{
    public class EmployeeMarkAttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeMarkAttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeMarkAttendance
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MarkAttendance.Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeMarkAttendance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MarkAttendance == null)
            {
                return NotFound();
            }

            var employeeMarkAttendanceModel = await _context.MarkAttendance
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeMarkAttendanceModel == null)
            {
                return NotFound();
            }

            return View(employeeMarkAttendanceModel);
        }

        // GET: EmployeeMarkAttendance/Create
        public IActionResult Create()
        {
            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0, new Models.EmployeeMasterData.EmployeeMasterDataModel { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(employees, "EmployeeID", "FullName");
            ViewData["Status"] = new SelectList(EmployeeAttendanceStatusList.Types, "ID", "Name");


            return View();
        }

        // POST: EmployeeMarkAttendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeID,Date,Status,Shift,lateEntry,EarlyExit")] EmployeeMarkAttendanceModel employeeMarkAttendanceModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(employeeMarkAttendanceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0, new Models.EmployeeMasterData.EmployeeMasterDataModel { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(employees, "EmployeeID", "FullName", employeeMarkAttendanceModel.EmployeeID);
             ViewData["Status"] = new SelectList(EmployeeAttendanceStatusList.Types, "ID", "Name", employeeMarkAttendanceModel.Status);


            return View(employeeMarkAttendanceModel);
        }

        // GET: EmployeeMarkAttendance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MarkAttendance == null)
            {
                return NotFound();
            }

            var employeeMarkAttendanceModel = await _context.MarkAttendance.FindAsync(id);
            if (employeeMarkAttendanceModel == null)
            {
                return NotFound();
            }
            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0, new Models.EmployeeMasterData.EmployeeMasterDataModel { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(employees, "EmployeeID", "FullName", employeeMarkAttendanceModel.EmployeeID);
            ViewData["Status"] = new SelectList(EmployeeAttendanceStatusList.Types, "ID", "Name", employeeMarkAttendanceModel.Status);

            return View(employeeMarkAttendanceModel);
        }

        // POST: EmployeeMarkAttendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeID,Date,Status,Shift,lateEntry,EarlyExit")] EmployeeMarkAttendanceModel employeeMarkAttendanceModel)
        {
            if (id != employeeMarkAttendanceModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeMarkAttendanceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeMarkAttendanceModelExists(employeeMarkAttendanceModel.ID))
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
            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0, new Models.EmployeeMasterData.EmployeeMasterDataModel { EmployeeID = "0", FullName = "---SELECT---" });
            ViewData["EmployeeID"] = new SelectList(employees, "EmployeeID", "FullName", employeeMarkAttendanceModel.EmployeeID);
            ViewData["Status"] = new SelectList(EmployeeAttendanceStatusList.Types, "ID", "Name", employeeMarkAttendanceModel.Status);

            return View(employeeMarkAttendanceModel);
        }

        // GET: EmployeeMarkAttendance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MarkAttendance == null)
            {
                return NotFound();
            }

            var employeeMarkAttendanceModel = await _context.MarkAttendance
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeMarkAttendanceModel == null)
            {
                return NotFound();
            }

            return View(employeeMarkAttendanceModel);
        }

        // POST: EmployeeMarkAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MarkAttendance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MarkAttendanceModel'  is null.");
            }
            var employeeMarkAttendanceModel = await _context.MarkAttendance.FindAsync(id);
            if (employeeMarkAttendanceModel != null)
            {
                _context.MarkAttendance.Remove(employeeMarkAttendanceModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMarkAttendanceModelExists(int id)
        {
            return _context.MarkAttendance.Any(e => e.ID == id);
        }
    }
}
