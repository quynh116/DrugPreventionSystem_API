using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using DrugPreventionSystem.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ProvideToken _provideToken;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IConsultantRepository _consultantRepository;

        public UserService(IUserRepository userRepositories, IUserProfileRepository userProfileRepository, IConsultantRepository consultantRepository, ProvideToken provideToken)
        {
            _userRepository = userRepositories;
            _provideToken = provideToken;
            _userProfileRepository = userProfileRepository;
            _consultantRepository = consultantRepository;
        }

        private UserResponse MapToUserResponse(User user)
        {
            if (user == null) return null;

            return new UserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role?.RoleName ?? "N/A",
                IsActive = user.IsActive,
                EmailVerified = user.EmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastLogin = user.LastLogin
            };
        }
        public async Task<Result<bool>> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<bool>.NotFound($"User with ID {id} not found.");
                }

                await _userRepository.DeleteUserAsync(id);
                return Result<bool>.Success(true, "User deleted.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting user: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var userResponses = users.Select(u => MapToUserResponse(u)).ToList();
                return Result<IEnumerable<UserResponse>>.Success(userResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserResponse>>.Error($"Error retrieving users: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<UserResponse>.NotFound($"User with ID {id} not found.");
                }
                return Result<UserResponse>.Success(MapToUserResponse(user));
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error retrieving user: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> RegisterMemberAsync(UserRegistrationRequest request)
        {
            try
            {
                
                if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                {
                    return Result<UserResponse>.Duplicated("Username already exists.");
                }
                if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                {
                    return Result<UserResponse>.Duplicated("Email already exists.");
                }

                // Lấy vai trò "Member"
                var memberRole = await _userRepository.GetRoleByNameAsync("Member");
                if (memberRole == null)
                {
                    return Result<UserResponse>.Error("Member role not found.");
                }

                var newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = PasswordHasher.HashPassword(request.Password),
                    RoleId = memberRole.RoleId,
                    IsActive = true,
                    EmailVerified = false,
                    CreatedAt = DateTime.Now
                };

                var addedUser = await _userRepository.AddUserAsync(newUser);

                var newUserProfile = new UserProfile
                {
                    ProfileId = Guid.NewGuid(),
                    UserId = addedUser.UserId,
                    CreatedAt = DateTime.Now
                };
                await _userProfileRepository.AddUserProfileAsync(newUserProfile);
                var userWithRole = await _userRepository.GetUserByIdAsync(addedUser.UserId);
                return Result<UserResponse>.Success(MapToUserResponse(userWithRole), "Member registered.");
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error during registration: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> UpdateUserAsync(Guid id, UserUpdateRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<UserResponse>.NotFound($"User with ID {id} not found.");
                }

                // Cập nhật các thuộc tính nếu được cung cấp
                if (!string.IsNullOrEmpty(request.Username) && user.Username != request.Username)
                {
                    if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                    {
                        return Result<UserResponse>.Duplicated("Username already exists.");
                    }
                    user.Username = request.Username;
                }

                if (!string.IsNullOrEmpty(request.Email) && user.Email != request.Email)
                {
                    if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                    {
                        return Result<UserResponse>.Duplicated("Email already exists.");
                    }
                    user.Email = request.Email;
                    user.EmailVerified = false;
                }

                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.PasswordHash = PasswordHasher.HashPassword(request.Password);
                }

                if (request.IsActive.HasValue)
                {
                    user.IsActive = request.IsActive.Value;
                }

                if (request.EmailVerified.HasValue)
                {
                    user.EmailVerified = request.EmailVerified.Value;
                }

                if (request.RoleId.HasValue && user.RoleId != request.RoleId.Value)
                {
                    var roleExists = await _userRepository.GetRoleByIdAsync(request.RoleId.Value);
                    if (roleExists != null)
                    {
                        user.RoleId = request.RoleId.Value;
                    }
                    else
                    {
                        return Result<UserResponse>.Invalid("Invalid role ID.");
                    }
                }

                user.UpdatedAt = DateTime.Now;
                await _userRepository.UpdateUserAsync(user);

                
                var updatedUserWithRole = await _userRepository.GetUserByIdAsync(user.UserId);
                return Result<UserResponse>.Success(MapToUserResponse(updatedUserWithRole), "User updated.");
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error updating user: {ex.Message}");
            }
        }

        public async Task<Result<LoginResponse>> LoginAsync(UserLoginRequest request)
        {
            try
            {
                
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    return Result<LoginResponse>.NotFound("Invalid email");
                }

                
                if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
                {
                    return Result<LoginResponse>.Failed("Invalid password");
                }

                
                if (!user.IsActive)
                {
                    return Result<LoginResponse>.Failed("Your account has been locked.");
                }

                //if (!user.EmailVerified)
                //{
                //    return Result<LoginResponse>.NotVerified("Your email is not verified.");
                //}

                
                user.LastLogin = DateTime.Now;
                await _userRepository.UpdateUserAsync(user); 

                
                var token = _provideToken.GenerateToken(user);
                var tokenExpires = _provideToken.GetTokenExpirationTime();

                
                var loginResponse = new LoginResponse
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role?.RoleName ?? "Member", 
                    Token = token,
                    TokenExpires = tokenExpires
                };

                return Result<LoginResponse>.Success(loginResponse, "Login successful.");
            }
            catch (InvalidOperationException ex) 
            {
                return Result<LoginResponse>.Error($"JWT configuration error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<LoginResponse>.Error($"Login failed: {ex.Message}");
            }
        }

        public async Task<Result<ChangePasswordResponse>> ChangePasswordAsync(Guid id, ChangePasswordRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<ChangePasswordResponse>.NotFound("User not found.");
                }

                if (!PasswordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash))
                {
                    return Result<ChangePasswordResponse>.Invalid("Current password is incorrect.");
                }

                user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);
                await _userRepository.UpdateUserAsync(user);

                return Result<ChangePasswordResponse>.Success(new ChangePasswordResponse 
                { 
                    Success = true,
                    Message = "Password changed successfully."
                });
            }
            catch (Exception ex)
            {
                return Result<ChangePasswordResponse>.Error($"Error changing password: {ex.Message}");
            }
        }

        public async Task<Result<ChangeRoleResponse>> ChangeUserRoleAsync(Guid userId, ChangeRoleRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return Result<ChangeRoleResponse>.NotFound($"User with ID {userId} not found.");
                }

                var role = await _userRepository.GetRoleByNameAsync(request.RoleName.ToString());
                if (role == null)
                {
                    return Result<ChangeRoleResponse>.Invalid($"Role '{request.RoleName}' not found.");
                }

                user.RoleId = role.RoleId;
                user.UpdatedAt = DateTime.Now;
                await _userRepository.UpdateUserAsync(user);

                var updatedUser = await _userRepository.GetUserByIdAsync(userId);
                return Result<ChangeRoleResponse>.Success(new ChangeRoleResponse
                {
                    UserId = updatedUser.UserId,
                    Username = updatedUser.Username,
                    Email = updatedUser.Email,
                    RoleId = updatedUser.RoleId,
                    RoleName = updatedUser.Role?.RoleName ?? "N/A",
                    Message = "User role updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Result<ChangeRoleResponse>.Error($"Error changing user role: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> RegisterUserByAdminAsync(AdminUserRegistrationRequest request)
        {
            try
            {
                if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                {
                    return Result<UserResponse>.Duplicated("Username already exists.");
                }
                if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                {
                    return Result<UserResponse>.Duplicated("Email already exists.");
                }

                var targetRole = await _userRepository.GetRoleByNameAsync(request.RoleName);
                if (targetRole == null)
                {
                    return Result<UserResponse>.Error($"Role '{request.RoleName}' not found.");
                }

                var newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = PasswordHasher.HashPassword(request.Password),
                    RoleId = targetRole.RoleId,
                    IsActive = true,
                    EmailVerified = false,
                    CreatedAt = DateTime.Now
                };

                var addedUser = await _userRepository.AddUserAsync(newUser);

                
                var newUserProfile = new UserProfile
                {
                    ProfileId = Guid.NewGuid(),
                    UserId = addedUser.UserId,
                    CreatedAt = DateTime.Now
                };
                await _userProfileRepository.AddUserProfileAsync(newUserProfile);

                // Nếu role là Consultant, tạo thêm Consultant profile
                if (request.RoleName.Equals("Consultant", StringComparison.OrdinalIgnoreCase))
                {
                    var newConsultant = new Consultant
                    {
                        ConsultantId = Guid.NewGuid(),
                        UserId = addedUser.UserId,
                        IsAvailable = true,         
                        TotalConsultations = 0,     
                        CreatedAt = DateTime.Now
                        
                    };
                    await _consultantRepository.AddConsultant(newConsultant);
                }

                var userWithRole = await _userRepository.GetUserByIdAsync(addedUser.UserId);
                return Result<UserResponse>.Success(MapToUserResponse(userWithRole), $"{request.RoleName} registered successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error during user registration by admin: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<RoleResponse>>> GetManagementRolesAsync()
        {
            try
            {
                var specificRoles = new string[] { "Manager", "Staff", "Consultant" };
                var roles = await _userRepository.GetSpecificRolesAsync(specificRoles);

                if (roles == null || !roles.Any())
                {
                    return Result<IEnumerable<RoleResponse>>.NotFound("No management roles found.");
                }

                var roleResponses = roles.Select(r => new RoleResponse
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    Description = r.Description
                });

                return Result<IEnumerable<RoleResponse>>.Success(roleResponses, "Management roles retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<RoleResponse>>.Error($"Error retrieving management roles: {ex.Message}");
            }
        }
    }
}
