using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserResponse>>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserResponse>>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("roles")]
        
        public async Task<ActionResult<Result<IEnumerable<RoleResponse>>>> GetManagementRoles()
        {
            var result = await _userService.GetManagementRolesAsync();
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result<LoginResponse>>> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<LoginResponse>.Invalid("Invalid login data.", errors));
            }

            var result = await _userService.LoginAsync(request);
            return HandleResult(result);
        }

        [HttpPost("register-member")]
        public async Task<ActionResult<Result<UserResponse>>> RegisterMember([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserResponse>.Invalid("Invalid registration data.", errors));
            }
            var result = await _userService.RegisterMemberAsync(request);
            if (result.ResultStatus == ResultStatus.Success && result.Data != null)
            {

                return CreatedAtAction(nameof(GetUserById), new { id = result.Data.UserId }, result);
            }
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<UserResponse>>> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserResponse>.Invalid("Invalid update data.", errors));
            }

            var result = await _userService.UpdateUserAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }

        [HttpPut("{id}/change-password")]
        public async Task<ActionResult<Result<ChangePasswordResponse>>> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<ChangePasswordResponse>.Invalid("Invalid password change data.", errors));
            }

            var result = await _userService.ChangePasswordAsync(id, request);
            return HandleResult(result);
        }

        [HttpPut("{id}/role")]
        public async Task<ActionResult<Result<ChangeRoleResponse>>> ChangeUserRole(Guid id, [FromBody] ChangeRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<ChangeRoleResponse>.Invalid("Invalid role change data.", errors));
            }

            var result = await _userService.ChangeUserRoleAsync(id, request);
            return HandleResult(result);
        }

        [HttpPost("admin/Create-user")]
        public async Task<ActionResult<Result<UserResponse>>> CreateUserByAdmin([FromBody] AdminUserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserResponse>.Invalid("Invalid registration data.", errors));
            }

            
            var result = await _userService.RegisterUserByAdminAsync(request);

            if (result.ResultStatus == ResultStatus.Success && result.Data != null)
            {
                
                return CreatedAtAction(nameof(GetUserById), new { id = result.Data.UserId }, result);
            }

            
            return HandleResult(result);
        }
    }
}
