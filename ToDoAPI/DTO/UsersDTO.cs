using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoAPI.DTO
{
    [DataContract]
    public class UsersDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}