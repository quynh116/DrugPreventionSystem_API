using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Blog;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Blog;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        private BlogResponse MapToResponse(Blog blog)
        {
            if (blog == null) return null;

            return new BlogResponse
            {
                Id = blog.Id,
                UserId = blog.UserId,
                Username = blog.User.Username,
                Title = blog.Title,
                Content = blog.Content,
                Excerpt = blog.Excerpt,
                ThumbnailUrl = blog.ThumbnailUrl,
                CategoryId = blog.CategoryId,
                Tags = blog.Tags,
                Status = blog.Status,
                PublishedAt = blog.PublishedAt,
                ViewsCount = blog.ViewsCount,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
            };
        }

        public async Task<Result<BlogResponse>> CreateAsync(BlogCreateRequest request)
        {
            var newBlog = new Blog
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title,
                Content = request.Content,
                Excerpt = request.Excerpt,
                ThumbnailUrl = request.ThumbnailUrl,
                CategoryId = request.CategoryId,
                Tags = request.Tags,
                Status = request.Status,
                PublishedAt = request.PublishedAt,
                ViewsCount = 0,
                CreatedAt = DateTime.Now,
            };

            await _blogRepository.CreateAsync(newBlog);
            var createdBlog = await _blogRepository.GetByIdAsync(newBlog.Id);
            return Result<BlogResponse>.Success(MapToResponse(createdBlog));
        }

        public async Task<Result<IEnumerable<BlogResponse>>> GetAllAsync()
        {
            try
            {
                var blogs = await _blogRepository.GetAllAsync();
                var responses = blogs.Select(MapToResponse).ToList();
                return Result<IEnumerable<BlogResponse>>.Success(responses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<BlogResponse>>.Error($"Error fetching blog list: {ex.Message}");
            }
        }

        public async Task<Result<BlogResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(id);
                if (blog == null)
                    return Result<BlogResponse>.NotFound($"Blog with ID {id} not found.");
                return Result<BlogResponse>.Success(MapToResponse(blog));
            }
            catch (Exception ex)
            {
                return Result<BlogResponse>.Error($"Error fetching blog: {ex.Message}");
            }
        }

        public async Task<Result<BlogResponse>> UpdateAsync(BlogUpdateRequest request, Guid id)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(id);
                if (blog == null)
                    return Result<BlogResponse>.NotFound($"Blog with ID {id} not found.");

                blog.Title = request.Title;
                blog.Content = request.Content;
                blog.Excerpt = request.Excerpt;
                blog.ThumbnailUrl = request.ThumbnailUrl;
                blog.CategoryId = request.CategoryId;
                blog.Tags = request.Tags;
                blog.Status = request.Status;
                blog.UpdatedAt = DateTime.Now;

                await _blogRepository.UpdateAsync(blog);
                var updatedBlog = await _blogRepository.GetByIdAsync(id);
                return Result<BlogResponse>.Success(MapToResponse(updatedBlog));
            }
            catch (Exception ex)
            {
                return Result<BlogResponse>.Error($"Error updating blog: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(id);
                if (blog == null)
                    return Result<bool>.NotFound($"Blog with ID {id} not found.");

                await _blogRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Blog deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting blog: {ex.Message}");
            }
        }
    }
}
