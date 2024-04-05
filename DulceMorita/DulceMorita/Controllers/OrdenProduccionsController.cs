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
    public class OrdenProduccionsController : Controller
    {
        private readonly DulceMoritaContext _context;
        private const string formatDate = "yyyy-MM-dd hh:mm:ss";
        private LoteProduccion loteProduccion = new LoteProduccion();
        public OrdenProduccionsController(DulceMoritaContext context)
        {
            _context = context;
        }

        // GET: OrdenProduccions
        public async Task<IActionResult> Index()
        {
            var dulceMoritaContext = _context.OrdenProduccions.Include(o => o.FkProductoNavigation);
            return View(await dulceMoritaContext.ToListAsync());
        }

        // GET: OrdenProduccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenProduccion = await _context.OrdenProduccions
                .Include(o => o.FkProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenProduccion == null)
            {
                return NotFound();
            }

            return View(ordenProduccion);
        }

        // GET: OrdenProduccions/Create
        public IActionResult Create()
        {
            ViewData["FkProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: OrdenProduccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrden,ProduccionTotal,FechaCreacion,FkProducto")] OrdenProduccion ordenProduccion)
        {
            if (ModelState.IsValid)
            {
                ordenProduccion.FechaCreacion = DateTime.Now.ToString(formatDate);
                _context.Add(ordenProduccion);
                Console.WriteLine(ordenProduccion.FechaCreacion.ToString());
                await _context.SaveChangesAsync();
                Console.WriteLine(ordenProduccion.IdOrden.ToString());

                return RedirectToAction(nameof(Index));
            }
            ViewData["FkProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ordenProduccion.FkProducto);
            return View(ordenProduccion);
        }

        // GET: OrdenProduccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenProduccion = await _context.OrdenProduccions.FindAsync(id);
            if (ordenProduccion == null)
            {
                return NotFound();
            }
            ViewData["FkProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ordenProduccion.FkProducto);
            return View(ordenProduccion);
        }

        // POST: OrdenProduccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrden,ProduccionTotal,FechaCreacion,FkProducto")] OrdenProduccion ordenProduccion)
        {
            if (id != ordenProduccion.IdOrden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenProduccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenProduccionExists(ordenProduccion.IdOrden))
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
            ViewData["FkProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", ordenProduccion.FkProducto);
            return View(ordenProduccion);
        }

        // GET: OrdenProduccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenProduccion = await _context.OrdenProduccions
                .Include(o => o.FkProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenProduccion == null)
            {
                return NotFound();
            }

            return View(ordenProduccion);
        }

        // POST: OrdenProduccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenProduccion = await _context.OrdenProduccions.FindAsync(id);
            if (ordenProduccion != null)
            {
                _context.OrdenProduccions.Remove(ordenProduccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenProduccionExists(int id)
        {
            return _context.OrdenProduccions.Any(e => e.IdOrden == id);
        }
    }
}
