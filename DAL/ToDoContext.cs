using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;
namespace ToDoListAPI.DAL
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            //    Database.Migrate();
        }
        public DbSet<User> User { get; set; }
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<Label> Label { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { UserId = 101, UserName = "Admin", Password = "Pa$$w0rd" });
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
        // {  
        //     optionsBuilder.UseSqlServer("connectionstring");  
        //     base.OnConfiguring(optionsBuilder);  
        // }

    }
}