using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EF
{
    public class ShopContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>{
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            // buider.AddFilter(DbLoggerCategory.Database.Name, LogLevel.Information);
            builder.AddConsole();
        });

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        private const string connectionString = @"
                        Data Source=localhost,1433;
                        Initial Catalog=shopdata;
                        User ID=sa;
                        Password=12345;
                        TrustServerCertificate=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies();//tự động nạp những cái reference
        }
    }
}