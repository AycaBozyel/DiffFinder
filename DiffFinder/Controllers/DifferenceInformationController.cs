using DiffFinder.Data;
using DiffFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using DiffFinder.Business;

namespace DiffFinder.Controllers
{
    public class DifferenceInformationController : Controller
    {
        private readonly MvcWebAppDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        public DiffFinderService diffFinderService = new DiffFinderService();

        public DifferenceInformationController(MvcWebAppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            hostingEnvironment = env;
        }
        // GET: DifferenceInformationController
        public async Task<IActionResult> Index()
        {
            ViewData["MainPath"] = Directory.GetCurrentDirectory();
            return View(await _context.DiffrenceInformation.ToListAsync());
        }

        // GET: DifferenceInformationController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffInfo = await _context.DiffrenceInformation.FirstOrDefaultAsync(m => m.DifferenceInformationId == id);
            if (diffInfo == null)
            {
                return NotFound();
            }

            return View(diffInfo);
        }


        // GET: DifferenceInformationController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DifferenceInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DifferenceInformationId,LeftString,RightString")] DiffrenceInformation diffrenceInformation)
        {
            if (ModelState.IsValid)
            {
                Response response = diffFinderService.CalculateDifference(diffrenceInformation);
                diffrenceInformation.Result = response.ResultMessage;
                diffrenceInformation.DiffsOffsets = response.DiffsOffsetList;
                _context.Add(diffrenceInformation);

                _ = _context.SaveChangesAsync();

                foreach(DiffsOffsets diffsOffsets in diffrenceInformation.DiffsOffsets)
                {
                    diffsOffsets.DiffrenceInformation = diffrenceInformation;
                    diffsOffsets.DiffrenceInformationId = diffrenceInformation.DifferenceInformationId;
                    _context.DiffsOffsets.Add(diffsOffsets);
                    _ = _context.SaveChangesAsync();
                } 
                
                return RedirectToAction(nameof(Index));
            }
            return View(diffrenceInformation);
        }

        // GET: DifferenceInformationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var diffInfo = await _context.DiffrenceInformation.FindAsync(id);
            if(diffInfo == null)
            {
                return NotFound();
            }

            return View(diffInfo);
        }

        // POST: DifferenceInformationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DifferenceInformationId,LeftString,RightString,Result")] DiffrenceInformation diffrenceInformation)
        {
            if (id != diffrenceInformation.DifferenceInformationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diffrenceInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiffInfoExist(diffrenceInformation.DifferenceInformationId))
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
            return View(diffrenceInformation);
        }

        // GET: DifferenceInformationController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffInfo = await _context.DiffrenceInformation.FirstOrDefaultAsync(m => m.DifferenceInformationId == id);
            if (diffInfo == null)
            {
                return NotFound();
            }

            return View(diffInfo);
        }

        // POST: DifferenceInformationController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diffInfo = await _context.DiffrenceInformation.FindAsync(id);
            _ = _context.DiffrenceInformation.Remove(diffInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: DiffsOffsets/List/5
        public async Task<IActionResult> List(int id)
        {
            var mvcWebAppDbContext = _context.DiffsOffsets.Include(d => d.DiffrenceInformation).Where(x => x.DiffrenceInformationId == id);
            return View(await mvcWebAppDbContext.ToListAsync());
        }

        private bool DiffInfoExist(int id)
        {
            return _context.DiffrenceInformation.Any(x => x.DifferenceInformationId == id);
        }

    }
}
