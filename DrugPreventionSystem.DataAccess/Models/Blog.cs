using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("blogs")]
    public class Blog
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid(); // Đổi từ int sang Guid

        [Required]
        [MaxLength(500)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("content", TypeName = "text")]
        public string? Content { get; set; }

        [Column("excerpt", TypeName = "text")]
        public string? Excerpt { get; set; }

        [MaxLength(1000)]
        [Column("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [ForeignKey("Category")]
        [Column("category_id")]
        public Guid? CategoryId { get; set; } // Đổi từ int? sang Guid?

        [Column("tags")]
        public string? Tags { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "draft";

        [Column("published_at")]
        public DateTime? PublishedAt { get; set; }

        [Column("views_count")]
        public int ViewsCount { get; set; } = 0;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        public virtual BlogCategory? Category { get; set; }
    }
}
