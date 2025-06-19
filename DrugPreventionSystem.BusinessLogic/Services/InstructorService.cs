using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Models.Request.Instructor;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ProvideToken _provideToken;

        public InstructorService(IInstructorRepository instructorRepository, ProvideToken provideToken)
        {
            _instructorRepository = instructorRepository;
            _provideToken = provideToken;
        }
        private InstructorResponse MapToResponse(Instructor instructor)
        {
            if (instructor == null)
            {
                return null;
            }
            return new InstructorResponse()
            {
               InstructorId =instructor.InstructorId,
               FullName = instructor.FullName,
               Bio= instructor.Bio,
               ProfileImageUrl = instructor.ProfileImageUrl,
               ExperienceYears = instructor.ExperienceYears,
               CreatedAt = instructor.CreatedAt,
            };
        }

        public async Task<Result<InstructorResponse>> CreateAsync(InstructorCreateRequest request)
        {
            var newInstructor = new Instructor()
            {
                InstructorId = Guid.NewGuid(),
                FullName = request.FullName,
                Bio = request.Bio,
                ProfileImageUrl = request.ProfileImageUrl,
                ExperienceYears = request.ExperienceYears,
                CreatedAt = request.CreatedAt,
            };
            var createdInstructor = await _instructorRepository.CreateAsync(newInstructor);
            return Result<InstructorResponse>.Success(MapToResponse(createdInstructor));
        }

        public async Task<Result<IEnumerable<InstructorResponse>>> GetAllAsync()
        {
            try
            {
                var instructors = await _instructorRepository.GetAllAsync();
                var instructorResponses = instructors.Select(c => MapToResponse(c)).ToList();
                return Result<IEnumerable<InstructorResponse>>.Success(instructorResponses);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<InstructorResponse>>.Error($"Instructor is null here :{ex.Message}");

            }
        }
        public async Task<Result<InstructorResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var instructor = await _instructorRepository.GetByIdAsync(id);
                if(instructor == null)
                {
                    return Result<InstructorResponse>.NotFound($"Instructor with ID {id} not found.");

                }
                return Result<InstructorResponse>.Success(MapToResponse(instructor));
            }
            catch (Exception ex)
            {
                return Result<InstructorResponse>.Error($"Instructor is null here :{ex.Message}");
            }
        }
        public async Task<Result<InstructorResponse>> UpdateAsync(InstructorUpdateRequest request, Guid id)
        {
            try
            {
                var instructor = await _instructorRepository.GetByIdAsync(id);
                if (instructor == null)
                {
                    return Result<InstructorResponse>.NotFound($"Instructor with ID {id} not found.");
                }
                
                instructor.FullName = request.FullName;
                instructor.Bio = request.Bio;
                instructor.ProfileImageUrl = request.ProfileImageUrl;
                instructor.ExperienceYears = request.ExperienceYears;
 
                await _instructorRepository.UpdateAsync(instructor);
                return Result<InstructorResponse>.Success(MapToResponse(instructor));
            }
            catch (Exception ex)
            {
                return Result<InstructorResponse>.Error($"Error updating Instructor: {ex.Message} ");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var course = await _instructorRepository.GetByIdAsync(id);
            try
            {
                if (course == null)
                {
                    return Result<bool>.NotFound($"Instructor with ID {id} not found.");

                }
                await _instructorRepository.DeleteAsync(id);
                return Result<bool>.Success(true, $"Instructor delete successfully.");

            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Instructor is null here :{ex.Message}");
            }
        }
    }
}
