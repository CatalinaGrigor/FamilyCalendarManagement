using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using tpCatalinaV4.Data;
using tpCatalinaV4.Models;

namespace tpCatalinaV4.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly tpCatalinaV4Context _context;

        public UtilisateursController(tpCatalinaV4Context context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
              return View(await _context.Utilisateur.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utilisateur == null)
            {
                return NotFound();
            }


            var utilisateur = await _context.Utilisateur
                .FirstOrDefaultAsync(m => m.UtilisateurID == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilisateurID,NomComplet,Role")] Utilisateur utilisateur)
        {
           // if (ModelState.IsValid)
           // {
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           // }
           // return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Utilisateur == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            

            var horaireSort = from h in _context.Horaire
                              select h;
           
            var detail=horaireSort.Where(h => h.UtilisateurID == id).Include("Tache")
                                    .FirstOrDefault();


            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilisateurID,NomComplet,Role")] Utilisateur utilisateur)
        {
            if (id != utilisateur.UtilisateurID)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
           // {
                try
                {
                    _context.Update(utilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateurExists(utilisateur.UtilisateurID))
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
            //return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utilisateur == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur
                .FirstOrDefaultAsync(m => m.UtilisateurID == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utilisateur == null)
            {
                return Problem("Entity set 'tpCatalinaV4Context.Utilisateur'  is null.");
            }
            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateur.Remove(utilisateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(int id)
        {
          return _context.Utilisateur.Any(e => e.UtilisateurID == id);
        }
    }
}
