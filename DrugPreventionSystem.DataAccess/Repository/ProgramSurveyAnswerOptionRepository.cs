using DrugPreventionSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class ProgramSurveyAnswerOptionRepository
    {
        private readonly Context.ApplicationDbContext _context;
        public ProgramSurveyAnswerOptionRepository(Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramSurveyAnswerOption>> GetByQuestionIdAsync(Guid questionId)
        {
            return await _context.ProgramSurveyAnswerOptions
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();
        }

        public async Task<ProgramSurveyAnswerOption?> GetByIdAsync(Guid optionId)
        {
            return await _context.ProgramSurveyAnswerOptions
                .FirstOrDefaultAsync(o => o.OptionId == optionId);
        }

        public async Task AddAsync(ProgramSurveyAnswerOption option)
        {
            _context.ProgramSurveyAnswerOptions.Add(option);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramSurveyAnswerOption option)
        {
            _context.ProgramSurveyAnswerOptions.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid optionId)
        {
            var option = await _context.ProgramSurveyAnswerOptions.FindAsync(optionId);
            if (option != null)
            {
                _context.ProgramSurveyAnswerOptions.Remove(option);
                await _context.SaveChangesAsync();
            }
        }
    }
} 