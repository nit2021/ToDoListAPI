namespace ToDoListAPI.Services
{
    public interface IUserService
    {
        public int userId { get; set; }
        bool ValidateCredentials(string username, string password);
    }
}