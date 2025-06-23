using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class PracticeExerciseService : IPracticeExerciseService
    {
        private readonly IPracticeExerciseRepository _repo;
        public PracticeExerciseService(IPracticeExerciseRepository repo)
        {
            _repo = repo;
        }
        private PracticeExerciseResponse MapToResponse(PracticeExercise entity)
        {
            return new PracticeExerciseResponse
            {
                ExerciseId = entity.ExerciseId,
                LessonId = entity.LessonId,
                Instruction = entity.Instruction,
                AttachmentUrl = entity.AttachmentUrl,
                CreatedAt = entity.CreatedAt
            };
        }
        public async Task<Result<PracticeExerciseResponse>> AddAsync(PracticeExerciseRequest request)
        {
            var entity = new PracticeExercise
            {
                LessonId = request.LessonId,
                Instruction = request.Instruction,
                AttachmentUrl = request.AttachmentUrl
            };
            var added = await _repo.AddAsync(entity);
            return Result<PracticeExerciseResponse>.Success(MapToResponse(added), "Added successfully");
        }
        public async Task<Result<IEnumerable<PracticeExerciseResponse>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return Result<IEnumerable<PracticeExerciseResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<PracticeExerciseResponse>> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<PracticeExerciseResponse>.NotFound($"Not found PracticeExercise with id: {id}");
            return Result<PracticeExerciseResponse>.Success(MapToResponse(entity));
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }
        public async Task<Result<PracticeExerciseResponse>> UpdateAsync(Guid id, PracticeExerciseRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<PracticeExerciseResponse>.NotFound($"Not found PracticeExercise with id: {id}");
            entity.Instruction = request.Instruction;
            entity.AttachmentUrl = request.AttachmentUrl;
            entity.LessonId = request.LessonId;
            await _repo.UpdateAsync(entity);
            return Result<PracticeExerciseResponse>.Success(MapToResponse(entity), "Updated successfully");
        }
        public async Task<Result<IEnumerable<PracticeExerciseResponse>>> GetByLessonIdAsync(Guid lessonId)
        {
            var list = await _repo.GetByLessonIdAsync(lessonId);
            return Result<IEnumerable<PracticeExerciseResponse>>.Success(list.Select(MapToResponse));
        }
    }
} 