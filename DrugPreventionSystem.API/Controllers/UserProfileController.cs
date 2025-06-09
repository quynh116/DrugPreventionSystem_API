using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService) 
        {
            _userProfileService = userProfileService;
        }

        private ActionResult<Result<T>> HandleResult<T>(Result<T> result)
        {
            return result.ResultStatus switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.NotFound => NotFound(result),
                ResultStatus.Duplicated => Conflict(result), // 409 
                ResultStatus.Invalid => BadRequest(result), // 400 
                ResultStatus.Failed => BadRequest(result),
                ResultStatus.NotVerified => Unauthorized(result), // 401 
                ResultStatus.Error => StatusCode(500, result), // 500 
                ResultStatus.Failure => StatusCode(500, result), // 
                _ => StatusCode(500, Result<T>.Error("Unknown error.")),
            };
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserProfileResponse>>>> GetAllUsersProfile()
        {
            var result = await _userProfileService.GetAllUsersProfileAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserProfileResponse>>> GetUserProfileById(Guid id)
        {
            var result = await _userProfileService.GetUserProfileByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<UserProfileResponse>>> UpdateUserProfile(Guid id, [FromBody] UserProfileUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserProfileResponse>.Invalid("Invalid update data.", errors));
            }

            var result = await _userProfileService.UpdateProfileUserAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteUser(Guid id)
        {
            var result = await _userProfileService.DeleteUserProfileAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
