using CompleteQuartzExample.Data.JobFactory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.QuartzService
{
    public static class QuartzMultiService
    {
        public static void StartJob<Tjob>() where Tjob : IJob
        {
            var scheduler = new StdSchedulerFactory().GetScheduler().Result;
            var Job = JobBuilder.Create<Tjob>().WithIdentity("jobs").Build();

            var triggle1 = TriggerBuilder.Create().WithIdentity("job.trigger1").StartNow().WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(5)).RepeatForever()).ForJob(Job).Build();
            var triggle2 = TriggerBuilder.Create().WithIdentity("job.trigger2").StartNow().WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(11)).RepeatForever()).ForJob(Job).Build();

            var dictionary = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>
{

    {Job,new HashSet<ITrigger>{

    triggle1,triggle2} }
};
            scheduler.Start();

            
        }


        public static void AddQuartz(this IServiceCollection serviceCollection, params Type[] jobs)
        {

            _ = serviceCollection.AddSingleton<IJobFactory, QuartzMultiFactory>();
            serviceCollection.Add(jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)));

            serviceCollection.AddSingleton(provider =>
            {

                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            }
            );     
        }







    }
}
