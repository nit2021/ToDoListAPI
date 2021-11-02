using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ToDoListAPI.Models
{
    [DataContract]
    public partial class ToDoItem
    {
        /// <summary>
        /// Optional.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int ItemId { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DataMember]
        public string Description { get; set; }

        [ForeignKey("Owner")]
        [Required]
        public User User { get; set; }

    }
}