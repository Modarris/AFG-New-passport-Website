using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace AFG_New_passport_Website.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly WebDbContext _context;

        public BankAccountsController(WebDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {

            var activeCities = _context.Cities
            .Where(c => c.IsDocumentProcessPlace)
            .ToList();

            ViewBag.ActiveCities = new SelectList(activeCities, "Id", "NameLocal");

            return View(new BankAccountViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BankAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {

                var activeCities = _context.Cities
                    .Where(c => c.IsDocumentProcessPlace)
                    .ToList();

                ViewBag.ActiveCities = new SelectList(activeCities, "Id", "NameLocal");

                return View(model);
            }

            // 
            var accounts = _context.BankAccounts
                .FirstOrDefault(pq => pq.ProcessingCityId == model.SelectedCityId);

           
                // 
                accounts = new BankAccounts
                {
                    ProcessingCityId = model.SelectedCityId,
                    AccountNumber = model.AccountNumber,
                    AccountHolder = model.AccountHolder,
                    Fee = model.Fee,
                    LocationCode = model.LocationCode,
                    OrgCode = model.OrgCode,
                    PassportBookletRevenueCode = model.PassportBookletRevenueCode,
                    PassportRevenueCode = model.PassportRevenueCode,
                    UniteCode = model.UniteCode,
                    CamercialBankAccount = model.CamercialBankAccount
                };
                _context.BankAccounts.Add(accounts);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "تعداد نوبت با موفقیت ثبت شد.";

                return RedirectToAction("Index");
            
        }

        public IActionResult List()
        {
            var accounts = _context.BankAccounts
       .Include(q => q.ProcessingCity)
       .ToList();

            return View(accounts);
        }
        






        // GET: List bank accounts
        public IActionResult Index()
        {
            var bankAccounts = _context.BankAccounts
                .Include(b => b.ProcessingCity)
                .ToList();

            return View(bankAccounts);
        }

        // GET: Edit form
      /*  [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new BankAccountViewModel
            {
                BankAccount = new BankAccounts(),
                Cities = _context.Cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(model);

        }

        // POST: Save edited bank account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BankAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // اینجا تبدیل درست به SelectListItem
                model.Cities = _context.Cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NameLocal
                }).ToList();

                return View(model);
            }

            _context.BankAccounts.Update(model.BankAccount);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }*/


        // GET: Delete confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var bankAccount = _context.BankAccounts
                .Include(b => b.ProcessingCity)
                .FirstOrDefault(b => b.Id == id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: Confirm delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bankAccount = _context.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _context.BankAccounts.Remove(bankAccount);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
