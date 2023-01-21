using System;
using System.Collections.Generic;
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
    public class HorairesController : Controller
    {
        private readonly tpCatalinaV4Context _context;

        public HorairesController(tpCatalinaV4Context context)
        {
            _context = context;
        }

        // GET: Horaires
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var tpCatalinaV4Context = _context.Horaire.Include(h => h.Tache).Include(h => h.Utilisateur);

            ViewData["CurrentFilter"] = searchString;
            ViewData["UtilisateurSortParm"] = sortOrder=="Utilisateur"? "utilizateur_desc" : "Utilisateur";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["NbRepetSortParm"] = sortOrder == "NbRepet" ? "nbRepet_desc" : "NbRepet";
            ViewData["TacheSortParm"] = sortOrder == "Tache" ? "tache_desc" : "Tache";
            var horaireSort = from h in tpCatalinaV4Context
                           select h;
            if (searchString !=null)
            {

                horaireSort = horaireSort.Where(h => h.Utilisateur.NomComplet.Equals(searchString));
                             
            }
            switch (sortOrder)
            {
                case "utilizateur_desc":
                    horaireSort = horaireSort.OrderByDescending(h => h.UtilisateurID);
                    break;
                case "Utilisateur":
                    horaireSort = horaireSort.OrderBy(h => h.UtilisateurID);
                    break;
                case "Date":
                    horaireSort = horaireSort.OrderBy(h => h.Date);
                    break;
                case "date_desc":
                    horaireSort = horaireSort.OrderByDescending(h => h.Date);
                    break;
                case "NbRepet":
                    horaireSort = horaireSort.OrderBy(h => h.NbRepet);
                    break;
                case "nbRepet_desc":
                    horaireSort = horaireSort.OrderByDescending(h => h.NbRepet);
                    break;
                case "Tache":
                    horaireSort = horaireSort.OrderBy(h => h.Tache);
                    break;
                case "tache_desc":
                    horaireSort = horaireSort.OrderByDescending(h => h.Tache);
                    break;

                default:
                  horaireSort = horaireSort.OrderBy(h => h.Date);
                  break;
            }
            return View(await horaireSort.AsNoTracking().ToListAsync());



         
        }

        // GET: Horaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Horaire == null)
            {
                return NotFound();
            }

            var horaire = await _context.Horaire
                .Include(h => h.Tache)
                .Include(h => h.Utilisateur)
                .FirstOrDefaultAsync(m => m.HoraireID == id);
            if (horaire == null)
            {
                return NotFound();
            }

            return View(horaire);
        }

        // GET: Horaires/Create
        public IActionResult Create()
        {
            ViewData["TacheID"] = new SelectList(_context.Set<Tache>(), "TacheID", "TacheID");
            ViewData["UtilisateurID"] = new SelectList(_context.Set<Utilisateur>(), "UtilisateurID", "UtilisateurID");
           
            return View();
        }

        // POST: Horaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoraireID,Date,NbRepet,UtilisateurID,TacheID")] Horaire horaire)
        {
            // if (ModelState.IsValid)
            //{

           

            for (int i = 0; i < horaire.NbRepet; i++)
            {
               
                var horaireRepet = new Horaire();
                horaireRepet.Date=horaire.Date.AddDays(i*7);
                horaireRepet.NbRepet = horaire.NbRepet;
                horaireRepet.UtilisateurID = horaire.UtilisateurID;
                horaireRepet.TacheID=horaire.TacheID;   
                
              
                _context.Add(horaireRepet);
                
                await _context.SaveChangesAsync();
            }

            


            return RedirectToAction(nameof(Index));
           // }
            ViewData["TacheID"] = new SelectList(_context.Set<Tache>(), "TacheID", "TacheID", horaire.TacheID);
            ViewData["UtilisateurID"] = new SelectList(_context.Set<Utilisateur>(), "UtilisateurID", "UtilisateurID", horaire.UtilisateurID);

           
            // return View(horaire);
        }

        // GET: Horaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Horaire == null)
            {
                return NotFound();
            }

           
            var horaire =await _context.Horaire.FindAsync(id);
            if (horaire == null)
            {
                return NotFound();
            }
            ViewData["TacheID"] = new SelectList(_context.Set<Tache>(), "TacheID", "TacheID", horaire.TacheID);
            ViewData["UtilisateurID"] = new SelectList(_context.Set<Utilisateur>(), "UtilisateurID", "UtilisateurID", horaire.UtilisateurID);
     
      
            return View(horaire);
        }

        // POST: Horaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoraireID,Date,NbRepet,UtilisateurID,TacheID")] Horaire horaire)
        {
            if (id != horaire.HoraireID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
           // {
                try
                {
                    _context.Update(horaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoraireExists(horaire.HoraireID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           // }
            ViewData["TacheID"] = new SelectList(_context.Set<Tache>(), "TacheID", "TacheID", horaire.TacheID);
            ViewData["UtilisateurID"] = new SelectList(_context.Set<Utilisateur>(), "UtilisateurID", "UtilisateurID", horaire.UtilisateurID);
           // return View(horaire);
        }

        // GET: Horaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Horaire == null)
            {
                return NotFound();
            }

            var horaire = await _context.Horaire
                .Include(h => h.Tache)
                .Include(h => h.Utilisateur)
                .FirstOrDefaultAsync(m => m.HoraireID == id);
            if (horaire == null)
            {
                return NotFound();
            }

            return View(horaire);
        }

        // POST: Horaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Horaire == null)
            {
                return Problem("Entity set 'tpCatalinaV4Context.Horaire'  is null.");
            }
            var horaire = await _context.Horaire.FindAsync(id);
            if (horaire != null)
            {
                _context.Horaire.Remove(horaire);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoraireExists(int id)
        {
          return _context.Horaire.Any(e => e.HoraireID == id);
        }

       
    }
}
