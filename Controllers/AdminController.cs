using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Data;
using PreInfoTrans.Models;
namespace PreInfoTrans.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        // GET: AdminController
        public ActionResult Index()
        {
            // Выполните объединение таблиц AspNetUsers и AspNetUserRoles
            var usersWithRoles = (from user in _context.Users
                                  join userRole in _context.UserRoles on user.Id equals userRole.UserId into userRolesGroup
                                  from userRole in userRolesGroup.DefaultIfEmpty() // Левое соединение
                                  join role in _context.Roles on userRole.RoleId equals role.Id into rolesGroup
                                  from role in rolesGroup.DefaultIfEmpty() // Левое соединение
                                  select new UserWithRoleViewModel
                                  {
                                      UserId = user.Id,
                                      UserName = user.UserName,
                                      Email = user.Email,
                                      RoleName = role == null ? "Не назначена" : role.Name
                                  }).ToList();

            return View(usersWithRoles);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public IActionResult Edit(string userId)
        {
            // Получаем пользователя по его идентификатору
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            // Получаем список ролей
            var roles = _context.Roles.ToList();

            // Получаем текущую роль пользователя
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);

            // Формируем список ролей в виде SelectList
            var roleList = new SelectList(roles, "Id", "Name", userRole?.RoleId);

            // Создаем модель представления
            var viewModel = new UserEditViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                
                RoleId = userRole?.RoleId,
                Roles = roleList
            };

            return View(viewModel);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model, string roleId)
        {
            // Присваиваем значение RoleId из параметра roleId модели
            model.RoleId = roleId;
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid)
            {
                // Если модель не прошла валидацию, возвращаем представление снова с модель   
                return View(model);
            }
            
            // Получаем пользователя по его идентификатору
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Получаем текущую роль пользователя
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == model.UserId);

            if (userRole != null)
            {
                // Удаляем текущую роль пользователя
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(model.RoleId))
            {
                // Добавляем новую роль пользователю
                var newUserRole = new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = model.RoleId
                };
                _context.UserRoles.Add(newUserRole);
                await _context.SaveChangesAsync();
            }

            // После изменения роли пользователя перенаправляем на страницу списка пользователей
            return RedirectToAction("Index");
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
