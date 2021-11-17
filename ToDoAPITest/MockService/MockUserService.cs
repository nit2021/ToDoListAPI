using ToDoListAPI.ToDoAPI.Services;

namespace ToDoAPITest.MockService.Test
{
    public class MockUserService : IUserService
    {
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
    }
}