using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Blog;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Blog;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Result<BlogResponse>> CreateAsync(BlogCreateRequest request);  
        Task<Result<IEnumerable<BlogResponse>>> GetAllAsync();
        Task<Result<BlogResponse>> GetByIdAsync(Guid id);
        Task<Result<BlogResponse>> UpdateAsync(BlogUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);

    }
}
