using Microsoft.EntityFrameworkCore;
using WarehouseManager.Models;


namespace WarehouseManager.Data

{
    public class AppDbContext: DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }



    }
}
