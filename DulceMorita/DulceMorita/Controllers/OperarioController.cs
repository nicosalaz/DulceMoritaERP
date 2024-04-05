using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DulceMorita.Models;

namespace DulceMorita.Controllers
{
    public class OperarioController : Controller
    {
        private readonly DulceMoritaContext _context;

        public OperarioController(DulceMoritaContext context)
        {
            _context = context;
        }

        // GET: Operario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Operarios.ToListAsync());
        }

        // GET: Operario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operario = await _context.Operarios
                .FirstOrDefaultAsync(m => m.IdOperario == id);
            if (operario == null)
            {
                return NotFound();
            }

            return View(operario);
        }

        // GET: Operario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOperario,NombreCompleto")] Operario operario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operario);
        }

        // GET: Operario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operario = await _context.Operarios.FindAsync(id);
            if (operario == null)
            {
                return NotFound();
            }
            return View(operario);
        }

        // POST: Operario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOperario,NombreCompleto")] Operario operario)
        {
            if (id != operario.IdOperario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperarioExists(operario.IdOperario))
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
            return View(operario);
        }

        // GET: Operario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operario = await _context.Operarios
                .FirstOrDefaultAsync(m => m.IdOperario == id);
            if (operario == null)
            {
                return NotFound();
            }

            return View(operario);
        }

        // POST: Operario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operario = await _context.Operarios.FindAsync(id);
            if (operario != null)
            {
                _context.Operarios.Remove(operario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperarioExists(int id)
        {
            return _context.Operarios.Any(e => e.IdOperario == id);
        }
    }
}
