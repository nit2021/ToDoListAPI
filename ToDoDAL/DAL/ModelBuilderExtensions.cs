using Microsoft.EntityFrameworkCore;
using ToDoAPI.Core.Models;
namespace ToDoAPI.DAL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 101, UserName = "Admin", Password = "Pa$$w0rd" });
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 102, UserName = "Standard", Password = "Pa$$w0rd" });
            modelBuilder.Entity<ToDoList>().HasData(new ToDoList { ListId = 201, Description = "ListItem1", OwnerID = 101, CreatedDate = System.DateTime.UtcNow });
            modelBuilder.Entity<ToDoItem>().HasData(new ToDoItem { ItemId = 301, Description = "Item1", ToDoListID = 201, CreatedDate = System.DateTime.UtcNow });
            modelBuilder.Entity<Label>().HasData(new Label { LabelId = 401, Description = "Label1", ToDoItemID = 301, ToDoListID = 201 });

        }
    }
}
