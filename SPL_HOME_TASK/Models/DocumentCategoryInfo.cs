using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPL_HOME_TASK.Models
{
    public class DocumentCategoryInfo
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string CategoryName { get; set; }

        [StringLength(250)]
        public string CategoryNameBangla { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}