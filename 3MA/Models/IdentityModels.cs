using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
// new...
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _3MA.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // This is the basic identity unit for managing individual accounts
    public class ApplicationUser : IdentityUser
    {
        public int Suite { get; internal set; }
        [Display(Name = "Floor plan")]
        public string Plan { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // ATTENTION - Add custom user claims here
            //userIdentity.AddClaims(new[] { new Claim("Suite", "911"), new Claim("Plan", "G9") });
            userIdentity.AddClaim(new Claim("Suite", this.Suite.ToString()));
            userIdentity.AddClaim(new Claim("Plan", this.Plan.ToString()));

            return userIdentity;
        }
    }

    // Entity Framework context used to manage interaction between our application and the database where our Account data is persisted.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DataContext", throwIfV1Schema: false) { }

        // Add DbSet<TEntity> properties here
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RoomFloor> FloorPlans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<POrder> POrders { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Accessories> Accessories { get; set; }
        //public DbSet<OneProduct> OneProduct { get; set; }

        // Turn OFF cascade delete, which is (unfortunately) the default setting
        // for Code First generated databases
        // For most apps, we do NOT want automatic cascade delete behaviour
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // First, call the base OnModelCreating method,
            // which uses the existing class definitions and conventions

            base.OnModelCreating(modelBuilder);

            // Then, turn off "cascade delete" for 
            // all default convention-based associations

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}