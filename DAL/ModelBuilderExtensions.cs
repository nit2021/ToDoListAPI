using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;
namespace ToDoListAPI.DAL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 101, UserName = "Admin", Password = "Pa$$w0rd" });
            modelBuilder.Entity<ToDoList>().HasData(new ToDoList { ListId = 201, Description = "ListItem1", Owner = 101, CreatedDate = System.DateTime.UtcNow });
            modelBuilder.Entity<ToDoItem>().HasData(new ToDoItem { ItemId = 301, Description = "Item1", TaskOwner = 201, CreatedDate = System.DateTime.UtcNow });
            modelBuilder.Entity<Label>().HasData(new Label { LabelId = 401, Description = "Label1", ItemOwner = 301 });

        }
    }
}