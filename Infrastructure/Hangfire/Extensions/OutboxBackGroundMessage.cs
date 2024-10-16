using Microsoft.AspNetCore.Builder;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Extensions
{
    public static class OutboxBackGroundMessage
    {
        public static IApplicationBuilder UseOutboxBackGroundMessage(this IApplicationBuilder app)
        {
            // Use ApplicationServices to access the service provider
            var recurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

            // Add or update the recurring job
            recurringJobManager.AddOrUpdate<IProcessOutboxMessagesJob>("out-box-processor",  job => job.ProcessAsync(), Cron.Hourly);

            return app;
        }
    }
}
