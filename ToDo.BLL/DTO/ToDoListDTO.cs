using System;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoBLL.DTO
{
    [DataContract]
    public class ToDoListDTO
    {
        [DataMember]
        public int ListId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int OwnerID { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime? UpdatedDate { get; set; }

    }
}