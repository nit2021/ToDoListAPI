using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoAPI.Core.Models
{
    [DataContract]
    public partial class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(50)]
        [DataMember]
        public string Description { get; set; }

        [ForeignKey("ToDoList")]
        [Required]
        public int TaskOwner { get; set; }
        public ToDoList ToDoList { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        
        [DataMember]
        public DateTime? UpdatedDate { get; set; }

    }
}