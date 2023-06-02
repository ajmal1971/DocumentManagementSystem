using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPL_HOME_TASK.Models
{
    public class FileInformation
    {
        [Key]
        public long FileIdentity { get; set; }

        [Required]
        [ForeignKey("DocumentIdentity")]
        public virtual DocumentInformation DocumentInformation { get; set; }
        public long DocumentIdentity { get; set; }

        [MaxLength(50)]
        public string FileNo { get; set; }

        [Required]
        [MaxLength(250)]
        public string FileName { get; set; }

        [MaxLength(450)]
        public string FileNameBangla { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(250)]
        public string FilePath { get; set; }

        [MaxLength(20)]
        public string FileStatus { get; set; }
    }
}