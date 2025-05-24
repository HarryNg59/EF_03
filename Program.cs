
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EF
{
    class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new ShopContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;

            var result = dbcontext.Database.EnsureCreated();

            if (result)
            {
                Console.WriteLine($"Tao db {dbname} thanh cong");
            }
            else
            {
                Console.WriteLine($"Tao db {dbname} that bai");
            }
        }

        static void DropDatabase()
        {
            using var dbcontext = new ShopContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;

            var result = dbcontext.Database.EnsureDeleted();

            if (result)
            {
                Console.WriteLine($"Xoa db {dbname} thanh cong");
            }
            else
            {
                Console.WriteLine($"Xoa db {dbname} that bai");
            }
        }

        static void InsertData()
        {
            using var dbcontext = new ShopContext();

            // Category c1 = new Category() { Name = "Dien thoai", Description = "Cac loai dien thoai" };
            // Category c2 = new Category() { Name = "Do uong", Description = "Cac loai do uong" };
            // dbcontext.categories.Add(c1);
            // dbcontext.categories.Add(c2);

            var c1 = (from c in dbcontext.categories where c.CategoryID == 1 select c).FirstOrDefault();
            var c2 = (from c in dbcontext.categories where c.CategoryID == 2 select c).FirstOrDefault();

            dbcontext.Add(new Product() { Name = "Iphone 8", Price = 1000, CategId = 1 });
            dbcontext.Add(new Product() { Name = "Samsung", Price = 900, Category = c1 });
            dbcontext.Add(new Product() { Name = "Ruou vang", Price = 500, Category = c2 });
            dbcontext.Add(new Product() { Name = "Nokia", Price = 600, Category = c1 });
            dbcontext.Add(new Product() { Name = "Cafe", Price = 100, Category = c2 });

            int numRowsChange = dbcontext.SaveChanges();//gọi khi làm bất kì tác vụ nào liên quan tới database
                                                        //trả về số dòng bị tác động
            Console.WriteLine($"Da chen {numRowsChange} dong du lieu");
        }

        static void ReadProduct()
        {
            using var dbcontext = new ShopContext();
            
            var product = (from p in dbcontext.products
                               where p.ProductId == 3
                               select p).FirstOrDefault();//nếu thấy thì trả về kq, ko có thì trả về null
            if (product != null) product.PrintInfor();
        }

        // static void RenameProduct(int id, string newName)
        // {
        //     using var dbcontext = new ShopContext();

        //     Product product = (from p in dbcontext.products
        //                        where p.ProductId == id
        //                        select p).FirstOrDefault();

        //     if (product != null)
        //     {
        //         //product -> DbContext
        //         EntityEntry<Product> entry = dbcontext.Entry(product);
        //         entry.State = EntityState.Detached;
        //         //code như này có nghĩa là tách table Product khỏi sự kiểm soát của DBcontext
        //         //có nghĩa là ko thay đổi được dữ liệu nữa nếu code như này

        //         product.ProductName = newName;
        //         int numRowsChange = dbcontext.SaveChanges();
        //         Console.WriteLine($"Da sua {numRowsChange} dong du lieu");
        //     }
        // }

        // static void DeleteProduct(int id)
        // {
        //     using var dbcontext = new ShopContext();

        //     Product product = (from p in dbcontext.products
        //                        where p.ProductId == id
        //                        select p).FirstOrDefault();

        //     if (product != null)
        //     {
        //         dbcontext.Remove(product);
        //         int numRowsChange = dbcontext.SaveChanges();
        //         Console.WriteLine($"Da xoa {numRowsChange} dong du lieu");
        //     }
        // }

        static void Main(string[] args)
        {
            // Entity -> Database, Table
            // Database - SQL Server: data01 -> DbContext
            // --product

            //Create, Drop Database
            // CreateDatabase();
            // DropDatabase();

            //Insert, Select, Update, Delete
            // InsertData();
            ReadProduct();
            // RenameProduct(1, "Laptop 02");
            // DeleteProduct(1);

            //Logging-

        }
    }
}