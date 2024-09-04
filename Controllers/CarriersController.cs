using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Data;
using PreInfoTrans.Models;
using System.Diagnostics;

namespace PreInfoTrans.Controllers
{
    public class CarriersController : Controller
    {
        
        private readonly EpiDbContext _context;
        public CarriersController(EpiDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Сотрудник")]
        // GET: CarriersController/Create
        public async Task<IActionResult> Create(int id)
        {
            List<Countries> countries = await _context.Countries.ToListAsync();
            ViewData["Countries"] = new List<Countries>(countries);
            Carrier carrier = new Carrier { EpiId = id };
            return View(carrier);
        }

        // POST: CarriersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Unp,Country,Address,EpiId")] Carrier carrier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Carrier.Add(carrier);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Edit", "Epis", new { id = carrier.EpiId });
                }
                List<Countries> countries = await _context.Countries.ToListAsync();
                ViewData["Countries"] = new List<Countries>(countries);
                return View(carrier);
            }
            catch(Exception e)
            {
                TempData["Message"] = $"Ошибка при заполнении данных перевозчика: {e.InnerException}";
                return RedirectToAction("Edit", "Epis", new { id = carrier.EpiId });
            }
        }

        
        // GET: Carriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carrier == null)
            {
                return NotFound();
            }

            var carrier = await _context.Carrier
                .FirstOrDefaultAsync(m => m.EpiId == id);
            if (carrier == null)
            {
                return NotFound();
            }

            return View(carrier);
        }

        // POST: Carriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carrier == null)
            {
                return Problem("Entity set 'EpiDbContext.Carrier'  is null.");
            }
            var carrier = await _context.Carrier.FirstOrDefaultAsync(m => m.EpiId == id);
            if (carrier != null)
            {
                _context.Carrier.Remove(carrier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Epis", new { id = carrier.EpiId });
        }
    }
}
