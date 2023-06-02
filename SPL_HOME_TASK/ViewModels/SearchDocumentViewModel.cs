using System;

namespace SPL_HOME_TASK.ViewModels
{
    public class SearchDocumentViewModel
    {
        public int? CategoryId { get; set; }

        public string DocumentName { get; set; }

        public string DocumentReferenceName { get; set; }

        public string MetaName { get; set; }

        public string FileName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}