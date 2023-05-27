using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UserAPI.API.Services.Interfaces;
using UserAPI.CustomFilters;
using UserAPI.Models.RequestModels;

namespace UserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Auth]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] JobCreateModel model,
        [FromServices] IValidator<JobCreateModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        await _jobService.CreateJob(model);
        return Ok();
    }
}