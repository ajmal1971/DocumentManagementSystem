using System.ComponentModel.DataAnnotations;

namespace SPL_HOME_TASK.ViewModels
{
    public class CreateOrEditDocumentCategoryViewModel
    {
        public int? CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryNameBangla { get; set; }

        public string Description { get; set; }
    }
}