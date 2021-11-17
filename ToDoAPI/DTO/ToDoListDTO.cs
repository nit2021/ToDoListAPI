using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoAPI.DTO
{
    [DataContract]
    public class ToDoListDTO
    {
        public int ListId { get; set; }
        public string Description { get; set; }
        public int OwnerID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}