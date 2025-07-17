using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class AdminDashboardSummaryDto
    {
        public int TotalUsers { get; set; }
        public int TotalCourseEnrollments { get; set; }
        public int TotalCompletedSurveys { get; set; }
        public int TotalProgramParticipations { get; set; }
    }
}
