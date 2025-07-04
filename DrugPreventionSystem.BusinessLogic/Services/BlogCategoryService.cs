using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.BlogCategory;
using DrugPreventionSystem.BusinessLogic.Models.Responses.BlogCategory;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        private BlogCategoryResponse MapToResponse(BlogCategory category)
        {
            if (category == null) return null;

            return new BlogCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<Result<BlogCategoryResponse>> CreateAsync(BlogCategoryCreateRequest request)
        {
            var newCategory = new BlogCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.Now
            };

            var created = await _blogCategoryRepository.CreateAsync(newCategory);
            return Result<BlogCategoryResponse>.Success(MapToResponse(created));
        }

        public async Task<Result<IEnumerable<BlogCategoryResponse>>> GetAllAsync()
        {
            try
            {
                var categories = await _blogCategoryRepository.GetAllAsync();
                var responses = categories.Select(MapToResponse).ToList();
                return Result<IEnumerable<BlogCategoryResponse>>.Success(responses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<BlogCategoryResponse>>.Error($"Error fetching categories: {ex.Message}");
            }
        }

        public async Task<Result<BlogCategoryResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var category = await _blogCategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return Result<BlogCategoryResponse>.NotFound($"Category with ID {id} not found.");
                return Result<BlogCategoryResponse>.Success(MapToResponse(category));
            }
            catch (Exception ex)
            {
                return Result<BlogCategoryResponse>.Error($"Error fetching category: {ex.Message}");
            }
        }

        public async Task<Result<BlogCategoryResponse>> UpdateAsync(BlogCategoryUpdateRequest request, Guid id)
        {
            try
            {
                var category = await _blogCategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return Result<BlogCategoryResponse>.NotFound($"Category with ID {id} not found.");

                category.Name = request.Name;
                category.Description = request.Description;
                category.UpdatedAt = DateTime.Now;

                await _blogCategoryRepository.UpdateAsync(category);
                return Result<BlogCategoryResponse>.Success(MapToResponse(category));
            }
            catch (Exception ex)
            {
                return Result<BlogCategoryResponse>.Error($"Error updating category: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var category = await _blogCategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return Result<bool>.NotFound($"Category with ID {id} not found.");

                await _blogCategoryRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting category: {ex.Message}");
            }
        }
    }
}
