using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Services
{
    public interface IUserService
    {
        public int userId { get; set; }
        bool ValidateCredentials(string username, string password);
    }
}