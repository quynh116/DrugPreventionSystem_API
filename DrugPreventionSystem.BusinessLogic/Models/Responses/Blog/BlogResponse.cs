using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Blog
{
    public class BlogResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; } 
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public string? ThumbnailUrl { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Tags { get; set; }
        public string Status { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ViewsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
