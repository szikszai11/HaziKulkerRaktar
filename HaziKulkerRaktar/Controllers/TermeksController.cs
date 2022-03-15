using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HaziKulkerRaktar.Data;
using Microsoft.AspNetCore.Authorization;
using HaziKulkerRaktar.Models;

namespace HaziKulkerRaktar.Controllers
{
    public class TermeksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TermeksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Termeks
        public async Task<IActionResult> Index(string NevKereso, string ArKereso)
        {
            Termekkereso kereso1 = new Termekkereso();
            var Termekek = _context.Termek.Select(x => x);
            if (!string.IsNullOrEmpty(NevKereso))
            {
                kereso1.NevKereso = NevKereso;
                Termekek = Termekek.Where(x => x.Elnevezes.Contains(NevKereso));
            }

            if (!string.IsNullOrEmpty(ArKereso))
            {
                kereso1.ArKereso = ArKereso;
                Termekek = Termekek.Where(x => x.Elnevezes.Equals(ArKereso));

            }

            kereso1.KategoriaLista = new SelectList(await _context.Termek.Select(x => x.Kategoria).Distinct().ToListAsync());
            kereso1.Kategoriak = await Termekek.ToListAsync();

            return View(kereso1);
        }

        // GET: Termeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termek = await _context.Termek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termek == null)
            {
                return NotFound();
            }

            return View(termek);
        }

        // GET: Termeks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Termeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Elnevezes,Kategoria,CsomEgyseg,Darabszam")] Termek termek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(termek);
        }

        // GET: Termeks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termek = await _context.Termek.FindAsync(id);
            if (termek == null)
            {
                return NotFound();
            }
            return View(termek);
        }

        // POST: Termeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Elnevezes,Kategoria,CsomEgyseg,Darabszam")] Termek termek)
        {
            if (id != termek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermekExists(termek.Id))
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
            return View(termek);
        }

        // GET: Termeks/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termek = await _context.Termek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termek == null)
            {
                return NotFound();
            }

            return View(termek);
        }

        // POST: Termeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termek = await _context.Termek.FindAsync(id);
            _context.Termek.Remove(termek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TermekExists(int id)
        {
            return _context.Termek.Any(e => e.Id == id);
        }
    }
}
