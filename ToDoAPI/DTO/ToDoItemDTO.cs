using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.ToDoAPI.DTO
{
    [DataContract]
    public class ToDoItemDTO
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public int ToDoListID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}