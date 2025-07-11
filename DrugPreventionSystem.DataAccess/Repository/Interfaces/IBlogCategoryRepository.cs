using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface IBlogCategoryRepository
    {
        Task<BlogCategory> CreateAsync(BlogCategory category);
        Task<IEnumerable<BlogCategory>> GetAllAsync();
        Task<BlogCategory?> GetByIdAsync(Guid id);
        Task UpdateAsync(BlogCategory category);
        Task DeleteAsync(Guid id);
    }
}
