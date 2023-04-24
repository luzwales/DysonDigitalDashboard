

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DysonDigitalDashboard.Data;
using DysonDigitalDashboard.Models.Domain;

namespace DysonDigitalDashboard.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DigitalDbContext digitalcontext;

        public EmployeesController(DigitalDbContext context)
        {
            digitalcontext = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
              return digitalcontext.Employees != null ? 
                          View(await digitalcontext.Employees.ToListAsync()) :
                          Problem("Entity set 'DigitalDbContext.Employees'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || digitalcontext.Employees == null)
            {
                return NotFound();
            }

            var employee = await digitalcontext.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Salary,DateOfBirth,Department")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                digitalcontext.Add(employee);
                await digitalcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || digitalcontext.Employees == null)
            {
                return NotFound();
            }

            var employee = await digitalcontext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,Salary,DateOfBirth,Department")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    digitalcontext.Update(employee);
                    await digitalcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || digitalcontext.Employees == null)
            {
                return NotFound();
            }

            var employee = await digitalcontext.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (digitalcontext.Employees == null)
            {
                return Problem("Entity set 'DigitalDbContext.Employees'  is null.");
            }
            var employee = await digitalcontext.Employees.FindAsync(id);
            if (employee != null)
            {
                digitalcontext.Employees.Remove(employee);
            }
            
            await digitalcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
          return (digitalcontext.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
