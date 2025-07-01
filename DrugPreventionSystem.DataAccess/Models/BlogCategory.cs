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
        public int Id { get; set; } // int PK with auto-increment

        [Required]
        [MaxLength(255)] // Adjust max length as needed
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description", TypeName = "text")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation collection for Blogs
        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}