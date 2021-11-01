using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.DAL;
using ToDoListAPI.Services;

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
            var user = _context.User.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
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