using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class LessonResourceService : ILessonResourceService
    {
        private readonly ILessonResourceRepository _lessonResourceRepository;

        public LessonResourceService(ILessonResourceRepository lessonResourceRepository)
        {
            _lessonResourceRepository = lessonResourceRepository;
        }

        public async Task<Result<LessonResource>> AddNewLessonResourceAsync(LessonResource lessonResource)
        {
            var added = await _lessonResourceRepository.AddNewLessonResource(lessonResource);
            return Result<LessonResource>.Success(added, "Added successfully");
        }

        public async Task<Result<IEnumerable<LessonResource>>> GetAllLessonResourcesAsync()
        {
            var list = await _lessonResourceRepository.GetAllLessonResourcesAsync();
            return Result<IEnumerable<LessonResource>>.Success(list);
        }

        public async Task<Result<LessonResource>> GetLessonResourceByIdAsync(Guid id)
        {
            var lr = await _lessonResourceRepository.GetLessonResourceByIdAsync(id);
            if (lr == null) return Result<LessonResource>.NotFound($"Not found LessonResource with id: {id}");
            return Result<LessonResource>.Success(lr);
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
    }
} 