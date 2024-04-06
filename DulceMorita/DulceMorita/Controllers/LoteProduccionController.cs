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
    public class LoteProduccionController : Controller
    {
        private readonly DulceMoritaContext _context;

        public LoteProduccionController(DulceMoritaContext context)
        {
            _context = context;
        }

        // GET: LoteProduccion
        public async Task<IActionResult> Index()
        {
            var dulceMoritaContext = _context.LoteProduccions.Include(l => l.FkOrdenNavigation);
            return View(await dulceMoritaContext.ToListAsync());
        }

        // GET: LoteProduccion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteProduccion = await _context.LoteProduccions
                .Include(l => l.FkOrdenNavigation)
                .FirstOrDefaultAsync(m => m.IdLote == id);
            if (loteProduccion == null)
            {
                return NotFound();
            }

            return View(loteProduccion);
        }

        // GET: LoteProduccion/Create
        public IActionResult Create()
        {
            ViewData["FkOrden"] = new SelectList(_context.OrdenProduccions, "IdOrden", "IdOrden");
            return View();
        }

        // POST: LoteProduccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLote,FkOrden,CantidadProduccion,FechaRegistro")] LoteProduccion loteProduccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loteProduccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkOrden"] = new SelectList(_context.OrdenProduccions, "IdOrden", "IdOrden", loteProduccion.FkOrden);
            return View(loteProduccion);
        }

        private bool LoteProduccionExists(int id)
        {
            return _context.LoteProduccions.Any(e => e.IdLote == id);
        }
    }
}
