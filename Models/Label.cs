using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListAPI.Models;
namespace ToDoListAPI.Models
{
    public partial class Label
    {

        /// <summary>
        /// Identity
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }


        /// <summary>
        /// Note.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [ForeignKey("ItemID")]
        [Required]
        public ToDoItem ToDoItem { get; set; }

    }
}