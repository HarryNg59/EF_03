
using System;

namespace EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Entity -> Database, Table
            // Database - SQL Server: data01 -> DbContext
            // --product
            var dbcontext = new ProductDbContext();
        }
    }
}