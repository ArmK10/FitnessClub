using FitnessClub.Models;
using FitnessClub.Models.Data;
using FitnessClub.ViewModels.Subscriptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Controllers
{
    //[Authorize(Roles = "admin, registeredUser")]
    public class SubscriptionsController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;

        public SubscriptionsController(
            AppCtx context,
            UserManager<User> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            // находим информацию о пользователе, который вошел в систему по его имени
            //// IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            // через контекст данных получаем доступ к таблице базы данных Subscriptions
            var appCtx = _context.Subscriptions
                .Include(s => s.Service)
                .OrderBy(f => f.Price);          // сортируем все записи по имени услуг

            // возвращаем в представление полученный список записей
            return View(await appCtx.ToListAsync());
        }

        // GET: Subscriptions/Create
        public async Task<IActionResult> CreateAsync()
        {
            // при отображении страницы заполняем элемент "выпадающий список" формами обучения
            // при этом указываем, что в качестве идентификатора используется поле "Id"
            // а отображать пользователю нужно поле "FormOfEdu" - название формы обучения
            ViewData["IdService"] = new SelectList(_context.Services, "Id", "ServiceName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel model)
        {

            if (_context.Subscriptions
                .Where(
                f => f.Price == model.Price &&
                f.CountVisits == model.CountVisits &&
                f.CountDays == model.CountDays &&
                f.IdService == model.IdService).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный абонемент уже существует");
            }

            if (ModelState.IsValid)
            {
                Subscription subscription = new()
                {
                    Price = model.Price,
                    CountVisits = model.CountVisits,
                    CountDays = model.CountDays,
                    IdService = model.IdService
                };

                _context.Add(subscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdService"] = new SelectList(
                _context.Services,"Id", "ServiceName", model.IdService);
            return View(model);
        }

        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            EditSubscriptionViewModel model = new()
            {
                Id = subscription.Id,
                Price = subscription.Price,
                CountVisits = subscription.CountVisits,
                CountDays = subscription.CountDays,
                IdService = subscription.IdService 
            };

            ViewData["IdService"] = new SelectList(
                _context.Services, "Id", "ServiceName", subscription.IdService);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditSubscriptionViewModel model)
        {
            if (_context.Subscriptions
                .Where(
                f => f.Price == model.Price &&
                f.CountVisits == model.CountVisits &&
                f.CountDays == model.CountDays &&
                f.IdService == model.IdService).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный абонемент уже существует");
            }

            Subscription subscription = await _context.Subscriptions.FindAsync(id);

            if (id != subscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    subscription.Price = model.Price;
                    subscription.CountVisits = model.CountVisits;
                    subscription.CountDays = model.CountDays;
                    subscription.IdService = model.IdService;
                    _context.Update(subscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.Id))
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
            ViewData["IdService"] = new SelectList(
                _context.Services, "Id", "ServiceName", subscription.IdService);
            return View(model);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        private bool SubscriptionExists(short id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}
