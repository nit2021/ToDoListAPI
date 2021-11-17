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
        public int ? ToDoItemID { get; set; }
        public int ? ToDoListID { get; set; }
        
       

    }
}