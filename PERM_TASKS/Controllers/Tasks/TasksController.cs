using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Models.Task;
using PERM_TASKS.Data;

namespace PERM.Controllers.Tasks
{
    public class TasksController : Controller
    {
        string[] lowerGrades = { "L1", "L2", "L3" };
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context._tasks.Include(_ => _.TaskAssignedBy).Include(_ => _.TaskAssignedTo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context._tasks == null)
            {
                return NotFound();
            }

            var _TasksModel = await _context._tasks
                .Include(_ => _.TaskAssignedBy)
                .Include(_ => _.TaskAssignedTo)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (_TasksModel == null)
            {
                return NotFound();
            }

            return View(_TasksModel);
        }

        ////[Authorize(Roles = "superadmin")]

        // GET: Tasks/Create
        public IActionResult Create()
        {
            

            var employees = _context.employeeMasterData.ToList();

            //var UppergradeEmp = employees.Where(x => x.Grade == ); 

            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });

            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();

            var employeesUnderManager = employees.Where(e => e.EmployeeID == "200").ToList();

            ViewData["TaskAssignedByID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            ViewData["TaskAssignedToID"] = new SelectList(employees, "EmployeeID", "FullName");

            return View();
        }
        ////[Authorize(Roles = "superadmin")]

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,TaskName,TaskDescription,TaskStatus,TaskType,Priority,TaskCreatedDate,TaskUpdatedDate,TaskDeadline,TaskCompletionDate,TaskAssignedToID,TaskAssignedByID")] _TasksModel _TasksModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_TasksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var employees = _context.employeeMasterData.ToList();
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });
            var upperGradeEmployees = employees.Where(e => !lowerGrades.Any(g => g == e.Grade)).ToList();

            ViewData["TaskAssignedByID"] = new SelectList(employees, "EmployeeID", "FullName");
            ViewData["TaskAssignedToID"] = new SelectList(upperGradeEmployees, "EmployeeID", "FullName");
            return View(_TasksModel);
        }
        ////[Authorize(Roles = "superadmin")]

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context._tasks == null)
            {
                return NotFound();
            }

            var _TasksModel = await _context._tasks.FindAsync(id);
            if (_TasksModel == null)
            {
                return NotFound();
            }
            var employees = _context.employeeMasterData.ToList(); 
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });


            ViewData["TaskAssignedByID"] = new SelectList(employees, "EmployeeID", "FullName");
            ViewData["TaskAssignedToID"] = new SelectList(employees, "EmployeeID", "FullName");
            return View(_TasksModel);
        }
        ////[Authorize(Roles = "superadmin")]

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,TaskName,TaskDescription,TaskStatus,TaskType,Priority,TaskCreatedDate,TaskUpdatedDate,TaskDeadline,TaskCompletionDate,TaskAssignedToID,TaskAssignedByID")] _TasksModel _TasksModel)
        {
            if (id != _TasksModel.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_TasksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_TasksModelExists(_TasksModel.TaskID))
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
            employees.Insert(0,
                new Models.EmployeeMasterData.EmployeeMasterDataModel
                { EmployeeID = "0", FullName = "---SELECT---" });


            ViewData["TaskAssignedByID"] = new SelectList(employees, "EmployeeID", "FullName");
            ViewData["TaskAssignedToID"] = new SelectList(employees, "EmployeeID", "FullName");
            return View(_TasksModel);
        }

        ////[Authorize(Roles = "superadmin")]

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context._tasks == null)
            {
                return NotFound();
            }

            var _TasksModel = await _context._tasks
                .Include(_ => _.TaskAssignedBy)
                .Include(_ => _.TaskAssignedTo)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (_TasksModel == null)
            {
                return NotFound();
            }

            return View(_TasksModel);
        }
        ////[Authorize(Roles = "superadmin")]

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context._tasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext._tasks'  is null.");
            }
            var _TasksModel = await _context._tasks.FindAsync(id);
            if (_TasksModel != null)
            {
                _context._tasks.Remove(_TasksModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool _TasksModelExists(int id)
        {
          return (_context._tasks?.Any(e => e.TaskID == id)).GetValueOrDefault();
        }
    }
}
