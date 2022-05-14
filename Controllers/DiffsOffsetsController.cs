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
    }
}
