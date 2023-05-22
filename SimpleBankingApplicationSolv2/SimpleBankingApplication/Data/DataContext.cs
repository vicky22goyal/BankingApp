using Microsoft.EntityFrameworkCore;
using SimpleBankingApplication.Models;

namespace SimpleBankingApplication.Data
{

    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));

            // Other service registrations

            services.AddControllers();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Accounts)
                .WithOne()
                .HasForeignKey(a => a.Id)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);


            // Additional configuration and constraints can be added here
        }
    }
}