using Hangfire;
using Microsoft.AspNetCore.Builder;
using System;

namespace FastCore.Hangfire
{
    public static class ApplicationBuilderExtension
    {
        public static void UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            ConfigHangfireTask();
        }

        //此处定义服务
        public static void ConfigHangfireTask()
        {
            var jobId = BackgroundJob.Schedule(
                () => Console.WriteLine("Delayed!"),
                TimeSpan.FromSeconds(30));

            RecurringJob.AddOrUpdate(
                "myrecurringjob",
                () => Console.WriteLine("Recurring!"),
                Cron.Minutely);
        }

    }
}
