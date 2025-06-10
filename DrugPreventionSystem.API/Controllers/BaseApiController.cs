using DrugPreventionSystem.BusinessLogic.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult<Result<T>> HandleResult<T>(Result<T> result)
        {
            return result.ResultStatus switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.NotFound => NotFound(result),
                ResultStatus.Duplicated => Conflict(result), // 409
                ResultStatus.Invalid => BadRequest(result),  // 400
                ResultStatus.Failed => BadRequest(result),   
                ResultStatus.NotVerified => Unauthorized(result), // 401
                ResultStatus.Error => StatusCode(500, result), // 500
                ResultStatus.Failure => StatusCode(500, result), 
                _ => StatusCode(500, Result<T>.Error("Unknown error.")), // Lỗi không xác định
            };
        }
    }
}
