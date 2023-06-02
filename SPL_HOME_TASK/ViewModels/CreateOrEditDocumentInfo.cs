using SPL_HOME_TASK.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPL_HOME_TASK.ViewModels
{
    public class CreateOrEditDocumentInfo
    {
        public long? DocumentIdentity { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string DocumentReferenceName { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public string DocumentName { get; set; }

        public string DocumentNameBangla { get; set; }

        public string Description { get; set; }

        public List<CreateOrEditMetaDataInfoViewModel> MetaDataInfos { get; set; }
        public List<CreateOrEditFileInfoViewModel> FileInfos { get; set; }
    }
}