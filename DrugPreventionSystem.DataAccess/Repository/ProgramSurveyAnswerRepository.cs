using DrugPreventionSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class ProgramSurveyAnswerRepository
    {
        private readonly Context.ApplicationDbContext _context;
        public ProgramSurveyAnswerRepository(Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramSurveyAnswer>> GetByResponseIdAsync(Guid responseId)
        {
            return await _context.ProgramSurveyAnswers
                .Where(a => a.ResponseId == responseId)
                .ToListAsync();
        }

        public async Task<ProgramSurveyAnswer?> GetByIdAsync(Guid answerId)
        {
            return await _context.ProgramSurveyAnswers
                .FirstOrDefaultAsync(a => a.AnswerId == answerId);
        }

        public async Task AddAsync(ProgramSurveyAnswer answer)
        {
            _context.ProgramSurveyAnswers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramSurveyAnswer answer)
        {
            _context.ProgramSurveyAnswers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid answerId)
        {
            var answer = await _context.ProgramSurveyAnswers.FindAsync(answerId);
            if (answer != null)
            {
                _context.ProgramSurveyAnswers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }
    }
} 