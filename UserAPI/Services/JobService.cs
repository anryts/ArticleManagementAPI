using Common.Enums;
using Microsoft.EntityFrameworkCore;
using UserAPI.API.Data;
using UserAPI.API.Services.Interfaces;
using UserAPI.Data;
using UserAPI.Data.Entities;
using UserAPI.Models.RequestModels;

namespace UserAPI.Services;

public class JobService : IJobService
{
    private readonly IBackgroundJobService _jobService;
    private readonly AppDbContext _appDbContext;
    private readonly ICurrentUserService _currentUserService;

    public JobService(IBackgroundJobService jobService, AppDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _jobService = jobService;
        _appDbContext = appDbContext;
        _currentUserService = currentUserService;
    }

    public async Task CreateJob(JobCreateModel model)
    {
        var entity = await _appDbContext.Jobs.AddAsync(new Job { Name = model.JobName });
        await _appDbContext.SaveChangesAsync();
        var userId = _currentUserService.GetCurrentUserId();
        _jobService.FireAndForget(() => SetUserJob(model.JobGender, entity.Entity.Id, userId));
    }

    private async Task SetUserJob(Gender gender, Guid jobId, Guid userId)
    {
        var query = _appDbContext.Users.AsQueryable();
        if (gender != Gender.Unknown)
            query = query.Where(x => x.Gender == gender);

        List<Guid> users = await query
            .Select(x => x.Id)
            .Where(x => x != userId)
            .ToListAsync();

        foreach (var user in users)
        {
            await _appDbContext.AddAsync<UserJob>(new UserJob { UserId = user, JobId = jobId });
        }
        await _appDbContext.SaveChangesAsync();
    }
}