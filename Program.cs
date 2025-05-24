
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EF
{
    class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new ProductDbContext();
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
            using var dbcontext = new ProductDbContext();
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

        static void InsertProduct()
        {
            using var dbcontext = new ProductDbContext();
            /* Các bước insert sp
            Model (Product)
            Add, AddAsyc
            SaveChanges
            */

            //Thêm 1 dòng
            // var p1 = new Product();
            // p1.ProductName = "San pham 1";
            // p1.Provider = "Cong ty 1";

            // var p1 = new Product()
            // {
            //     ProductName = "San pham 2",
            //     Provider = "Cong ty 2"
            // };

            // dbcontext.Add(p1);

            //Thêm nhiều dòng
            var products = new object[]{
                new Product() {ProductName = "San pham 3", Provider = "Cong ty 3"},
                new Product() {ProductName = "San pham 4", Provider = "Cong ty 4"},
                new Product() {ProductName = "San pham 5", Provider = "Cong ty 5"},
            };

            dbcontext.AddRange(products);


            int numRowsChange = dbcontext.SaveChanges();//gọi khi làm bất kì tác vụ nào liên quan tới database
                                                        //trả về số dòng bị tác động
            Console.WriteLine($"Da chen {numRowsChange} dong du lieu");
        }

        static void ReadProduct()
        {
            using var dbcontext = new ProductDbContext();
            //LINQ
            //Truy vấn toàn bộ
            // var products = dbcontext.products.ToList();
            // products.ForEach(product => product.PrintInfor());

            //Truy vấn có điều kiện
            //lấy ra sp có id > 3
            // var products = from product in dbcontext.products
            //                where product.ProductId >= 3
            //                select product;
            //lấy ra sp có nsx là cong và xếp theo chiều giảm giần của id
            // var products = from product in dbcontext.products
            //                where product.Provider.Contains("Cong")
            //                orderby product.ProductId descending
            //                select product;
            // products.ToList().ForEach(product => product.PrintInfor());

            //Lấy ra sp có id = 4
            Product product = (from p in dbcontext.products
                               where p.ProductId == 4
                               select p).FirstOrDefault();//nếu thấy thì trả về kq, ko có thì trả về null
            if (product != null) product.PrintInfor();
        }

        static void RenameProduct(int id, string newName)
        {
            using var dbcontext = new ProductDbContext();

            Product product = (from p in dbcontext.products
                               where p.ProductId == id
                               select p).FirstOrDefault();

            if (product != null)
            {
                //product -> DbContext
                EntityEntry<Product> entry = dbcontext.Entry(product);
                entry.State = EntityState.Detached;
                //code như này có nghĩa là tách table Product khỏi sự kiểm soát của DBcontext
                //có nghĩa là ko thay đổi được dữ liệu nữa nếu code như này

                product.ProductName = newName;
                int numRowsChange = dbcontext.SaveChanges();
                Console.WriteLine($"Da sua {numRowsChange} dong du lieu");
            }
        }

        static void DeleteProduct(int id)
        {
            using var dbcontext = new ProductDbContext();

            Product product = (from p in dbcontext.products
                               where p.ProductId == id
                               select p).FirstOrDefault();

            if (product != null)
            {
                dbcontext.Remove(product);
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
            // InsertProduct();
            // ReadProduct();
            // RenameProduct(1, "Laptop 02");
            // DeleteProduct(1);

            //Login
        }
    }
}