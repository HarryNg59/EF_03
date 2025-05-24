using Microsoft.EntityFrameworkCore;

namespace EF
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> products { get; set; }

        private const string connectionString = @"
                        Data Source=localhost,1433;
                        Initial Catalog=data01;
                        User ID=sa;
                        Password=12345;
                        TrustServerCertificate=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}