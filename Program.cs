
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

            // Category ca1 = new Category() { Name = "Dien thoai", Description = "Cac loai dien thoai" };
            // Category ca2 = new Category() { Name = "Do uong", Description = "Cac loai do uong" };
            // dbcontext.categories.Add(ca1);
            // dbcontext.categories.Add(ca2);

            var c1 = (from c in dbcontext.categories where c.CategoryID == 1 select c).FirstOrDefault();
            var c2 = (from c in dbcontext.categories where c.CategoryID == 2 select c).FirstOrDefault();

            dbcontext.Add(new Product() { Name = "Iphone 8", Price = 1000, CategId = 1 });
            dbcontext.Add(new Product() { Name = "Samsung", Price = 900, Category = c1 });
            dbcontext.Add(new Product() { Name = "Ruou vang", Price = 500, Category = c2 });
            dbcontext.Add(new Product() { Name = "Nokia", Price = 600, Category = c1 });
            dbcontext.Add(new Product() { Name = "Cafe", Price = 100, Category = c2 });
            dbcontext.Add(new Product() { Name = "Nuoc ngot", Price = 50, Category = c2 });
            dbcontext.Add(new Product() { Name = "Bia", Price = 200, Category = c2 });

            int numRowsChange = dbcontext.SaveChanges();//gọi khi làm bất kì tác vụ nào liên quan tới database
                                                        //trả về số dòng bị tác động
            Console.WriteLine($"Da chen {numRowsChange} dong du lieu");
        }

        static void ReadProduct()
        {
            using var dbcontext = new ShopContext();
                        
            // //truy vấn dạng join bảng để lấy nhiều hàng dữ liệu
            // var category = (from c in dbcontext.categories
            //                 where c.CategoryID == 2
            //                 select c).FirstOrDefault();
            // Console.WriteLine($"{category.CategoryID} - {category.Name}");

            // // var e = dbcontext.Entry(category);
            // // e.Collection(c => c.Products).Load();//điều hướng tập hợp, dùng để lưu trữ nhìu sản phẩm
            // //do đã có virtual của proxies nên ko cần 2 dòng code này

            // if (category.Products != null)
            // {
            //     Console.WriteLine($"So san pham: {category.Products.Count()}");
            //     category.Products.ForEach(p => p.PrintInfor());
            // }
            // else Console.WriteLine("Product == null");

            // truy vấn dạng join nhưng chỉ lấy 1 hàng dữ liệu
            // var product = (from p in dbcontext.products
            //                where p.ProductId == 3
            //                select p).FirstOrDefault();//nếu thấy thì trả về kq, ko có thì trả về null

            // //Cách truy xuất 1 bảng khác theo khóa ngoại
            // // var e = dbcontext.Entry(product);//dùng nạp dữ liệu tham chiếu vào 1 model khác
            // // e.Reference(p => p.Category).Load();//lấy về dữ liệu tham chiếu

            // if (product != null) product.PrintInfor();

            // if (product.Category != null)
            // {
            //     Console.WriteLine($"{product.Category.Name} - {product.Category.Description}");
            // }
            // else Console.WriteLine("Category == null");
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

        static void DeleteCategory()
        {
            using var dbcontext = new ShopContext();

            var category = (from c in dbcontext.categories
                            where c.CategoryID == 1
                            select c).FirstOrDefault();

            

            if (category != null)
            {
                dbcontext.Remove(category);
                int numRowsChange = dbcontext.SaveChanges();
                Console.WriteLine($"Da xoa {numRowsChange} dong du lieu");
            }
        }

        static void Main(string[] args)
        {
            // Entity -> Database, Table
            // Database - SQL Server: data01 -> DbContext
            // --product

            //Create, Drop Database
            // CreateDatabase();
            // DropDatabase();

            //Insert, Select, Update, Delete
            InsertData();
            // ReadProduct();
            // RenameProduct(1, "Laptop 02");
            // DeleteCategory();

            //Logging-

        }
    }
}