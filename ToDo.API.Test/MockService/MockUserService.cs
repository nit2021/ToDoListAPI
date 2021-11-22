using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.DTO;
using ToDoListAPI.ToDoAPI.Services;

namespace ToDoAPITest.MockService.Test
{
    public class MockUserService : IUserService
    {
        public List<Users> users { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }

        public MockUserService()
        {
        }

        public bool ValidateCredentials(string username, string password)
        {
            if (userName == username && Password == password)
                return true;
            else
                return false;
        }

        public async Task<Users> CreateUser(UsersDTO user)
        {
            if (user==null)
                return null;
            Users newUser=new Users{UserId=207,UserName=user.UserName,Password=user.Password};
            users.Add(newUser);
            return await Task.FromResult(newUser);
        }
    }
}