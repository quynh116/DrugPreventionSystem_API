using DrugPreventionSystem.BusinessLogic.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<string> GenerateCertificateWithTemplateAsync(CertificateData data);
    }
}
