using System;
using System.ComponentModel.DataAnnotations;

namespace SPL_HOME_TASK.Models
{
    public class DocumentInformation
    {
        [Key]
        public long DocumentIdentity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(150)]
        public string DocumentReferenceName { get; set; }

        public DateTime DocumentDate { get; set; }

        [Required]
        [MaxLength(250)]
        public string DocumentName { get; set; }

        [MaxLength(500)]
        public string DocumentNameBangla { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}