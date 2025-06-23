using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class LessonResourceService : ILessonResourceService
    {
        private readonly ILessonResourceRepository _lessonResourceRepository;

        public LessonResourceService(ILessonResourceRepository lessonResourceRepository)
        {
            _lessonResourceRepository = lessonResourceRepository;
        }

        private LessonResourceResponse MapToResponse(LessonResource resource)
        {
            // Tri?n khai mapping c?a b?n
            return new LessonResourceResponse
            {
                ResourceId = resource.ResourceId,
                LessonId = resource.LessonId,
                ResourceType = resource.ResourceType,
                ResourceUrl = resource.ResourceUrl,
                Description = resource.Description
            };
        }

        public async Task<Result<LessonResource>> AddNewLessonResourceAsync(LessonResourceRequest lessonResource)
        {
            var newLessonResource = new LessonResource
            {
                LessonId = lessonResource.LessonId,
                ResourceType = lessonResource.ResourceType,
                ResourceUrl = lessonResource.ResourceUrl,
                Description = lessonResource.Description
            };

            var added = await _lessonResourceRepository.AddNewLessonResource(newLessonResource);
            return Result<LessonResource>.Success(added, "Added successfully");
        }

        public async Task<Result<IEnumerable<LessonResourceResponse>>> GetAllLessonResourcesAsync()
        {
            var list = await _lessonResourceRepository.GetAllLessonResourcesAsync();
            var responseList = list.Select(MapToResponse).ToList();
            return Result<IEnumerable<LessonResourceResponse>>.Success(responseList);
        }

        public async Task<Result<LessonResourceResponse>> GetLessonResourceByIdAsync(Guid id)
        {
            var lr = await _lessonResourceRepository.GetLessonResourceByIdAsync(id);
            if (lr == null) return Result<LessonResourceResponse>.NotFound($"Not found LessonResource with id: {id}");
            return Result<LessonResourceResponse>.Success(MapToResponse(lr));
        }

        public async Task<Result<bool>> DeleteLessonResourceByIdAsync(Guid id)
        {
            await _lessonResourceRepository.DeleteLessonResourceByIdAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }

        public async Task<Result<LessonResource>> UpdateLessonResourceAsync(Guid id, LessonResource lessonResource)
        {
            var existing = await _lessonResourceRepository.GetLessonResourceByIdAsync(id);
            if (existing == null) return Result<LessonResource>.NotFound($"Not found LessonResource with id: {id}");
            // Update fields
            existing.LessonId = lessonResource.LessonId;
            existing.ResourceType = lessonResource.ResourceType;
            existing.ResourceUrl = lessonResource.ResourceUrl;
            existing.Description = lessonResource.Description;
            await _lessonResourceRepository.UpdateLessonResourceAsync(existing);
            return Result<LessonResource>.Success(existing, "Updated successfully");
        }

        public async Task<Result<IEnumerable<LessonResourceResponse>>> GetResourcesByLessonIdAsync(Guid lessonId)
        {
            try
            {
                var resources = await _lessonResourceRepository.GetResourcesByLessonIdAsync(lessonId);
                if (resources == null || !resources.Any())
                {
                    return Result<IEnumerable<LessonResourceResponse>>.NotFound("Kh�ng t�m th?y t�i nguy�n n�o cho b�i h?c n�y.");
                }
                var resourceResponses = resources.Select(r => MapToResponse(r)).ToList();
                return Result<IEnumerable<LessonResourceResponse>>.Success(resourceResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<LessonResourceResponse>>.Error($"L?i khi l?y t�i nguy�n b�i h?c: {ex.Message}");
            }
        }
    }
} 