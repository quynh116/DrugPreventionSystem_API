using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class ChangeRoleRequest
    {
        [Required(ErrorMessage = "Role name is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleType RoleName { get; set; }
    }

    public enum RoleType
    {
        Admin = 1,
        Manager = 2,
        Staff = 3,
        Consultant = 4,
        Member = 5
    }
} 