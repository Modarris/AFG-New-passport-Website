
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModel = AFG_New_passport_Website.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SelectPdf;
using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using System.Drawing.Imaging;
using BarcodeLib;
using QRCoder;
using AFG_New_passport_Website.Models.Domain;

using Application = AFG_New_passport_Website.Models.Domain.Application;
using ProfilePhoto = AFG_New_passport_Website.Models.Domain.ProfilePhoto;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZXing.QrCode.Internal;
namespace AFG_New_passport_Website.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly WebDbContext _context;
        private const int COMMERCIAL_TYPE_ID = 4;
        private static readonly Random random = new Random();
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private string GetClientIp()
        {
            var ip = HttpContext.Connection.RemoteIpAddress;

            if (ip == null)
                return "unknown";
            if (ip.IsIPv4MappedToIPv6)
                ip = ip.MapToIPv4();

            return ip.ToString();
        }

        public RegistrationController(WebDbContext context)
        {
            _context = context;

        }
        //----------------- Form for user-------------------
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ViewModel
            {
                Genders = _context.Genders.ToList(),
                AllProfessions = _context.Professions
                    .Where(p => p.IsActive)
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.NameLocal
                    }).ToList(),
                PassportTypes = _context.PassportTypes.ToList(),
                PassportDurations = _context.PassportDurations.ToList(),
                PaymentTypes = _context.PaymentTypes.ToList(),
                AppoinmentTypes = _context.AppoinmentTypes.ToList(),
                UserAddresses = _context.UserAddresses.ToList(),
                IdentityDocumentTypes = _context.IdentityDocumentTypes.ToList(),
                AllCitiesForBirth = _context.Cities.ToList(),
                ActiveCitiesForProcessing = _context.Cities
                    .Where(c => c.IsDocumentProcessPlace)
                    .ToList(),
                PostalAddresses = _context.PostalAddresses
                    .Where(pa => pa.IsActive)
                    .ToList()
            };


            return View(model);
        }

        //Appointment
        private DateTime GetAppointmentDate(int cityId)
        {
            var now = DateTime.Now;

            // Get province quota
            var quota = _context.ProvinceQuotas.FirstOrDefault(q => q.ProcessingCityId == cityId);
            if (quota == null)
                throw new Exception("Quota not set.");

            // Get active public holidays
            var holidays = _context.PublicHolidays.Where(h => h.IsActive).Select(h => h.HolidayDate.Date).ToList();

            // Default end time if not set
            var endTime = quota.EndTime ?? new TimeSpan(12, 0, 0);

            // Get last appointment date
            var lastAppointment = _context.Appointments
                .Where(a => a.ProcessingCityId == cityId)
                .OrderByDescending(a => a.SlotDate)
                .Select(a => a.SlotDate.Date)
                .FirstOrDefault();

            // Start from today or last appointment date
            DateTime checkDate = lastAppointment > now.Date ? lastAppointment : now.Date;

            // Move to next day if past quota end time
            if (checkDate == now.Date && now.TimeOfDay >= endTime)
                checkDate = checkDate.AddDays(1);

            // Find next available date
            while (true)
            {
                // Skip Fridays and holidays
                if (checkDate.DayOfWeek == DayOfWeek.Friday || holidays.Contains(checkDate))
                {
                    checkDate = checkDate.AddDays(1);
                    continue;
                }

                // Adjust quota for Thursday
                bool isThursday = checkDate.DayOfWeek == DayOfWeek.Thursday;
                int dailyQuota = isThursday ? quota.DailyQuota / 2 : quota.DailyQuota;

                // Count used slots
                int used = _context.Appointments.Count(a => a.ProcessingCityId == cityId && a.SlotDate.Date == checkDate);

                // If available, break
                if (used < dailyQuota)
                    break;

                checkDate = checkDate.AddDays(1);
            }

            return checkDate;
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------
        // Create rundom code
        public static string GenerateCustomCode()
        {
            DateTime birthDate = DateTime.Now; 
            var guid = Guid.NewGuid().ToString();
            var code1 = guid.Replace("-", "").ToUpper();
            var code2 = "P0" + code1.Substring(16, 16);
            var dayStr = birthDate.Day.ToString("D2");
            return code2 + dayStr;
        }
        //Barcode
        public static string GenerateBarcodeImage(string code)
        {
            using (var barcode = new BarcodeLib.Barcode())
            {
                barcode.IncludeLabel = false;
                var backgroundColor = Color.FromArgb(248, 249, 250);
                var img = barcode.Encode(TYPE.CODE93, code, Color.Black, backgroundColor, 410, 30);

                using (var ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        //QR code
        public static string GenerateQRCodeImage(string code, string dName, string fatherName)
        {
            string qrText = $"کد: {code}\nنام: {dName}\nنام پدر: {fatherName}";

            using (var qrGenerator = new QRCoder.QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCoder.QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCoder.QRCode(qrCodeData))
                {
                    using (var bitmap = qrCode.GetGraphic(4, Color.Black, Color.FromArgb(248, 249, 250), true))
                    {
                        using (var ms = new MemoryStream())
                        {
                            bitmap.Save(ms, ImageFormat.Png);
                            return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
        }

        // Search data by code
        [HttpGet]
        public IActionResult ProfileByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("کد بارکد معتبر نیست.");

            var profile = _context.Profiles.FirstOrDefault(p => p.Code == code);

            if (profile == null)
                return NotFound("پروفایل با این کد یافت نشد.");

            return View(profile);
        }


        //Preview
        [HttpPost]
        public async Task<IActionResult> Preview(ViewModel model)
        {



            //  GenderId 
            var genders = _context.Genders.ToList();
            var gender = genders.FirstOrDefault(g => g.Id == model.GenderId);
            model.GenderDName = gender != null ? $"{gender.DName} | {gender.EName}" : "نامشخص";

            //  ProfessionId
            var professions = _context.Professions.ToList();
            var profession = professions.FirstOrDefault(p => p.Id == model.ProfessionId);
            model.ProfessionNameLocal = profession != null ? profession.NameLocal : "نامشخص";
            //  PostalAddressId

            var postalAddresses = _context.PostalAddresses.Where(p => p.IsActive).ToList();
            var postal = postalAddresses.FirstOrDefault(p => p.Id == model.PostalAddressId);
            model.PostalAddressName = postal != null ? postal.Name : "نامشخص";
            // 
            var passportType = _context.PassportTypes.FirstOrDefault(p => p.Id == model.SelectedPassportTypeId);
            model.PassportType = passportType ?? new PassportType { Type = "نامشخص" };

            // 
            var passportDuration = _context.PassportDurations.FirstOrDefault(p => p.Id == model.SelectedPassportDurationId);
            model.PassportDuration = passportDuration ?? new PassportDuration { Name = "نامشخص" };

            // 
            var paymentType = _context.PaymentTypes.FirstOrDefault(p => p.Id == model.SelectedPaymentTypeId);
            model.PaymentType = paymentType ?? new PaymentType { Type = "نامشخص" };
            // 


            var identityDocType = _context.IdentityDocumentTypes
         .FirstOrDefault(x => x.Id == model.IdentityDocumentTypeId);

            model.Name = identityDocType != null ? identityDocType.Name : "نامشخص";

            model.BirthCityNameLocal = _context.Cities
         .FirstOrDefault(c => c.Id == model.SelectedBirthCityId)?.NameLocal ?? "نامشخص";

            model.ProcessingCityNameLocal = _context.Cities
                .FirstOrDefault(c => c.Id == model.SelectedProcessingCityId)?.NameLocal ?? "نامشخص";



            HttpContext.Session.SetString("FormData", JsonConvert.SerializeObject(model));

            return View(model); // 


        }

        //Edit preview
        [HttpGet]
        public IActionResult EditFromPreview()
        {
            var formDataJson = HttpContext.Session.GetString("FormData");
            if (string.IsNullOrEmpty(formDataJson))
            {

                return RedirectToAction("Create");
            }

            var model = JsonConvert.DeserializeObject<ViewModel>(formDataJson);



            model.AllProfessions = _context.Professions
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.NameLocal })
                .ToList();

            model.Genders = _context.Genders.ToList();

            model.PostalAddresses = _context.PostalAddresses
                .Where(p => p.IsActive)
                .ToList();

            model.PassportTypes = _context.PassportTypes.ToList();

            model.PassportDurations = _context.PassportDurations.ToList();

            model.PaymentTypes = _context.PaymentTypes.ToList();

            model.AppoinmentTypes = _context.AppoinmentTypes.ToList();

            model.IdentityDocumentTypes = _context.IdentityDocumentTypes.ToList();

            model.AllCitiesForBirth = _context.Cities.ToList();

            model.ActiveCitiesForProcessing = _context.Cities.ToList();



            return View("Create", model);
        }

        //Tarifa code
        private async Task<List<string>> GenerateTarifaCodesAsync(int count)
        {
            // کد شروع به‌صورت رشته با صفرهای اول
            string startCodeStr = "0005126140000";
            long startCode = long.Parse(startCodeStr);

            // گرفتن بزرگ‌ترین کد از دیتابیس (رشته‌ای)، تبدیل به عدد برای مقایسه
            var maxCodeStr = await _context.Tarifas
                                .OrderByDescending(t => t.CPDTarifaCode)
                                .Select(t => t.CPDTarifaCode)
                                .FirstOrDefaultAsync();

            long maxCode = 0;
            if (!string.IsNullOrEmpty(maxCodeStr))
            {
                long.TryParse(maxCodeStr, out maxCode);
            }

            long lastCode = maxCode > startCode ? maxCode : startCode;

            List<string> codes = new List<string>();

            for (int i = 1; i <= count; i++)
            {
                long nextCode = lastCode + i;
                codes.Add(nextCode.ToString().PadLeft(13, '0')); // اضافه کردن صفرهای اول
            }

            return codes;
        }


        //------------ Create section and save data in database--------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ViewModel model, IFormFile ProfilePhoto, string BirthDateShamsi, string CaptchaInput)
        {
            if (ProfilePhoto == null || ProfilePhoto.Length == 0)
            {
                ModelState.AddModelError("", "لطفاً عکس پروفایل را آپلود کنید.");
                return View(model);
            }

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(ProfilePhoto.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                ModelState.AddModelError("", "فرمت عکس باید jpg، jpeg یا png باشد.");
                return View(model);
            }

            long maxFileSize = 10 * 1024 * 1024; // 10MB
            if (ProfilePhoto.Length > maxFileSize)
            {
                ModelState.AddModelError("", "حجم عکس نباید بیشتر از 2 مگابایت باشد.");
                return View(model);
            }


            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile-pictures");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid().ToString() + ext;
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ProfilePhoto.CopyToAsync(fileStream);
            }

            var profile = new Profile
            {

                DName = model.DName,
                EName = model.EName,
                DLastName = model.DLastName,
                ELastName = model.ELastName,
                DFatherName = model.DFatherName,
                EFatherName = model.EFatherName,
                DGrandFatherName = model.DGrandFatherName,
                EGrandFatherName = model.EGrandFatherName,
                DateOfBirth = model.DateOfBirth,
                Height = model.Height,
                NIDNumber = model.NIDNumber,
                GenderId = model.GenderId,
                ProfessionId = model.ProfessionId,
                BirthCityId = model.SelectedBirthCityId,
                ProcessingCityId = model.SelectedProcessingCityId,
                IdentityDocumentTypeId = model.IdentityDocumentTypeId,
            };
            if (string.IsNullOrEmpty(profile.Code))
            {
                profile.Code = GenerateCustomCode();
            }

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            //CompanyInfo
            var companyInfo = new CompanyInfo
            {
                ProfileId = profile.Profile_Id,
                Position = model.Position,
                LicenseNumber = model.LicenseNumber,
                TIN = model.TIN,
                IssueDate = model.IssueDate,
                ExpiryDate = model.ExpiryDate,
                CompanyAddress = model.CompanyAddress
            };
            _context.CompanyInfos.Add(companyInfo);
            await _context.SaveChangesAsync();

            //  UserAddress
            string fullPhone = "+93" + model.PrimaryPhone;
            var userAddress = new UserAddress
            {
                AddressDetails = model.AddressDetails,
                PrimaryPhone = model.PrimaryPhone,
                SecondaryPhone = model.SecondaryPhone,
                ProfileId = profile.Profile_Id,
                PostalAddressId = model.PostalAddressId
            };
            _context.UserAddresses.Add(userAddress);
            await _context.SaveChangesAsync();

            //  Application
            var application = new Application
            {
                ProfileId = profile.Profile_Id,
                PassportTypeId = model.SelectedPassportTypeId,
                PassportDurationId = model.SelectedPassportDurationId,
                PaymentTypeId = model.SelectedPaymentTypeId,
                AppoinmentTypeId = model.SelectedAppoinmentTypeId,
                UserAddressId = userAddress.Address_Id
            };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            //  ProfilePhoto
            var profilePhoto = new ProfilePhoto
            {
                ProfileId = profile.Profile_Id,
                PhotoPath = "/images/profile-pictures/" + fileName,
                UploadDate = DateTime.Now
            };
            _context.ProfilePhotos.Add(profilePhoto);
            await _context.SaveChangesAsync();


            // Get ProcessingCityId from Profile
            var processingCityId = profile.ProcessingCityId;
            var bankAccount = _context.BankAccounts
                .FirstOrDefault(b => b.ProcessingCityId == processingCityId);

            if (bankAccount == null)
            {
                throw new Exception("حساب بانکی برای شهر پروسس پیدا نشد.");
            }

            //  FixedAmount   
            var fixedAmountStr = model.FixedAmount;
            var onlyNumbers = new string(fixedAmountStr.Where(c => char.IsDigit(c)).ToArray());
            int fixedAmountValue = int.TryParse(onlyNumbers, out int val) ? val : 0;
            var tarifaCodes = await GenerateTarifaCodesAsync(3);

            //  Tarifa
            var tarifa = new Tarifa
            {
                ApplicationId = application.Id,

                PassportPrice = fixedAmountValue,
              

                CPDTarifaCode = tarifaCodes[0],
                PostTarifaCode = tarifaCodes[1],
                RedCrossTarifaCode = tarifaCodes[2],

                TarifaDate = DateTime.Now
            };

            _context.Tarifas.Add(tarifa);
            await _context.SaveChangesAsync();


            //
            if (profile.ProcessingCityId == null)
                throw new Exception("محل پروسس انتخاب نشده است.");



            //Appointment
            DateTime slotDate = GetAppointmentDate(profile.ProcessingCityId.Value);
            int todayCount = _context.Appointments
                .Count(a => a.ProcessingCityId == profile.ProcessingCityId.Value && a.SlotDate.Date == slotDate.Date);
            int appointmentNumber = todayCount + 1;

            var appointment = new Appointment
            {
                ProfileId = profile.Profile_Id,
                ProcessingCityId = profile.ProcessingCityId.Value,
                AppointmentNumber = appointmentNumber,
                CreatedOn = DateTime.UtcNow.AddHours(4.5),
                SlotDate = GetAppointmentDate(profile.ProcessingCityId.Value)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            var birthShamsi = model.BirthDateShamsi;  // آیا مقدار دارد؟

            TempData["BirthDateShamsi"] = birthShamsi;

            HttpContext.Session.SetString("UserCode", profile.Code);

            
            return RedirectToAction("Profile");
        }

        //----- Profile By Code ---------
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
           
            var code = HttpContext.Session.GetString("UserCode");

            if (string.IsNullOrWhiteSpace(code))
                return RedirectToAction("Create"); // 

            var profile = _context.Profiles
                .Include(p => p.Gender)
                .Include(p => p.Profession)
                .Include(p => p.ProcessingCity)
                .Include(p => p.BirthCity)
                .Include(p => p.Application)
                    .ThenInclude(a => a.PassportType)
                .Include(p => p.Application)
                    .ThenInclude(a => a.PassportDuration)
                .Include(p => p.Application)
                    .ThenInclude(a => a.PaymentType)
                .Include(p => p.Application)
                    .ThenInclude(a => a.AppoinmentType)
                .Include(p => p.Application)
                    .ThenInclude(a => a.UserAddress)
                        .ThenInclude(ua => ua.PostalAddress)
                .Include(p => p.ProfilePhoto)
                .Include(p => p.IdentityDocumentType)
                .Include(p => p.Application)
                    .ThenInclude(a => a.Tarifas)
                .FirstOrDefault(p => p.Code == code);

            if (profile == null)
                return NotFound();

            var bankAccounts = _context.BankAccounts
                .Where(b => b.ProcessingCityId == profile.ProcessingCityId)
                .ToList();

            var appointment = _context.Appointments
                 .Where(a => a.ProfileId == profile.Profile_Id)
                 .OrderByDescending(a => a.SlotDate)
                 .FirstOrDefault();
         


            var firstTarifa = profile.Application?.Tarifas?.FirstOrDefault();

            string barcodeBase64 = GenerateBarcodeImage(profile.Code);
            string qrCodeBase64 = GenerateQRCodeImage(profile.Code, profile.DName, profile.DFatherName);

            ViewBag.BarcodeValue = profile.Code;
            ViewBag.BarcodeImageBase64 = barcodeBase64;
            ViewBag.QRCodeImageBase64 = qrCodeBase64;

            var viewModel = new ProfileViewModel
            {
                ApplicationId = profile.Application?.Id ?? 0,
                Profile = profile,
                Application = profile.Application,
                PassportType = profile.Application.PassportType,
                PassportDuration = profile.Application.PassportDuration,
                PaymentType = profile.Application.PaymentType,
                AppoinmentType = profile.Application.AppoinmentType,
                UserAddress = profile.Application.UserAddress,
                PostalAddress = profile.Application.UserAddress?.PostalAddress,
                ProfilePhoto = profile.ProfilePhoto,
                IdentityDocumentType = profile.IdentityDocumentType,
                BankAccounts = bankAccounts,
                Tarifas = profile.Application.Tarifas,
                PassportPrice = firstTarifa?.PassportPrice,
                Code = profile.Code,
                BarcodeImageBase64 = barcodeBase64,

                AppointmentDate = appointment?.SlotDate,
                Appointment = appointment,
            };

            if (TempData.ContainsKey("FixedPayAmount"))
            {
                ViewBag.FixedPayAmount = TempData["FixedPayAmount"];
            }

            return View("Profile", viewModel);
        }

        // ----------------- Download pdf for user ------------------
        [HttpPost]
        public async Task<IActionResult> GeneratePdf(string html, int applicationId)
        {
            try
            {
                var application = await _context.Applications
                    .FirstOrDefaultAsync(a => a.Id == applicationId);

                if (application == null)
                    return NotFound("درخواست نامعتبر است.");

                var appointment = await _context.Appointments
                    .Where(a => a.ProfileId == application.ProfileId)
                    .OrderByDescending(a => a.SlotDate)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                    return NotFound("نوبت یافت نشد.");

                var timeRemaining = appointment.SlotDate - DateTime.UtcNow;

                if (timeRemaining.TotalHours > 24)
                {
                    var allowedTime = appointment.SlotDate.AddHours(-24);
                    string msg = $"📅 فرم تنها از تاریخ {allowedTime:yyyy/MM/dd} بعد از ساعت {"04:00"} بعد از ظهر  قابل دانلود است.";
                    return BadRequest(new { message = msg });
                }

                Console.WriteLine("Ok");            
                html = html.Replace("StrTag", "<").Replace("EndTag", ">");

                string inlineCss = @"
            <style>
                body {
                    font-family: Arial, sans-serif;
                    direction: rtl;
                    text-align: right;
                }
                #Appointment {
                    display: flex;
                    justify-content: space-between; 
                    padding-left:300px; padding-right:25px;color:red;
                }
                .center #p2 {
                    font-size:19px;  font-weight: bold;  
                }
                .center #p1 {
                    font-size:21px; font-weight: bold;
                }
                .center #p3 {
                    font-size:17px; font-weight: bold;
                }
                #pdfContainer {
                    padding:10px;
                    margin-top:50px;
                    display: flex;
                    justify-content: space-between; 
                    direction: rtl;
                    align-items: center;
                    padding:10px;
                    margin:10px;
                }
                #border { 
                    padding:10px;
                    margin:10px;
                    border:solid 2px black;
                }
                .header {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    width:90%; margin-right:50px;
                    padding-top:10px;
                }
                .logo1 img, .logo2 img {
                    width: 150px;
                    height: auto;
                }
                .center {
                    text-align: center;
                    flex-grow: 1;
                }
                table {
                    width: 100%;
                    border-collapse: collapse;
                    direction: rtl; 
                    border-bottom:none;   
                }
                hr {
                    width:95%; margin-right:25px;
                }
                #firstTable, #secondT, #table3, #table4 {
                    width:95%; margin-right:25px;
                }
                #tarifaContainer {
                    margin-right:5px;
                }
                .logo1 img, .logo2 img {
                    width:120px;
                }
                #footer div h6 {
                    display: flex;
                    justify-content: center; padding-top:-10px;
                }
                th, td {
                    border: 1px solid gray;
                    padding: 8px;
                    text-align: right;
                    font-size:13px;
                }
                #ProfileImage {
                    height:220px;
                    width:200px;    
                    border:solid 1px lightgray;
                    margin-right:3px;
                    margin-top:-0px;
                }
                #ProfileImage img {
                    height:218px;
                    width:198px;      
                }
                #image {
                    width:20%;
                }
                #headerInfo {
                    height:60px;
                }
                #headerIn {
                    height:50px;
                }
                #firstTable, #secondT {
                    margin-top:15px; margin-bottom:15px;
                }
                .imageTable {
                    margin:10px;
                }
                #secondTable {
                    margin:10px; margin-top:10px;
                }
                #line {
                    margin:10px;
                }
                .text-start {
                    text-align:left;
                }
                #line #h6 {
                    text-align:center;  font-size:16px; font-weight:bold;
                }
                #ProcessCity #h6 {
                    text-align:center; font-size:16px; color:red;
                }
                #bottomBarcodes {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    padding-left:17px;
                }
                .container .card {
                    border: solid 1px black; margin-bottom:50px; margin-left:15px; margin-right:15px;
                }
                .table-sm tr th {
                    padding:4px; font-size:12px; text-align: center;
                }
                .table-sm .bgcolor {
                    background-color:lightgray;
                }
                .table-sm tr td {
                    padding:4px; font-size:12px; text-align: center; border-left-end: none; border-right-start: none; border-bottom:none;
                }
                .table-sm {
                    border-left:none;
                }
                .d-flex {
                    margin-right:10px; margin-bottom:5px;
                }
                #firstTable .DariInfo #headerIno img {
                    background-color:white;
                }
                #footer {
                    margin-left:35px; margin-right:35px;
                }
                .my-box {
                    border: 1px solid #999;
                    border-radius: 0px;
                    position: relative;
                    padding-top: 20px;
                    height:300px;
                }
                .box-title {
                    position: absolute;
                    top: -12px;
                    left: 50%;
                    transform: translateX(-50%);
                    background: white;
                    padding: 0 10px;
                    font-weight: bold;
                    color: #333;
                    z-index: 1;
                }
                #Table3 {
                    margin-bottom:12px;
                }
                #ProfileImage {
                    height: 250px;
                    width: 200px;
                    background-color: lightgray;
                    border: solid 1px lightgray;
                    margin-right: 3px;
                    margin-top: 0px;
                }
                #ProfileImage img {
                    height: 250px;
                    width: 200px;
                }
                #image {
                    width:200px;
                }
                #tarifaHeader {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    width:90%; margin-right:50px;
                    padding-top:10px;
                }
            </style>";

                html = $"<html><head>{inlineCss}</head><body>{html}</body></html>";

                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(html);
                byte[] pdfFile = doc.Save();
                doc.Close();

           /*     s*//*tring fileName = $"فرم ثبت نام برای دریافت پاسپورت";
                return File(pdfFile, "application/pdf", fileName);*/

                string fileName = $"form_passport_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                return File(pdfFile, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating PDF: " + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //----------------end pdf section-----------------------

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
        //--------------------------------------end codes--------------

        // ------------------Searching ----------------------
        // Expired blocks ceaning
        private async Task CleanupExpiredBlocks()
        {
            var expiredBlocks = _context.SearchBlocks
                .Where(b => b.BlockedUntil != null && b.BlockedUntil <= DateTime.UtcNow);

            if (expiredBlocks.Any())
            {
                _context.SearchBlocks.RemoveRange(expiredBlocks);
                await _context.SaveChangesAsync();
            }
        }
        //Search
        [HttpPost]
        public async Task<IActionResult> Result(string code)
        {
            // cleaning searching IP
            await CleanupExpiredBlocks();

            string ip = GetClientIp();

            var blockEntry = await _context.SearchBlocks.FirstOrDefaultAsync(b => b.IpAddress == ip);
            if (blockEntry != null && blockEntry.BlockedUntil.HasValue && blockEntry.BlockedUntil.Value > DateTime.UtcNow)
            {
                var remainingTime = blockEntry.BlockedUntil.Value - DateTime.UtcNow;

                return Json(new
                {
                    errorMessage = "شما به دلیل تلاش‌های ناموفق، به‌طور موقت مسدود شده‌اید.",
                    blocked = true,
                    remainingSeconds = (int)remainingTime.TotalSeconds
                });
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                return Json(new { errorMessage = "لطفاً کد پیگیری را وارد نمایید." });
            }

            var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);

            if (profile == null)
            {
                if (blockEntry == null)
                {
                    blockEntry = new SearchBlock
                    {
                        IpAddress = ip,
                        FailedAttempts = 1,
                        LastAttempt = DateTime.UtcNow
                    };
                    _context.SearchBlocks.Add(blockEntry);
                }
                else
                {
                    blockEntry.FailedAttempts++;
                    blockEntry.LastAttempt = DateTime.UtcNow;

                    if (blockEntry.FailedAttempts >= 3)
                    {
                        blockEntry.BlockedUntil = DateTime.UtcNow.AddMinutes(2);
                    }
                }

                await _context.SaveChangesAsync();

                string errorMsg = blockEntry.BlockedUntil.HasValue && blockEntry.BlockedUntil.Value > DateTime.UtcNow
                    ? "شما به دلیل تلاش‌های زیاد ناموفق، به مدت ۲۴ ساعت مسدود شده‌اید."
                    : "هیچ معلوماتی با این کد پیدا نشد.";

                return Json(new { errorMessage = errorMsg, blocked = blockEntry.BlockedUntil.HasValue });
            }

            if (blockEntry != null)
            {
                _context.SearchBlocks.Remove(blockEntry);
                await _context.SaveChangesAsync();
            }

            // ✅ ذخیره کد در Session
            HttpContext.Session.SetString("UserCode", profile.Code);

            // ✅ ریدایرکت بدون کد در URL
            return Json(new
            {
                redirectToProfile = true,
                redirectUrl = Url.Action("Profile", "Registration")
            });
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoadList()
        {
            try
            {
                var profile = _context.Profiles
                    .Include(p => p.Gender)
                    .Include(p => p.Profession)

                    .ToList();

                if (profile == null || !profile.Any())
                {
                    return NotFound();
                }

                return View("_List", profile);
            }
            catch (Exception ex)
            {
                // Log error to console or file
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


    }
}
