using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Commons
{
    public enum ResultStatus
    {
        Success,
        NotFound,
        Duplicated,
        Error,
        Invalid,
        Failed,
        NotVerified,
        Failure,
    }
}
