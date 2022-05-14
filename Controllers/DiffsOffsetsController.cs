using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiffFinder.Data;
using DiffFinder.Models;

namespace DiffFinder.Controllers
{
    public class DiffsOffsetsController : Controller
    {
        private readonly MvcWebAppDbContext _context;

        public DiffsOffsetsController(MvcWebAppDbContext context)
        {
            _context = context;
        }

        // GET: DiffsOffsets
        public async Task<IActionResult> Index()
        {
            var mvcWebAppDbContext = _context.DiffsOffsets.Include(d => d.DiffrenceInformation);
            return View(await mvcWebAppDbContext.ToListAsync());
        }

        // GET: DiffsOffsets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiffsOffsets == null)
            {
                return NotFound();
            }

            var diffsOffsets = await _context.DiffsOffsets
                .Include(d => d.DiffrenceInformation)
                .FirstOrDefaultAsync(m => m.DiffsOffsetsId == id);
            if (diffsOffsets == null)
            {
                return NotFound();
            }

            return View(diffsOffsets);
        }

        // GET: DiffsOffsets/Create
        public IActionResult Create()
        {
            ViewData["DiffrenceInformationId"] = new SelectList(_context.DiffrenceInformation, "DifferenceInformationId", "LeftString");
            return View();
        }

        // POST: DiffsOffsets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiffsOffsetsId,DiffrenceInformationId,Diffs,Offset")] DiffsOffsets diffsOffsets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diffsOffsets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiffrenceInformationId"] = new SelectList(_context.DiffrenceInformation, "DifferenceInformationId", "LeftString", diffsOffsets.DiffrenceInformationId);
            return View(diffsOffsets);
        }

        // GET: DiffsOffsets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiffsOffsets == null)
            {
                return NotFound();
            }

            var diffsOffsets = await _context.DiffsOffsets.FindAsync(id);
            if (diffsOffsets == null)
            {
                return NotFound();
            }
            ViewData["DiffrenceInformationId"] = new SelectList(_context.DiffrenceInformation, "DifferenceInformationId", "LeftString", diffsOffsets.DiffrenceInformationId);
            return View(diffsOffsets);
        }

        // POST: DiffsOffsets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiffsOffsetsId,DiffrenceInformationId,Diffs,Offset")] DiffsOffsets diffsOffsets)
        {
            if (id != diffsOffsets.DiffsOffsetsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diffsOffsets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiffsOffsetsExists(diffsOffsets.DiffsOffsetsId))
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
            ViewData["DiffrenceInformationId"] = new SelectList(_context.DiffrenceInformation, "DifferenceInformationId", "LeftString", diffsOffsets.DiffrenceInformationId);
            return View(diffsOffsets);
        }

        // GET: DiffsOffsets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiffsOffsets == null)
            {
                return NotFound();
            }

            var diffsOffsets = await _context.DiffsOffsets
                .Include(d => d.DiffrenceInformation)
                .FirstOrDefaultAsync(m => m.DiffsOffsetsId == id);
            if (diffsOffsets == null)
            {
                return NotFound();
            }

            return View(diffsOffsets);
        }

        // POST: DiffsOffsets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiffsOffsets == null)
            {
                return Problem("Entity set 'MvcWebAppDbContext.DiffsOffsets'  is null.");
            }
            var diffsOffsets = await _context.DiffsOffsets.FindAsync(id);
            if (diffsOffsets != null)
            {
                _context.DiffsOffsets.Remove(diffsOffsets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiffsOffsetsExists(int id)
        {
          return (_context.DiffsOffsets?.Any(e => e.DiffsOffsetsId == id)).GetValueOrDefault();
        }
    }
}
