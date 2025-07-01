using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("blog_categories")]
    public class BlogCategory
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid(); // Đổi từ int sang Guid

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description", TypeName = "text")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
