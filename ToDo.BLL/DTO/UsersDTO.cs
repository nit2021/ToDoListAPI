using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoBLL.DTO
{
    [DataContract]
    public class UsersDTO
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

    }
}