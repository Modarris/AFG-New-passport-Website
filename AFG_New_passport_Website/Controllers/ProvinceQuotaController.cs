using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static QRCoder.Base64QRCode;

namespace AFG_New_passport_Website.Controllers
{
    public class ProvinceQuotaController : Controller
    {
        private readonly WebDbContext _context;

        public ProvinceQuotaController(WebDbContext context)
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

            return View(new ProvinceQuotaViewModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProvinceQuotaViewModel model)
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
            var quota = _context.ProvinceQuotas
                .FirstOrDefault(pq => pq.ProcessingCityId == model.SelectedCityId);

            if (quota == null)
            {
                // 
                quota = new ProvinceQuota
                {
                    ProcessingCityId = model.SelectedCityId,
                    DailyQuota = model.DailyQuota,
                    EndTime = model.EndTime
                };
                _context.ProvinceQuotas.Add(quota);
            }
            else
            {
                //
                quota.DailyQuota = model.DailyQuota;
                quota.EndTime = model.EndTime;
                _context.ProvinceQuotas.Update(quota);
            }

            _context.SaveChanges();

            TempData["SuccessMessage"] = "تعداد نوبت با موفقیت ثبت شد.";

            return RedirectToAction("List");

        }

        public IActionResult List()
        {
            var quotas = _context.ProvinceQuotas
       .Include(q => q.ProcessingCity) 
       .ToList();

            return View(quotas);
        }
        /*  [HttpPost]
          public IActionResult Create(ProvinceQuotaViewModel model)
          {
              if (!ModelState.IsValid)
              {

                  return View(model);
              }

              var quota = new ProvinceQuota
              {

                  DailyQuota = model.DailyQuota,
                  IsActive = model.IsActive
              };

              _context.ProvinceQuotas.Add(quota);
              _context.SaveChanges();

              return RedirectToAction("List");
          }

          public IActionResult List()
          {
              var quotas = _context.ProvinceQuotas.ToList();
              return View(quotas);
          }


          public IActionResult Edit(int id)
          {
              var quota = _context.ProvinceQuotas.Find(id);
              if (quota == null) return NotFound();

              var model = new ProvinceQuotaViewModel
              {


                  DailyQuota = quota.DailyQuota,
                  IsActive = quota.IsActive,

              };

              return View(model);
          }

          [HttpPost]
          public IActionResult Edit(int id, ProvinceQuotaViewModel model)
          {
              if (!ModelState.IsValid)
              {

                  return View(model);
              }

              var quota = _context.ProvinceQuotas.Find(id);
              if (quota == null) return NotFound();



              quota.DailyQuota = model.DailyQuota;
              quota.IsActive = model.IsActive;

              _context.SaveChanges();
              return RedirectToAction("List");
          }


          public IActionResult Delete(int id)
          {
              var quota = _context.ProvinceQuotas.Find(id);
              if (quota == null) return NotFound();

              _context.ProvinceQuotas.Remove(quota);
              _context.SaveChanges();

              return RedirectToAction("List");
          }
          [HttpPost]
          public IActionResult DeleteConfirmed(int id)
          {
              var quota = _context.ProvinceQuotas.Find(id);
              if (quota == null) return NotFound();

              _context.ProvinceQuotas.Remove(quota);
              _context.SaveChanges();

              return RedirectToAction("List");
          }*/
    }
}

