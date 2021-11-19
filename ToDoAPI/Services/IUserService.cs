using System.Threading.Tasks;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.DTO;

namespace ToDoListAPI.ToDoAPI.Services
{
    public interface IUserService
    {
        public int userId { get; set; }
        bool ValidateCredentials(string username, string password);
        Task<Users> CreateUser(UsersDTO user);
    }
}