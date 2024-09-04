using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Data;
using PreInfoTrans.Models;

namespace PreInfoTrans.Controllers
{
    [Authorize]
    public class TsmpsController : Controller
    {
        private readonly EpiDbContext _context;

        public TsmpsController(EpiDbContext context)
        {
            _context = context;
        }

        // GET: Tsmps/Create
        public IActionResult Create(string? docname, int? tsmptype)
        {
            Tsmp tsmp = new Tsmp() { TypeCode = (int)tsmptype };
            List<TsmpTypes> types = _context.TsmpTypes.Where(t => t.TypeCode == tsmptype).ToList();
            List<Countries> countries = _context.Countries.ToList();
            List<Brands> brands = _context.Brands.ToList();
            TsmpTypesViewModel tsmpTypesViewModel = new TsmpTypesViewModel
            {
                TsmpTypes = types,
                Brands = brands,
                Countries = countries,
                Tsmp = tsmp,
                DocName = docname
            };
            tsmp.EpiDocName = docname;
            return View(tsmpTypesViewModel);
        }

        // POST: Tsmps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,Type,TypeCode,RegNum,RegCountry,VinCode,EpiDocName")] Tsmp tsmp)
        {
            //tsmp.Type = HttpContext.Request.Form["SelectedName"].ToString().Split(':')[1];
           // tsmp = BusinessLogic.Utils.CheckType(HttpContext.Request.Form["SelectedName"].ToString(), tsmp);
            if (ModelState.IsValid)
            {
                if (tsmp.TypeCode == 30 && tsmp.VinCode.Length == 17) 
                {
                    var _vin = tsmp.VinCode?.ToUpper();
                    tsmp.VinCode = _vin;
                }
                
                _context.Add(tsmp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Epis", new { Id = Int32.Parse(tsmp.EpiDocName)});
            }
            List<TsmpTypes> types = _context.TsmpTypes.Where(t => t.TypeCode == tsmp.TypeCode).ToList();
            List<Countries> countries = _context.Countries.ToList();
            List<Brands> brands = _context.Brands.ToList();
            TsmpTypesViewModel tsmpTypesViewModel = new TsmpTypesViewModel
            {
                TsmpTypes = types,
                Brands = brands,
                Countries = countries,
                Tsmp = tsmp,
                DocName = tsmp.EpiDocName
            };
            return View(tsmpTypesViewModel);
        }

        // GET: Tsmps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tsmp == null)
            {
                return NotFound();
            }

            var tsmp = await _context.Tsmp.FindAsync(id);
            if (tsmp == null)
            {
                return NotFound();
            }
            return View(tsmp);
        }

        // POST: Tsmps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Type,TypeCode,RegNum,RegCountry,VinCode,EpiDocName")] Tsmp tsmp)
        {
            if (id != tsmp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tsmp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TsmpExists(tsmp.Id))
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
            return View(tsmp);
        }

        // GET: Tsmps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tsmp == null)
            {
                return NotFound();
            }

            var tsmp = await _context.Tsmp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tsmp == null)
            {
                return NotFound();
            }

            return View(tsmp);
        }

        // POST: Tsmps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tsmp == null)
            {
                return Problem("Entity set 'EpiDbContext.Tsmp'  is null.");
            }
            var tsmp = await _context.Tsmp.FindAsync(id);
            if (tsmp != null)
            {
                _context.Tsmp.Remove(tsmp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Epis", new { Id = Int32.Parse(tsmp.EpiDocName) });
        }

        private bool TsmpExists(int id)
        {
          return (_context.Tsmp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
