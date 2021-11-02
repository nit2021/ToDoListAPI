using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ToDoListAPI.Models
{
    [DataContract]
    public partial class Label
    {

        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int LabelId { get; set; }


        /// <summary>
        /// Note.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DataMember]
        public string Description { get; set; }

        [ForeignKey("ItemID")]
        [Required]
        public ToDoItem ToDoItem { get; set; }

    }
}