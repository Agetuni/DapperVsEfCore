using Microsoft.EntityFrameworkCore;

namespace DapperTest.Model.context;

public class ApplicationDbContext :DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testDapper;Trusted_Connection=True;");

        using (var serviceScope = services.BuildServiceProvider().CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }

    public DbSet<Person> Person { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<AddressLocation> AddressLocation { get; set; }
}
