using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoAPI.Core.Models
{
    [DataContract]
    public class Users
    {

        /// <summary>
        /// Optional.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [DataMember]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

    }
}