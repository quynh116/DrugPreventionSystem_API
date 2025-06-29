﻿using System;
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
    public class CommunityProgramRepository : ICommunityProgramRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunityProgramRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommunityProgram> AddCommunityProgram(CommunityProgram program)
        {
            await _context.CommunityPrograms.AddAsync(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task DeleteCommunityProgram(Guid communityProgramId)
        {
            var communityProgram = await _context.CommunityPrograms.FindAsync(communityProgramId);
            if (communityProgram != null)
            {
                _context.CommunityPrograms.Remove(communityProgram);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<IEnumerable<CommunityProgram>> GetAllPrograms()
        {
            return await _context.CommunityPrograms.ToListAsync();
        }

        public async Task<CommunityProgram> GetProgramById(Guid id)
        {
            return await _context.CommunityPrograms.FirstOrDefaultAsync(cp => cp.ProgramId == id);
        }

        public async Task<CommunityProgram> UpdateCommunityProgram(CommunityProgram program)
        {
            _context.Update(program);
            await _context.SaveChangesAsync();
            return program;
        }
    }
}
