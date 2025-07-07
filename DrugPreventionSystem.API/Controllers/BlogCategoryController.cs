using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.BlogCategory;
using DrugPreventionSystem.BusinessLogic.Models.Responses.BlogCategory;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : BaseApiController
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [HttpPost]
        public async Task<ActionResult<Result<BlogCategoryResponse>>> AddAsync([FromBody] BlogCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<BlogCategoryResponse>.Invalid("Invalid category data", errors));
            }

            var result = await _blogCategoryService.CreateAsync(request);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<BlogCategoryResponse>>>> GetAllAsync()
        {
            var result = await _blogCategoryService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<BlogCategoryResponse>>> GetByIdAsync(Guid id)
        {
            var result = await _blogCategoryService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<BlogCategoryResponse>>> UpdateAsync(Guid id, [FromBody] BlogCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<BlogCategoryResponse>.Invalid("Invalid update data", errors));
            }

            var result = await _blogCategoryService.UpdateAsync(request, id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteAsync(Guid id)
        {
            var result = await _blogCategoryService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
