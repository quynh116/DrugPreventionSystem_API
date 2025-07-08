using DrugPreventionSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class ProgramSurveyResponseRepository
    {
        private readonly Context.ApplicationDbContext _context;
        public ProgramSurveyResponseRepository(Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramSurveyResponse>> GetBySurveyIdAsync(Guid surveyId)
        {
            return await _context.ProgramSurveyResponses
                .Where(r => r.SurveyId == surveyId)
                .Include(r => r.Answers)
                .ToListAsync();
        }

        public async Task<ProgramSurveyResponse?> GetByIdAsync(Guid responseId)
        {
            return await _context.ProgramSurveyResponses
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.ResponseId == responseId);
        }

        public async Task AddAsync(ProgramSurveyResponse response)
        {
            _context.ProgramSurveyResponses.Add(response);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramSurveyResponse response)
        {
            _context.ProgramSurveyResponses.Update(response);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid responseId)
        {
            var response = await _context.ProgramSurveyResponses.FindAsync(responseId);
            if (response != null)
            {
                _context.ProgramSurveyResponses.Remove(response);
                await _context.SaveChangesAsync();
            }
        }
    }
} 