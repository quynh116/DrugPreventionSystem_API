using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.BlogCategory;
using DrugPreventionSystem.BusinessLogic.Models.Responses.BlogCategory;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<Result<BlogCategoryResponse>> CreateAsync(BlogCategoryCreateRequest request);
        Task<Result<IEnumerable<BlogCategoryResponse>>> GetAllAsync();
        Task<Result<BlogCategoryResponse>> GetByIdAsync(Guid id);
        Task<Result<BlogCategoryResponse>> UpdateAsync(BlogCategoryUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
    }
}
