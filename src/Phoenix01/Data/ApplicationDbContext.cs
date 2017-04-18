using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Models;
using Phoenix01.Models.AccountViewModels;

namespace Phoenix01.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public DbSet<Story> Stories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUserLanguage>()
                .HasKey(t => new { t.ApplicationUserId, t.LanguageId });

            //builder.Entity<ApplicationUser_Language>()
            //    .HasOne(pt => pt.ApplicationUser)
            //    .WithMany(p => p.LanguageLinks)
            //    .HasForeignKey(pt => pt.AspNetUsersId);

            //builder.Entity<ApplicationUser_Language>()
            //        .HasOne(pt => pt.Language)
            //        .WithMany(t => t.UserLinks)
            //        .HasForeignKey(pt => pt.LanguageId);
        }


     
        }
  
    }

