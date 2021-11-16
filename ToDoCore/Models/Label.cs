using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ToDoAPI.Core.Models
{
    [DataContract]
    public partial class Label
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int LabelId { get; set; }

        [Required]
        [MaxLength(50)]
        [DataMember]
        public string Description { get; set; }

        [ForeignKey("ToDoItem")]
        [Required]
        public int ItemOwner { get; set; }

        [ForeignKey("ToDoList")]
        [Required]
        public int ToDoListID { get; set; }
        public ToDoItem ToDoItem { get; set; }

    }
}