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
        public int Id { get; set; } // int PK with auto-increment

        [Required]
        [MaxLength(500)] // Adjust max length as needed
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("content", TypeName = "text")] // Content can be HTML or Markdown
        public string? Content { get; set; }

        [Column("excerpt", TypeName = "text")] // Short description for list view
        public string? Excerpt { get; set; }

        [MaxLength(1000)] // Adjust max length for URL
        [Column("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [ForeignKey("Category")] // Foreign key to BlogCategory
        [Column("category_id")]
        public int? CategoryId { get; set; } // Nullable if a blog can exist without a category

        [Column("tags")]
        public string? Tags { get; set; } // Can be a JSON string or CSV string for tags

        [Required]
        [MaxLength(50)] // e.g., "draft", "published", "archived"
        [Column("status")]
        public string Status { get; set; } = "draft";

        [Column("published_at")]
        public DateTime? PublishedAt { get; set; } // Time when the blog was published

        [Column("views_count")]
        public int ViewsCount { get; set; } = 0;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation property to BlogCategory
        public virtual BlogCategory? Category { get; set; }
    }
}

