using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AFG_New_passport_Website.Controllers
{
    public class PaymentTypesController : Controller
    {

        private readonly WebDbContext _context;
        public PaymentTypesController(WebDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var paymentTypes = _context.PaymentTypes.ToList();
            return View(paymentTypes);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
             _context.PaymentTypes.Add(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(id);
            if(paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                _context.PaymentTypes.Update(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }
     

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var paymentType = _context.PaymentTypes.FirstOrDefault(p => p.Id == id);
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var paymentType = _context.PaymentTypes.Find(id);
            if (paymentType == null)
            {
                return NotFound();
            }
            _context.PaymentTypes.Remove(paymentType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
