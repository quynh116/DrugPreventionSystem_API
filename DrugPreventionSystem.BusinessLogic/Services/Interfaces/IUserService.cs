﻿using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync();
        Task<Result<UserResponse>> GetUserByIdAsync(Guid id);
        Task<Result<UserResponse>> RegisterMemberAsync(UserRegistrationRequest request); 
        Task<Result<UserResponse>> UpdateUserAsync(Guid id, UserUpdateRequest request); 
        Task<Result<bool>> DeleteUserAsync(Guid id);
        Task<Result<LoginResponse>> LoginAsync(UserLoginRequest request);
        Task<Result<ChangePasswordResponse>> ChangePasswordAsync(Guid id, ChangePasswordRequest request);
        Task<Result<ChangeRoleResponse>> ChangeUserRoleAsync(Guid userId, ChangeRoleRequest request);
        Task<Result<UserResponse>> RegisterUserByAdminAsync(AdminUserRegistrationRequest request);
        Task<Result<IEnumerable<RoleResponse>>> GetManagementRolesAsync();
    }
}
