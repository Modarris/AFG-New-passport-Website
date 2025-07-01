using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ---------- Hosted Service ----------
builder.Services.AddHostedService<BlockCleanupService>();

// ---------- Logging ----------
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ---------- Identity ----------
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<WebDbContext>()
    .AddDefaultTokenProviders();

// ---------- DbContext ----------
builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Passport"))
);

// ---------- MVC + Localization ----------
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// ---------- Session ----------
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  //
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ---------- Localization config ----------
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("fa-IR"),
    new CultureInfo("ps-AF")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fa-IR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
});
// Lan hosting
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP 
});
var app = builder.Build();

// ---------- Use Localization ----------
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

// ---------- Error Handling ----------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ---------- Middleware pipeline ----------
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();  //Authentication

app.UseAuthentication();
app.UseAuthorization();

// ---------- Routing ----------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Language}/{action=Index}/{id?}");

// ---------- Run app ----------
app.Run();
