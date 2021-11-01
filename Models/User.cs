using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListAPI.Models;

namespace ToDoListAPI.Models
{
    public class User
    {

        /// <summary>
        /// Optional.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [ForeignKey("Owner")]
        [Required]
        public ICollection<ToDoItem> ToDoItems { get; set; }

    }
}