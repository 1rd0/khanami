using khanami.Entities;
using khanami.Model;
using Microsoft.EntityFrameworkCore;


namespace khanami.Data


{
    public class DBContext: DbContext
    {
        public  DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ItemCategory> Category { get; set; }
        
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public DBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
         
       

      

    }
}
