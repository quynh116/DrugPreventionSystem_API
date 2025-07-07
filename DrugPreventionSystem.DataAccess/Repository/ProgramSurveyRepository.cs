using DrugPreventionSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class ProgramSurveyRepository
    {
        private readonly Context.ApplicationDbContext _context;
        public ProgramSurveyRepository(Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramSurvey>> GetAllAsync()
        {
            return await _context.ProgramSurveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.AnswerOptions)
                .Include(s => s.Programs)
                .ToListAsync();
        }

        public async Task<ProgramSurvey?> GetByIdAsync(Guid surveyId)
        {
            return await _context.ProgramSurveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.AnswerOptions)
                .Include(s => s.Programs)
                .FirstOrDefaultAsync(s => s.SurveyId == surveyId);
        }

        public async Task AddAsync(ProgramSurvey survey)
        {
            _context.ProgramSurveys.Add(survey);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramSurvey survey)
        {
            _context.ProgramSurveys.Update(survey);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid surveyId)
        {
            var survey = await _context.ProgramSurveys.FindAsync(surveyId);
            if (survey != null)
            {
                _context.ProgramSurveys.Remove(survey);
                await _context.SaveChangesAsync();
            }
        }
    }
} 