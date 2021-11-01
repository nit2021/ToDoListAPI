using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ToDoListAPI.Models
{
    public partial class ToDoItem
    {
        /// <summary>
        /// Optional.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

    }
}