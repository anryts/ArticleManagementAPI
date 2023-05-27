using System.Linq.Expressions;

namespace ArticleAPI.Services.Interfaces
{
    public interface IBackgroundJobService
    {
        string FireAndForget(Expression<Func<Task>> methodCall);
        
        /// <summary>
        /// Use this method to call Delayed job by hangfire
        /// </summary>
        /// <param name="methodCall">Method which you want to execute</param>
        /// <param name="time">When execute</param>
        /// <returns></returns>
        string ScheduleJob(Expression<Func<Task>> methodCall, TimeSpan time);

        /// <summary>
        /// Use this method to set-up ...
        /// </summary>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        void CronJob(Expression<Func<Task>> methodCall);

        /// <summary>
        /// Use this method to set-up job which will be executed by cron expression
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="cronExpression"></param>
        void CronJob(Expression<Func<Task>> methodCall, string cronExpression);
    }
}
