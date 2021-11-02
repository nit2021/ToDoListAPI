using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.Models
{
    [DataContract]
    public class User
    {

        /// <summary>
        /// Optional.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

    }
}