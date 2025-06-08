namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class ChangeRoleResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
} 