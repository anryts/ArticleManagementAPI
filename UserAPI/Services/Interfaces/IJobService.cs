using UserAPI.Models.RequestModels;

namespace UserAPI.API.Services.Interfaces
{
    public interface IJobService
    {
        public Task CreateJob(JobCreateModel model);
    }
}
