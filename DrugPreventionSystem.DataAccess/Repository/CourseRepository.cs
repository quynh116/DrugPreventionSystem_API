using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.DataAccess.Context;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Course> CreateAsync(Course response )
        {
            await _context.Courses.AddAsync(response);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
           return await _context.Courses.ToListAsync();
        }
        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
        }
        public async Task UpdateAsync(Course response)
        {
            _context.Courses.Update(response);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var response = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (response != null)
            {
                _context.Courses.Remove(response);
                await _context.SaveChangesAsync();
            }
        }

    }
}
