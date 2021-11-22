using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ToDoListAPI.ToDoAPI.DTO
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