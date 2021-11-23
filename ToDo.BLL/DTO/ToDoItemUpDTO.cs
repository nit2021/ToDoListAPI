using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoBLL.DTO
{
    [DataContract]
    public class ToDoItemUpDTO
    {
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public string Description { get; set; }

    }
}