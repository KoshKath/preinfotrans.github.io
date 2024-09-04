using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Data;
using PreInfoTrans.Models;

namespace PreInfoTrans.Controllers
{
    [Authorize]
    public class EpisController : Controller
    {

        private readonly EpiDbContext _context;


        public List<Tsmp>? tsmps;
        public EpisController(EpiDbContext context)
        {
            _context = context;         
        }

        // GET: Epis
        [Authorize(Roles = "Администратор, Сотрудник, Руководитель")]
        public async Task<IActionResult> Index()
        {
            IQueryable<Epi> query = _context.Epi;

            // Если пользователь имеет роль Сотрудник, фильтруем Epi по полю CreatorId
            if (User.IsInRole("Сотрудник"))
            {
                query = query.Where(e => e.CreatorId == User.Identity.Name);
            }

            // Получаем список Epi
            List<Epi> epis = await query.ToListAsync();
            return _context.Epi != null ?
                        View(epis) :
                        Problem("Не возможно получить список ЭПИ");
        }

        [Authorize(Roles = "Администратор, Сотрудник, Руководитель")]
        public async Task<IActionResult> Isto()
        {
            return _context.Epi != null ?
                        View(await _context.Epi.Where(t =>  (t.Result == Result.Pending) || (t.Result == Result.Registration) || ((t.Result == Result.Release) && (t.RegEndDate == null))).ToListAsync()) :
                        Problem("Entity set 'EpiDbContext.Epi'  is null.");
        }

        // GET: Epis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }
            List<Tsmp> tsmpsModel = _context.Tsmp.Where(c => c.EpiDocName == epi.DocName).ToList();
            Carrier carrier = _context.Carrier.Where(c => c.EpiId == id).FirstOrDefault();
            Owner owner = _context.Owner.Where(c => c.EpiId == id).FirstOrDefault();
            epi.TsmpFormatedString = string.Join("/", tsmpsModel.Select(t => t.RegNum));

            EpiTsmsViewModel epiTsmsViewModel = new EpiTsmsViewModel
            {
                EpiDocName = epi.DocName,
                Epi = epi,
                Carrier = carrier,
                Owner = owner,
                Tsmps = tsmpsModel
            };
            return View(epiTsmsViewModel);
        }

        // GET: Epis/Print/5
        public async Task<IActionResult> Print(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            if (epi.Result == Result.Canceling)
            {
                return RedirectToAction("Index");
            }

            List<Tsmp> tsmpsModel = _context.Tsmp.Where(c => c.EpiDocName == epi.DocName).ToList();
            Carrier carrier = _context.Carrier.Where(c => c.EpiId == id).FirstOrDefault();
            Owner owner = _context.Owner.Where(c => c.EpiId == id).FirstOrDefault();
            epi.TsmpFormatedString = BusinessLogic.EpiLogic.GetTsmpFormatedString(tsmpsModel);

            EpiTsmsViewModel epiTsmsViewModel = new EpiTsmsViewModel
            {
                EpiDocName = epi.DocName,
                Epi = epi,
                Carrier = carrier,
                Owner = owner,
                Tsmps = tsmpsModel
            };
            return View(epiTsmsViewModel);
        }

        // GET: Epis/Create
        [Authorize(Roles = "Сотрудник")]
        public IActionResult Create()
        {
            var epi = new Epi();

            epi.DocDate = DateTime.Now;
            epi.Result = Result.Created;
            epi.IsPassengers = 0;
            epi.IsCrew = 0;
            epi.Targets = null;
            epi.CreatorId = User.Identity?.Name;
            _context.Add(epi);
            _context.SaveChanges();
            int id = epi.Id;
            epi.DocName = id.ToString().PadLeft(7, '0');
            _context.SaveChanges();
            return RedirectToAction(nameof(Edit), new { id = id });
        }

        // GET: Epis/Edit/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }
            if (epi.CreatorId != User.Identity?.Name) 
            {
                TempData["Message"] = $"Нельзя редактировать чужую ЭПИ!";
                return RedirectToAction("Index");
            }
            if (epi.Result != Result.Created) 
            {
                var resText = "";
                switch (epi.Result) 
                {
                    case Result.Canceling:
                        resText = "Аннулирование";
                        break;
                    case Result.Deleted:
                        resText = "Удалено";
                        break;
                    case Result.Denied:
                        resText = "Отказ регистрации";
                        break;
                    case Result.Pending:
                        resText = "Направлена в ИСТО";
                        break;
                    case Result.Refused:
                        resText = "Отказ выпуска";
                        break;
                    case Result.Registration:
                        resText = "Регистрация";
                        break;
                    case Result.Release:
                        resText = "Выпуск";
                        break;
                    case Result.Revoked:
                        resText = "Отозвана";
                        break;
                    case Result.Revoking:
                        resText = "Отзыв";
                        break;
                    default:
                        resText = "не известно";
                        break;
                }
                TempData["Message"] = $"ЭПИ в статусе {resText} редактировать нельзя!";
                return RedirectToAction("Index");
            }

            List<Tsmp> tsmpsModel = await _context.Tsmp.Where(c => c.EpiDocName == epi.DocName).ToListAsync();
            Carrier carrier = await _context.Carrier.Where(c => c.EpiId == id).FirstOrDefaultAsync();
            Owner owner = await _context.Owner.Where(c => c.EpiId == id).FirstOrDefaultAsync();
            List<Countries> countries = await _context.Countries.ToListAsync();
            var onlyCountriesNames = countries.GroupBy(n => new { n.CountryName }).Select(g => g.First()).ToList();
            EpiTsmsViewModel epiTsmsViewModel = new EpiTsmsViewModel
            {
                EpiDocName = epi.DocName,
                Carrier = carrier,
                Owner = owner,
                Countries = countries,
                Epi = epi,
                Tsmps = tsmpsModel
            };
            List<TsmpTypes> allTsmps = await _context.TsmpTypes.ToListAsync();
            // Группируем записи по полю TypeName и проецируем результат, чтобы получить только названия групп и их коды
            var uniqueTypes = allTsmps.GroupBy(t => new { t.TypeCode, t.TypeName })
                                      .Select(g => g.First())
                                      .ToList();
            ViewData["Tska"] = tsmpsModel;
            ViewData["UniqueTypes"] = uniqueTypes;
            ViewData["Countries"] = onlyCountriesNames;
            ViewData["Carrier"] = carrier != null ? carrier.GetAllFieldsAsString() : "";
            ViewData["Owner"] = owner != null ? owner.GetAllFieldsAsString() : "";
            return View(epi);
        }

        // POST: Epis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Edit(int id, Epi epi)
        {
            
            if (id != epi.Id)
            {
                return NotFound();
            }

            List<Tsmp> tfslist = await _context.Tsmp.Where(t=>t.EpiDocName==epi.DocName).ToListAsync();
            if (tfslist.Count == 0)
            {
                TempData["Message"] = $"Не добавлено ни одного ТСМП!";
                return RedirectToAction("Edit", id);
            }

            var carrier = await _context.Carrier.Where(c => c.EpiId == epi.Id).FirstOrDefaultAsync();
            if (carrier == null)
            {
                TempData["Message"] = $"Не заполнены данные о лице, осуществляющем перевозку!";
                return RedirectToAction("Edit", id);
            }

            var owner = await _context.Owner.Where(c => c.EpiId == epi.Id).FirstOrDefaultAsync();
            if (tfslist[0].TypeCode != 20) 
            {
                if (owner == null)
                {
                    TempData["Message"] = $"Не заполнены данные о лице, ответственном за использование транспортного средства!";
                    return RedirectToAction("Edit", id);
                }
            }
            

            if (Request.Form["sparePartsOption"] == "yes" && string.IsNullOrEmpty(epi.SpareParts))
            {
                TempData["Message"] = $"Зап. части/оборудование обязательны для заполнения, если выбрано 'Да'.";
                return RedirectToAction("Edit", id);
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (BusinessLogic.EpiLogic.CheckAllTargetsOk(tfslist))
                    {
                        epi.TsmpFormatedString = BusinessLogic.EpiLogic.GetTsmpFormatedString(tfslist); // строка с номерами ТСМП
                        epi.Targets = BusinessLogic.EpiLogic.CheckTargets(tfslist, epi.DirectionIn); // Проверка страны принадлежности ТС и направления
                        _context.Update(epi);
                        await _context.SaveChangesAsync();
                    }
                    else 
                    {
                        TempData["Message"] = $"В одной ЭПИ не могут содержаться ТС из стран ЕАЭС и стран, не являющихся членами ЕАЭС!";
                        return RedirectToAction("Edit", id);
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpiExists(epi.Id))
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
            return View(epi);
        }

        // GET: Epis/Delete/5
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            return View(epi);
        }

        // POST: Epis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                _context.Epi.Remove(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Epis/Release/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Releasing(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            return View(epi);
        }

        // GET: Epis/Accept/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            return View(epi);
        }

        // POST: Epis/Accept/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> AcceptConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                epi.Result = Result.Registration;
                epi.RegDateTime = DateTime.Now;
                epi.RegNumTDTS = HttpContext.Request.Form["RegNumTDTS"];
                _context.Epi.Update(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: Epis/Accept/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> DenyConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                epi.Result = Result.Denied;
                epi.RegDateTime = DateTime.Now;
                epi.CancelReason = HttpContext.Request.Form["CancelReason"];
                _context.Epi.Update(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> RevokingConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                epi.Result = Result.Revoking;
                epi.RegComleteDateTime = DateTime.Now;
                epi.CancelReason = HttpContext.Request.Form["CancelReason"];
                _context.Epi.Update(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> RefuseConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                epi.Result = Result.Refused;
                epi.RegComleteDateTime = DateTime.Now;
                epi.CancelReason = HttpContext.Request.Form["CancelReason"];
                _context.Epi.Update(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Epis/Complete/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Complete(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);

            if (epi == null)
            {
                return NotFound();
            }
            if (epi.Result == Result.Release) return View(epi);
            TempData["Message"] = "Не зарегистрированную в ИСТО ЭПИ выпустить нельзя!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> CompleteConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                epi.Result = Result.Release;
                // Получаем и преобразуем дату
                string RegEndDateString = HttpContext.Request.Form["RegEndDate"];
                DateTime RegEndDate;
                if (DateTime.TryParse(RegEndDateString, out RegEndDate))
                {
                    epi.RegEndDate = RegEndDate;
                }
                else
                {
                    // Обработка ошибки: неправильный формат даты
                    ModelState.AddModelError("Дата выпуска", "Неверный формат даты.");
                    return View(epi); // Вернуть модель с ошибками валидации
                }
                epi.RegCompleteTDTS = HttpContext.Request.Form["RegCompleteTDTS"];
                _context.Epi.Update(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Epis/Release/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Release(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);

            if (epi == null)
            {
                return NotFound();
            }
            if (epi.Result == Result.Registration) return View(epi);
            TempData["Message"] = "Не зарегистрированную в ИСТО ЭПИ выпустить нельзя!";
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> ReleaseConfirmed(int id)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi'  is null.");
            }
            var epi = await _context.Epi.FindAsync(id);
            if (epi != null)
            {
                epi.Result = Result.Release;
                epi.RegComleteDateTime = DateTime.Now;
                epi.RegNumOutTDTS = HttpContext.Request.Form["RegNumOutTDTS"];
                // Получаем и преобразуем дату
                string TemporaryInDateString = HttpContext.Request.Form["TemporaryInDate"];
                if (TemporaryInDateString.Length > 0) 
                {
                    DateTime TemporaryInDate;
                    if (DateTime.TryParse(TemporaryInDateString, out TemporaryInDate))
                    {
                        epi.TemporaryInDate = TemporaryInDate;
                    }
                    else
                    {
                        // Обработка ошибки: неправильный формат даты
                        ModelState.AddModelError("Дата выпуска", "Неверный формат даты.");
                        return View(epi); // Вернуть модель с ошибками валидации
                    }
                }
                _context.Epi.Update(epi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Общий метод для изменения статуса
        [Authorize(Roles = "Сотрудник")]
        private async Task<IActionResult> ChangeStatus(int id, Result newStatus, string cancelReason)
        {
            if (_context.Epi == null)
            {
                return Problem("Entity set 'EpiDbContext.Epi' is null.");
            }

            var epi = await _context.Epi.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }

            epi.Result = newStatus;
            epi.CancelReason = cancelReason;
            _context.Epi.Update(epi);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Epis/Send/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Send(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            if (epi.Result != Result.Created) 
            {
                TempData["Message"] = "ЭПИ не в статусе Создана нельзя направить в ИСТО!";
                return RedirectToAction("Index");
            }
            return View(epi);
        }
        // GET: Epis/Cancel/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);
            
            if (epi == null)
            {
                return NotFound();
            }
            if (epi.Result == Result.Created) return View(epi);
            TempData["Message"] = "ЭПИ, принятую в работу, аннулировать нельзя!";
            return RedirectToAction("Index");
            
        }
        // GET: Epis/Deny/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Deny(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);

            if (epi == null)
            {
                return NotFound();
            }
            if (epi.Result == Result.Pending) return View(epi);
            TempData["Message"] = "Не направленную в ИСТО ЭПИ отказать нельзя!";
            return RedirectToAction("Index");

        }

        // GET: Epis/Refuse/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Refuse(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);

            if (epi == null)
            {
                return NotFound();
            }
            if (epi.Result == Result.Registration) return View(epi);
            TempData["Message"] = "Не зарегистрированную в ИСТО ЭПИ отозвать нельзя!";
            return RedirectToAction("Index");

        }

        // GET: Epis/Revoking/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Revoking(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);

            if (epi == null)
            {
                return NotFound();
            }
            if (epi.Result == Result.Registration) return View(epi);
            TempData["Message"] = "Не зарегистрированную в ИСТО ЭПИ отозвать нельзя!";
            return RedirectToAction("Index");

        }
        // GET: Epis/Revoke/5
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> Revoke(int? id)
        {
            if (id == null || _context.Epi == null)
            {
                return NotFound();
            }

            var epi = await _context.Epi.FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            if (epi.Result != Result.Pending) 
            {
                TempData["Message"] = "Можно отзывать только ЭПИ, направленные в ИСТО!";
                return RedirectToAction("Index");
            }

            return View(epi);
        }
        // POST: Epis/Send/5
        [HttpPost, ActionName("Send")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> SendConfirmed(int id)
        {

            return await ChangeStatus(id, Result.Pending, string.Empty);
        }

        // POST: Epis/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> CancelConfirmed(int id, string cancelReason)
        {
            return await ChangeStatus(id, Result.Canceling, cancelReason);
        }

        // POST: Epis/Revoke/5
        [HttpPost, ActionName("Revoke")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Сотрудник")]
        public async Task<IActionResult> RevokeConfirmed(int id)
        {
            return await ChangeStatus(id, Result.Revoked, string.Empty);
        }

        [Authorize(Roles = "Администратор, Сотрудник, Руководитель")]
        public async Task<IActionResult> Charts()
        {
            var countsByResult = Enum.GetValues(typeof(Result))
            .Cast<Result>()
            .Select(r => new EpiResultViewModel
            {
                Result = r,
                Count = 0,
                Percent = 0.0,
                CssRepresentation = "" // или какой-то начальный стиль
            })
            .ToList();
            var countsByResultData = await _context.Epi
                .Where(e => !e.IsDeleted) // исключаем удаленные документы
                .GroupBy(e => e.Result) // группируем по полю Result
                .Select(g => new EpiResultViewModel
                {
                    Result = (Result)g.Key, 
                    Count = g.Count() // количество документов в группе
                })
                .ToListAsync();
            foreach (var count in countsByResultData)
            {
                var item = countsByResult.FirstOrDefault(i => i.Result == count.Result);
                if (item != null)
                {
                    item.Count = count.Count;
                }
            };
            var episCountByCreator = _context.Epi
            .Where(e => e.CreatorId != null) // берем поля с заполненным CreatorId
            .GroupBy(e => e.CreatorId) // группируем по полю CreatorId
            .Select(g => new EpiUsersViewModel 
            { 
                UserName = g.Key, 
                EpisCount = g.Count() // количество документов у пользователя
            })
            .ToList();

            var chartsViewModel = new ChartsViewModel
            {
                epiResultViewModels = BusinessLogic.Utils.CalculateCircleDiagram(countsByResult),
                epiUsersViewModels = BusinessLogic.Utils.CalculateBars(episCountByCreator),
                barWidth = (int)((600 - (episCountByCreator.Count+1)*20) / episCountByCreator.Count) - 1
            };
            return View(chartsViewModel);

        }
        [Authorize(Roles = "Администратор, Сотрудник, Руководитель")]
        public IActionResult Reports()
        {
            return View();
        }

        [Authorize(Roles = "Администратор, Сотрудник, Руководитель")]
        public async Task<IActionResult> Report1()
        {
            IQueryable<Epi> query = _context.Epi;
            ViewData["IsEmployee"] = "false";
            // Если пользователь имеет роль Сотрудник, фильтруем Epi по полю CreatorId
            if (User.IsInRole("Сотрудник"))
            {
                query = query.Where(e => e.CreatorId == User.Identity.Name);
                ViewData["IsEmployee"] = "true";
            }
            // Получаем список Epi
            List<Epi> epis = await query.Where(e => e.Result == Result.Release 
                                                && (e.Targets == Targets.TemporaryIn || e.Targets == Targets.TemporaryOut)
                                                && (e.TemporaryInDate != null)
                                                && (e.TemporaryInDate < DateTime.Now)
                                                && (e.RegCompleteTDTS == null || e.RegCompleteTDTS == "")).ToListAsync();
            return _context.Epi != null ?
                        View(epis) :
                        Problem("Не возможно получить список ЭПИ");
        }

        [Authorize(Roles = "Администратор, Сотрудник, Руководитель")]
        public async Task<IActionResult> Report2()
        {
            IQueryable<Epi> query = _context.Epi;
            ViewData["IsEmployee"] = "false";
            // Если пользователь имеет роль Сотрудник, фильтруем Epi по полю CreatorId
            if (User.IsInRole("Сотрудник"))
            {
                query = query.Where(e => e.CreatorId == User.Identity.Name);
                ViewData["IsEmployee"] = "true";
            }
            // Получаем список Epi
            DateTime sevenDaysFromNow = DateTime.Now.AddDays(7);
            DateTime now = DateTime.Now;

            List<Epi> epis = await query.Where(e => e.Result == Result.Release
                                                && (e.Targets == Targets.TemporaryIn || e.Targets == Targets.TemporaryOut)
                                                && (e.TemporaryInDate != null)
                                                && (e.TemporaryInDate < sevenDaysFromNow)
                                                && (e.TemporaryInDate > now)
                                                && (e.RegCompleteTDTS == null || e.RegCompleteTDTS == "")).ToListAsync();

            return _context.Epi != null ?
                        View(epis) :
                        Problem("Не возможно получить список ЭПИ");
        }

        private bool EpiExists(int id)
        {
            return (_context.Epi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
