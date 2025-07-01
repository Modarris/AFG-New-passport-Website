using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AFG_New_passport_Website.Controllers
{
    public class CompanyOfficialController : Controller
    {
        private readonly WebDbContext _context;

        public CompanyOfficialController(WebDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create() => View();


        [HttpPost]
        public IActionResult Create(CompanyOfficial official)
        {
            var existingOfficial = _context.CompanyOfficials
       .FirstOrDefault(o => o.Id == official.Id);

            if (existingOfficial != null)
            {
                string message = $"آی‌دی <span style='color:red;'>\"{existingOfficial.Id}\"</span> قبلاً برای " + $"<span style='color:red;'>\"{existingOfficial.Name}\"</span>  ثبت شده است.";
                ViewBag.DuplicateMessage = message;
                return View(official);
            }
            if (ModelState.IsValid)
            {
                _context.CompanyOfficials.Add(official);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(official);
        }

        public IActionResult List()
        {
            var data = _context.CompanyOfficials.ToList();
            return View(data);
        }
    }

}
