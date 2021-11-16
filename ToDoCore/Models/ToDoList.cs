using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoAPI.Core.Models
{
    [DataContract]
    public partial class ToDoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int ListId { get; set; }

        [Required]
        [MaxLength(50)]
        [DataMember]
        public string Description { get; set; }

        [ForeignKey("User")]
        [Required]
        public int Owner { get; set; }
        public Users User { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        
        [DataMember]
        public DateTime? UpdatedDate { get; set; }

    }
}