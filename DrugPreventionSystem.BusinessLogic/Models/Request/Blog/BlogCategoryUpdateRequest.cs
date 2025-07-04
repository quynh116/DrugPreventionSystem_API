using System.ComponentModel.DataAnnotations;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.BlogCategory
{
    public class BlogCategoryUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
