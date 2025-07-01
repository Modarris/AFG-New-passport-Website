using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AFG_New_passport_Website.Data
{
    public class WebDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<News> NewsItems { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<ProvinceOfBirth> ProvinceOfBirths { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }
        public DbSet<ProvinceQuota> ProvinceQuotas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PublicHoliday> PublicHolidays { get; set; }
        public DbSet<PassportApplication> PassportApplications { get; set; }
        public DbSet<AppoinmentType> AppoinmentTypes { get; set; }
        public DbSet<PassportType> PassportTypes { get; set; }
        public DbSet<PassportDuration> PassportDurations { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }   
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<CompanyOfficial> CompanyOfficials { get; set; }
        public DbSet<IdentityDocumentType> IdentityDocumentTypes { get; set; }
        public DbSet<BankAccounts> BankAccounts { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<SearchBlock> SearchBlocks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // UserAddress → Profile
            modelBuilder.Entity<UserAddress>()
                .HasOne(ua => ua.Profile)
                .WithMany()
                .HasForeignKey(ua => ua.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment → ProvinceQuota
           /* modelBuilder.Entity<Appointment>()
               .HasOne(a => a.ProvinceQuota)
               .WithMany()
               .HasForeignKey(a => a.ProvinceQuotaId)
               .OnDelete(DeleteBehavior.Restrict);*/


            // Profile → CompanyInfo
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.CompanyInfo)
                .WithOne(b => b.Profile)
                .HasForeignKey<CompanyInfo>(b => b.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            // Profile → IdentityDocumentType
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.IdentityDocumentType)
                .WithMany(idt => idt.Profiles)
                .HasForeignKey(p => p.IdentityDocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Country → City

            modelBuilder.Entity<CompanyOfficial>()
                .Property(x => x.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Profession>()
               .Property(x => x.Id)
               .ValueGeneratedNever();


        }

    }
}
