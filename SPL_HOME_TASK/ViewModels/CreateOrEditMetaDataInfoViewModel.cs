using System.ComponentModel.DataAnnotations;

namespace SPL_HOME_TASK.ViewModels
{
    public class CreateOrEditMetaDataInfoViewModel
    {
        public long? MetaIdentity { get; set; }

        [Required]
        public string MetaName { get; set; }

        public string MetaNameBangla { get; set; }

        public string Description { get; set; }
    }
}