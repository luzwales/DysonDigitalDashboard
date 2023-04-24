using System;
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
    public class Scores : Controller
    {
        private readonly DigitalDbContext _context;

        public Scores(DigitalDbContext context)
        {
            _context = context;
        }

        // GET: Scores
        public async Task<IActionResult> Index()
        {
              return _context.ScoreList != null ? 
                          View(await _context.ScoreList.ToListAsync()) :
                          Problem("Entity set 'DigitalDbContext.ScoreList'  is null.");
        }

        // GET: Scores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ScoreList == null)
            {
                return NotFound();
            }

            var scoreList = await _context.ScoreList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (scoreList == null)
            {
                return NotFound();
            }

            return View(scoreList);
        }

        // GET: Scores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Kpi,ScoreValue,Date,Remark")] ScoreList scoreList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scoreList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scoreList);
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ScoreList == null)
            {
                return NotFound();
            }

            var scoreList = await _context.ScoreList.FindAsync(id);
            if (scoreList == null)
            {
                return NotFound();
            }
            return View(scoreList);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Kpi,ScoreValue,Date,Remark")] ScoreList scoreList)
        {
            if (id != scoreList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scoreList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreListExists(scoreList.ID))
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
            return View(scoreList);
        }

        // GET: Scores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ScoreList == null)
            {
                return NotFound();
            }

            var scoreList = await _context.ScoreList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (scoreList == null)
            {
                return NotFound();
            }

            return View(scoreList);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ScoreList == null)
            {
                return Problem("Entity set 'DigitalDbContext.ScoreList'  is null.");
            }
            var scoreList = await _context.ScoreList.FindAsync(id);
            if (scoreList != null)
            {
                _context.ScoreList.Remove(scoreList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreListExists(int id)
        {
          return (_context.ScoreList?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
