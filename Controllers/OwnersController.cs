using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Data;
using PreInfoTrans.Models;

namespace PreInfoTrans.Controllers
{
    public class OwnersController : Controller
    {
        private readonly EpiDbContext _context;

        public OwnersController(EpiDbContext context)
        {
            _context = context;
        }


        // GET: Owners/Create
        public IActionResult Create(int id)
        {
            var countries = _context.Countries.ToList();
            ViewData["Countries"] = countries;
            Owner owner = new Owner() { EpiId = id,
                DateIssue = DateTime.Now.AddDays(-1)  };// Вчерашняя дата
            return View(owner);
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Passport,IdNumber,DateIssue,Country,Address,EpiId")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Epis", new { id = owner.EpiId });
            }
            var countries = _context.Countries.ToList();
            ViewData["Countries"] = countries;
            return View(owner);
        }

  
        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner
                .FirstOrDefaultAsync(m => m.EpiId == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Owner == null)
            {
                return Problem("Entity set 'EpiDbContext.Owner'  is null.");
            }
            var _id = id;
            var owner = await _context.Owner.FirstOrDefaultAsync(c => c.EpiId == id);
            if (owner != null)
            {
                _context.Owner.Remove(owner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Epis", new { id = _id });
        }

        private bool OwnerExists(int id)
        {
          return (_context.Owner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
