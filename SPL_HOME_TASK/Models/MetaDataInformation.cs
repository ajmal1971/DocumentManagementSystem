using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPL_HOME_TASK.Models
{
    public class MetaDataInformation
    {
        [Key]
        public long MetaIdentity { get; set; }

        [Required]
        [ForeignKey("DocumentIdentity")]
        public virtual DocumentInformation DocumentInformation { get; set; }
        public long DocumentIdentity { get; set; }

        [Required]
        [MaxLength(150)]
        public string MetaName { get; set; }

        [MaxLength(250)]
        public string MetaNameBangla { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}