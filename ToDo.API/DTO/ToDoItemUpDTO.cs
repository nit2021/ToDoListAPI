using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoAPI.DTO
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