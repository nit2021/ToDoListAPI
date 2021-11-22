using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ToDoListAPI.ToDoAPI.DTO
{
    [DataContract]
    public class LabelDTO
    {
        [DataMember]
        public int LabelId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int ToDoItemID { get; set; }
        [DataMember]
        public int ToDoListID { get; set; }

    }
}