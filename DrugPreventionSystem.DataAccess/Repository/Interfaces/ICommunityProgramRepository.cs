using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface ICommunityProgramRepository
    {
        Task<CommunityProgram> GetProgramById(Guid id);
        Task<IEnumerable<CommunityProgram>> GetAllPrograms();
        Task<CommunityProgram> AddCommunityProgram(CommunityProgram program);
        Task<CommunityProgram> UpdateCommunityProgram(CommunityProgram program);
        Task DeleteCommunityProgram(Guid communityProgramId);
    }
}
