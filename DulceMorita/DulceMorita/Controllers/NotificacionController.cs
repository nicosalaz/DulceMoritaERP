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
    public class NotificacionController : Controller
    {
        private readonly DulceMoritaContext _context;

        public NotificacionController(DulceMoritaContext context)
        {
            _context = context;
        }

        // GET: Notificacion
        public async Task<IActionResult> Index()
        {
            var dulceMoritaContext = _context.Notificacions.Include(n => n.FkLoteNavigation).Include(n => n.FkOpeNavigation);
            return View(await dulceMoritaContext.ToListAsync());
        }

        // GET: Notificacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions
                .Include(n => n.FkLoteNavigation)
                .Include(n => n.FkOpeNavigation)
                .FirstOrDefaultAsync(m => m.IdNotificacion == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // GET: Notificacion/Create
        public IActionResult Create()
        {
            ViewData["FkLote"] = new SelectList(_context.LoteProduccions, "IdLote", "IdLote");
            ViewData["FkOpe"] = new SelectList(_context.Operarios, "IdOperario", "NombreCompleto");
            return View();
        }

        // POST: Notificacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNotificacion,FkLote,FkOpe,Buenas,Malas,FInicio,FFin,GastosAdicionales,Obseraciones")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                LoteProduccion loteProduccion = await _context.LoteProduccions.Include(n => n.FkOrdenNavigation).FirstOrDefaultAsync(m => m.IdLote == notificacion.FkLote);
                notificacion.Buenas = loteProduccion.CantidadProduccion - notificacion.Malas;
                _context.Add(notificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLote"] = new SelectList(_context.LoteProduccions, "IdLote", "IdLote", notificacion.FkLote);
            ViewData["FkOpe"] = new SelectList(_context.Operarios, "IdOperario", "IdOperario", notificacion.FkOpe);
            return View(notificacion);
        }

        // GET: Notificacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            ViewData["FkLote"] = new SelectList(_context.LoteProduccions, "IdLote", "IdLote", notificacion.FkLote);
            ViewData["FkOpe"] = new SelectList(_context.Operarios, "IdOperario", "IdOperario", notificacion.FkOpe);
            return View(notificacion);
        }

        // POST: Notificacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNotificacion,FkLote,FkOpe,Buenas,Malas,FInicio,FFin,GastosAdicionales,Obseraciones")] Notificacion notificacion)
        {
            if (id != notificacion.IdNotificacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionExists(notificacion.IdNotificacion))
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
            ViewData["FkLote"] = new SelectList(_context.LoteProduccions, "IdLote", "IdLote", notificacion.FkLote);
            ViewData["FkOpe"] = new SelectList(_context.Operarios, "IdOperario", "IdOperario", notificacion.FkOpe);
            return View(notificacion);
        }

        // GET: Notificacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions
                .Include(n => n.FkLoteNavigation)
                .Include(n => n.FkOpeNavigation)
                .FirstOrDefaultAsync(m => m.IdNotificacion == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // POST: Notificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion != null)
            {
                _context.Notificacions.Remove(notificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificacionExists(int id)
        {
            return _context.Notificacions.Any(e => e.IdNotificacion == id);
        }
    }
}
