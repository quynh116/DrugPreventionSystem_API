using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class ProgramSurveyQuestionRepository : IProgramSurveyQuestionRepository
    {
        private readonly Context.ApplicationDbContext _context;
        public ProgramSurveyQuestionRepository(Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramSurveyQuestion>> GetBySurveyIdAsync(Guid surveyId)
        {
            return await _context.ProgramSurveyQuestions
                .Where(q => q.SurveyId == surveyId)
                .Include(q => q.AnswerOptions)
                .ToListAsync();
        }

        public async Task<ProgramSurveyQuestion?> GetByIdAsync(Guid questionId)
        {
            return await _context.ProgramSurveyQuestions
                .Include(q => q.AnswerOptions)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }

        public async Task AddAsync(ProgramSurveyQuestion question)
        {
            _context.ProgramSurveyQuestions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramSurveyQuestion question)
        {
            _context.ProgramSurveyQuestions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid questionId)
        {
            var question = await _context.ProgramSurveyQuestions.FindAsync(questionId);
            if (question != null)
            {
                _context.ProgramSurveyQuestions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }
    }
} 