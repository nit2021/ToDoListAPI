using System.Runtime.Serialization;
namespace ToDoListAPI.ToDoBLL.DTO
{
    [DataContract]
    public class LabelInDTO
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int ToDoItemID { get; set; }
        [DataMember]
        public int ToDoListID { get; set; }

    }
}