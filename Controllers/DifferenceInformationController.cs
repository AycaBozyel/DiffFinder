﻿using DiffFinder.Data;
using DiffFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DiffFinder.Controllers
{
    public class DifferenceInformationController : Controller
    {
        private readonly MvcWebAppDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private string ResultMessage;
        private List<DiffsOffsets> DiffsOffsetsList;

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
        public async Task<IActionResult> Create([Bind("DifferenceInformationId,LeftString,RightString,Result")] DiffrenceInformation diffrenceInformation)
        {
            if (ModelState.IsValid)
            {
                CalculateDifference(diffrenceInformation);
                diffrenceInformation.Result = ResultMessage;
                diffrenceInformation.DiffsOffsets = DiffsOffsetsList;
                _context.Add(diffrenceInformation);
                await _context.SaveChangesAsync();

                //DiffOffsetleri de kaydet :) 
                
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

            return View();
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


        private bool DiffInfoExist(int id)
        {
            return _context.DiffrenceInformation.Any(x => x.DifferenceInformationId == id);
        }

        #region Calculate Difference

        private void CalculateDifference(DiffrenceInformation diffrenceInformation)
        {
            string leftString = diffrenceInformation.LeftString;
            string rightString = diffrenceInformation.RightString;

            byte[] leftData = Convert.FromBase64String(leftString);
            string decodedLeftString = Encoding.UTF8.GetString(leftData);

            byte[] rightData = Convert.FromBase64String(rightString);
            string decodedRightString = Encoding.UTF8.GetString(rightData);

            if(leftData.Length != rightData.Length)
            {
                ResultMessage = "Inputs are of different size.";
                DiffsOffsetsList = new List<DiffsOffsets>();
            }
            List<DiffsOffsets> diffOffsetList = new List<DiffsOffsets>();
            for(int i = 0; i < decodedLeftString.Length; i++)
            {
                int diffCount = 0;
                byte[] byteListLeftStringChar = Encoding.UTF8.GetBytes(decodedLeftString[i].ToString());
                byte[] byteListRightStringChar = Encoding.UTF8.GetBytes(decodedRightString[i].ToString());
                for(int j = 0; j < byteListLeftStringChar.Length; j++)
                {
                    if(byteListLeftStringChar[j] != byteListRightStringChar[j])
                    {
                        diffCount++;
                    }
                }
                if (diffCount != 0)
                {
                    DiffsOffsets diffsOffsets = new DiffsOffsets();
                    diffsOffsets.Offset = i;
                    diffsOffsets.Diffs = diffCount;
                    diffOffsetList.Add(diffsOffsets);
                }


                DiffsOffsetsList = diffOffsetList;
            }
            
            if (String.Equals(decodedLeftString, decodedRightString))
            {
                ResultMessage = "Inputs were equal.";
            }
            else
            {
                ResultMessage = "Offsets and lenghts of the diffences.";
            }
        }

        #endregion
    }
}
