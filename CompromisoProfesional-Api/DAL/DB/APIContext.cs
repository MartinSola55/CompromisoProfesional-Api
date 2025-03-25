using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CompromisoProfesional_Api.Models;

namespace CompromisoProfesional_Api.DAL.DB;

public class APIContext(DbContextOptions<APIContext> options) : IdentityDbContext<ApiUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApiUser>().ToTable("User");
        builder.Entity<IdentityRole>().ToTable("Role");
        builder.Ignore<IdentityUserRole<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();

        builder.Entity<ApiUser>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Appliance>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Employee>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Evolution>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Expense>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<ExpenseAppliance>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<ExpenseSupply>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Patient>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<PatientAppliance>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<PatientSupply>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Profession>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Provider>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<SocialSecurity>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<SuggestedPrice>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Supply>().HasQueryFilter(x => x.DeletedAt == null);
    }

    // Entities
    public DbSet<ApiUser> User { get; set; }
    public DbSet<IdentityRole> Role { get; set; }
    public DbSet<Appliance> Appliance { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Evolution> Evolution { get; set; }
    public DbSet<Expense> Expense { get; set; }
    public DbSet<ExpenseAppliance> ExpenseAppliance { get; set; }
    public DbSet<ExpenseSupply> ExpenseSupply { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<PatientAppliance> PatientAppliance { get; set; }
    public DbSet<PatientSupply> PatientSupply { get; set; }
    public DbSet<Profession> Profession { get; set; }
    public DbSet<Provider> Provider { get; set; }
    public DbSet<SocialSecurity> SocialSecurity { get; set; }
    public DbSet<SuggestedPrice> SuggestedPrice { get; set; }
    public DbSet<Supply> Supply { get; set; }

}
