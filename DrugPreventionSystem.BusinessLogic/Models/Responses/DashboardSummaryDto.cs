using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class DashboardSummaryDto
    {
        public int TotalUsers { get; set; }
        public int TotalCourses { get; set; }
        public int Totalblogs { get; set; }
        public int TotalPrograms { get; set; }
    }
}
