using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Core.Models;
using ToDoAPI.Core.Utilities;
using ToDoAPI.DAL;
using ToDoListAPI.ToDoBLL.DTO;

namespace ToDoListAPI.ToDoBLL.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoContext _context;
        public int userId { get; set; }
        public UserService(ToDoContext context)
        {
            this._context = context;
        }
        public bool ValidateCredentials(string username, string password)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var user = _context.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (password != Cryptography.Decrypt(user.Password))
                return false;
            else
            {
                userId = user.UserId;
                return true;
            }
        }

        public async Task<Users> CreateUser(UsersDTO user)
        {
            Users newUser = new Users();
            newUser.UserName = user.UserName;
            newUser.Password = Cryptography.Encrypt(user.Password);
            _context.Users.Attach(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
    }
}