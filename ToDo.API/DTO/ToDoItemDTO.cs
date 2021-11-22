using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoAPI.DTO
{
    [DataContract]
    public class ToDoItemDTO
    {
        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int ToDoListID { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        
        [DataMember]
        public DateTime? UpdatedDate { get; set; }

    }
}