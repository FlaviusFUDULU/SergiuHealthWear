using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AplicatieSalariati.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<SalariatModel> Salariati { get; set; }
        public DbSet<TaxePrestabiliteModel> TaxePrestabilite { get; set; }
        public DbSet<DateAdministratorModel> DateAdministratorModels { get; set; }
        public DbSet<DateEchipaModel> DateEchipeModels { get; set; }
        public DbSet<DateManagerModel> DateManagerModels { get; set; }
        public DbSet<DateAngajatModel> DateAngajatModels { get; set; }
        public DbSet<Pacient> Pacient { get; set; }
        public DbSet<Medic> Medic { get; set; }
        public DbSet<RetetaModel> Reteta { get; set; }
        public DbSet<Istoric> Istoric { get; set; }
        public DbSet<Orar> Orar { get; set; }
        public DbSet<Programare> Programare { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}