using System;

namespace SPL_HOME_TASK.ViewModels
{
    public class DocumentInformationsViewModel
    {
        public long DocumentIdentity { get; set; }

        public string CategoryName { get; set; }

        public string DocumentReferenceName { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentName { get; set; }

        public string DocumentNameBangla { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
    }
}