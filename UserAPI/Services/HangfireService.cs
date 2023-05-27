using System.Linq.Expressions;
using Hangfire;
using UserAPI.API.Services.Interfaces;

namespace UserAPI.Services;

public class HangfireService : IBackgroundJobService
{
    public string FireAndForget(Expression<Func<Task>> methodCall)
    {
        return BackgroundJob.Enqueue(methodCall);
    }

    /// <summary>
    /// Use this method to call Delayed job by hangfire
    /// </summary>
    /// <param name="methodCall">Method which you want to execute</param>
    /// <param name="time">When execute</param>
    /// <returns></returns>
    public string ScheduleJob(Expression<Func<Task>> methodCall, TimeSpan time)
    {
        return BackgroundJob.Schedule(methodCall, time);
    }

    /// <summary>
    /// Use this method to set-up job which will be executed every day
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public void CronJob(Expression<Func<Task>> methodCall)
    {
        RecurringJob.AddOrUpdate(methodCall, Cron.Daily);
    }
    
    /// <summary>
    /// Use this method to set-up job which will be executed by cron expression
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="cronExpression"></param>
    public void CronJob(Expression<Func<Task>> methodCall, string cronExpression)
    {
        RecurringJob.AddOrUpdate(methodCall, cronExpression);
    }
}