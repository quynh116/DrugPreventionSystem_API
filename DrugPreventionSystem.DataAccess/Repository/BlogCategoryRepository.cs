using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugPreventionSystem.DataAccess.Context;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogCategory> CreateAsync(BlogCategory category)
        {
            await _context.BlogCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<BlogCategory>> GetAllAsync()
        {
            return await _context.BlogCategories.Include(c => c.Blogs).ToListAsync();
        }

        public async Task<BlogCategory?> GetByIdAsync(Guid id)
        {
            return await _context.BlogCategories.Include(c => c.Blogs)
                                                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(BlogCategory category)
        {
            _context.BlogCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.BlogCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _context.BlogCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
