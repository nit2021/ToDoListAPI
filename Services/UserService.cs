using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.DAL;

namespace ToDoListAPI.Services
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

            //var count = (from x in _context.User where x.UserName == username && x.Password == password select x).Count();
            var user = _context.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
            if (user == null)
                return false;
            else
            {
                userId = user.UserId;
                return true;
            }

            //return username.Equals("admin") && password.Equals("Pa$$WoRd");  
        }
    }
}