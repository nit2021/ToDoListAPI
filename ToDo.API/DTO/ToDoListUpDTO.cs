using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoAPI.DTO
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