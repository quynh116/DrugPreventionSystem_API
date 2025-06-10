using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly IConsultantRepository _consultantRepository;
        private readonly ProvideToken _provideToken;
        public ConsultantService(IConsultantRepository consultantRepositories, ProvideToken provideToken)
        {
            _consultantRepository = consultantRepositories;
            _provideToken = provideToken;
        }
        private ConsultantUpdateResponse MapToConsultantResponse(Consultant consultant)
        {
            if (consultant == null) return null;
            return new ConsultantUpdateResponse
            {
                ConsultantId = consultant.ConsultantId.ToString(),
                LicenseNumber = consultant.LicenseNumber,
                Specialization = consultant.Specialization,
                YearsOfExperience = consultant.YearsOfExperience,
                Qualifications = consultant.Qualifications,
                Bio = consultant.Bio,
                ConsultationFee = consultant.ConsultationFee,
                IsAvailable = consultant.IsAvailable,
                WorkingHours = consultant.WorkingHours,
                Rating = consultant.Rating,
                TotalConsultations = consultant.TotalConsultations
            };
        }
        public async Task<Result<IEnumerable<ConsultantReadResponse>>> GetAllConsultantAsync()
        {
            try
            {
                var consultants = await _consultantRepository.GetAllConsultantsAsync();
                var consultantResponses = consultants.Select(c => MapToConsultantReadResponse(c)).ToList(); // Sử dụng ánh xạ đúng
                return Result<IEnumerable<ConsultantReadResponse>>.Success(consultantResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ConsultantReadResponse>>.Error($"consultants is null here: {ex.Message}");
            }
        }
        public async Task<Result<ConsultantReadResponse>> GetConsultantByIdAsync(Guid consultantId)
        {
            try
            {
                var consultant = await _consultantRepository.GetConsultantByIdAsync(consultantId);
                if (consultant == null)
                {
                    return Result<ConsultantReadResponse>.NotFound($"Consultant with ID {consultantId} not found.");
                }
                return Result<ConsultantReadResponse>.Success(MapToConsultantReadResponse(consultant));
            }
            catch (Exception ex)
            {
                return Result<ConsultantReadResponse>.Error($"Error retrieving consultant: {ex.Message}");
            }
        }
        private ConsultantReadResponse MapToConsultantReadResponse(Consultant consultant)
        {
            return new ConsultantReadResponse
            {
                //ConsultantId = consultant.ConsultantId,
                LicenseNumber = consultant.LicenseNumber,
                Specialization = consultant.Specialization,
                YearsOfExperience = consultant.YearsOfExperience,
                Qualifications = consultant.Qualifications,
                Bio = consultant.Bio,
                ConsultationFee = consultant.ConsultationFee,
                IsAvailable = consultant.IsAvailable,
                WorkingHours = consultant.WorkingHours,
                Rating = consultant.Rating,
                TotalConsultations = consultant.TotalConsultations
            };
        }
        public async Task<Result<ConsultantUpdateResponse>> UpdateConsultantAsync(Guid consultantId, ConsultantUpdateRequest request)
        {
            try
            {
                var consultant = await _consultantRepository.GetConsultantByIdAsync(consultantId);
                if (consultant == null)
                {
                    return Result<ConsultantUpdateResponse>.NotFound($"Consultant with ID {consultantId} not found.");
                }

                // Cập nhật nếu có dữ liệu mới
                if (!string.IsNullOrEmpty(request.LicenseNumber))
                    consultant.LicenseNumber = request.LicenseNumber;

                if (!string.IsNullOrEmpty(request.Specialization))
                    consultant.Specialization = request.Specialization;

                if (request.YearsOfExperience.HasValue)
                    consultant.YearsOfExperience = request.YearsOfExperience;

                if (!string.IsNullOrEmpty(request.Qualifications))
                    consultant.Qualifications = request.Qualifications;

                if (!string.IsNullOrEmpty(request.Bio))
                    consultant.Bio = request.Bio;

                if (request.ConsultationFee.HasValue)
                    consultant.ConsultationFee = request.ConsultationFee;

                if (request.IsAvailable.HasValue)
                    consultant.IsAvailable = request.IsAvailable.Value;

                if (!string.IsNullOrEmpty(request.WorkingHours))
                    consultant.WorkingHours = request.WorkingHours;

                await _consultantRepository.UpdateConsultantAsync(consultant);

                return Result<ConsultantUpdateResponse>.Success(MapToConsultantResponse(consultant), "Consultant updated.");
            }
            catch (Exception ex)
            {
                return Result<ConsultantUpdateResponse>.Error($"Error updating consultant: {ex.Message}");
            }
        }
        public async Task<Result<bool>> DeleteConsultantAsync(Guid id)
        {
            try
            {
                var user = await _consultantRepository.GetConsultantByIdAsync(id);
                if (user == null)
                {
                    return Result<bool>.NotFound($"User with ID {id} not found.");
                }

                await _consultantRepository.DeleteConsultantAsync(id);
                return Result<bool>.Success(true, "Consultant deleted.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting Consultant: {ex.Message}");
            }
        }


    }
}
