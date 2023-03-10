using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpCatalinaV4.Data;
using tpCatalinaV4.Models;

namespace tpCatalinaV4.Controllers
{
    public class TachesController : Controller
    {
        private readonly tpCatalinaV4Context _context;

        public TachesController(tpCatalinaV4Context context)
        {
            _context = context;
        }

        // GET: Taches
        public async Task<IActionResult> Index()
        {
              return View(await _context.Tache.ToListAsync());
        }

        // GET: Taches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tache == null)
            {
                return NotFound();
            }

            var tache = await _context.Tache
                .FirstOrDefaultAsync(m => m.TacheID == id);
            if (tache == null)
            {
                return NotFound();
            }

            return View(tache);
        }

        // GET: Taches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TacheID,Description,Duree,Repetitive")] Tache tache)
        {
            //if (ModelState.IsValid)
            //{

                _context.Add(tache);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
           //return View(tache);
        }

        // GET: Taches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tache == null)
            {
                return NotFound();
            }

            var tache = await _context.Tache.FindAsync(id);
            if (tache == null)
            {
                return NotFound();
            }
            return View(tache);
        }

        // POST: Taches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TacheID,Description,Duree,Repetitive")] Tache tache)
        {
            if (id != tache.TacheID)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(tache);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacheExists(tache.TacheID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
           // return View(tache);
        }

        // GET: Taches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tache == null)
            {
                return NotFound();
            }

            var tache = await _context.Tache
                .FirstOrDefaultAsync(m => m.TacheID == id);
            if (tache == null)
            {
                return NotFound();
            }

            return View(tache);
        }

        // POST: Taches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tache == null)
            {
                return Problem("Entity set 'tpCatalinaV4Context.Tache'  is null.");
            }
            var tache = await _context.Tache.FindAsync(id);
            if (tache != null)
            {
                _context.Tache.Remove(tache);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacheExists(int id)
        {
          return _context.Tache.Any(e => e.TacheID == id);
        }
    }
}
