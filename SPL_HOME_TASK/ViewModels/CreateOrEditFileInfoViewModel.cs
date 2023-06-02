using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SPL_HOME_TASK.ViewModels
{
    public class CreateOrEditFileInfoViewModel
    {
        public long? FileIdentity { get; set; }

        public string FileNo { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FileNameBangla { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string FilePath { get; set; }

        public string FileStatus { get; set; }
    }
}