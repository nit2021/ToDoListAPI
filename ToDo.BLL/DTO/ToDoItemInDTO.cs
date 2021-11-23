using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoBLL.DTO
{
    [DataContract]
    public class ToDoItemInDTO
    {

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int ToDoListID { get; set; }

    }
}