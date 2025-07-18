﻿using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.BlogCategory
{
    public class BlogCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
