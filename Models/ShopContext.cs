using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EF
{
    public class ShopContext : DbContext
    {

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            // buider.AddFilter(DbLoggerCategory.Database.Name, LogLevel.Information);
            builder.AddConsole();
        });//cái này dùng để in ra nội dung trong file log

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryDetails> CategoryDetail { get; set; }

        

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
            Console.WriteLine("OnConfiguring");
        }

        //cách để tạo ra fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API

            // //entity => Fluent Api là 1 Product (có 3 cách)
            // var entity = modelBuilder.Entity(typeof(Product));
            // var entity = modelBuilder.Entity<Product>();//giống cách ở trên
            modelBuilder.Entity<Product>(entity =>
            {
                // Table mapping: dùng như này thì trực tiếp đặt tên cho bảng ko cần đặt bên kia
                entity.ToTable("Product");
                //PK
                entity.HasKey(p => p.ProductId);
                //Index: đánh chỉ mục: truy vấn nhanh hơn thôi chứ ko có j đặc biệt
                entity.HasIndex(p => p.Price)
                .HasDatabaseName("index-product-price");
                //FK - Relative
                entity.HasOne(p => p.Category)
                .WithMany()//nghĩa là category ko có property của product
                .HasForeignKey("CategId")// đặt tên cho FK
                .OnDelete(DeleteBehavior.Cascade)// thiết lập nếu khóa chính bên kia bị xóa thì khóa phụ bị ảnh hưởng thế nào
                .HasConstraintName("FK_Product_Category");//thiết lập tên cho mối quan hệ giữa 2 bảng

                entity.HasOne(p => p.Category2)
                .WithMany(c => c.Products)//chỉ ra thuộc tính tương ứng với phần nhiều
                .HasForeignKey("CategId2")
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Product_Category2");

                entity.Property(p => p.Name)//chọn cột cần thao tác
                .HasColumnName("Product Name")//đặt tên
                .HasColumnType("nvarchar")//chọn kiểu dữ liệu
                .HasMaxLength(60)//chọn độ dài
                .IsRequired(true)//not null hay ko
                .HasDefaultValue("productNameDefault");//đặt giá trị mặc định nếu ko có giá trị

                //không nên lạm dụng fluent API

                
            });

            // thiết lập quan hệ 1-1
            modelBuilder.Entity<CategoryDetails>(entity =>
            {
                entity.HasOne(d => d.category)
                .WithOne(c => c.categoryDetails) // set mối quan hệ 1-1
                .HasForeignKey<CategoryDetails>(c => c.CategoryDetailID) //tạo FK
                .OnDelete(DeleteBehavior.Cascade);
            });

            Console.WriteLine("OnModelCreating");
        }
    }
}