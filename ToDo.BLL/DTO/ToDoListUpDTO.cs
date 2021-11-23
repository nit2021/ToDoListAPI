using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoBLL.DTO
{
    [DataContract]
    public class ToDoListUpDTO
    {
        [DataMember]
        public int ListId { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}