using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompleteQuartzExample.Data.JobSchedules;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace CompleteQuartzExample.Data.QuartzService
{
    public class QuartzHostedService : IHostedService
  {

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartzHostedService(
    ISchedulerFactory schedulerFactory,
    IJobFactory jobFactory,
    IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }


        public IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {

            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

            public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
             
                .Build();
        }

        public string GetJobKey()
        { 
        var jobKey = Scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();

          return "s";
        }
        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            ICronTrigger trigger =
             (ICronTrigger)TriggerBuilder
                .Create()               
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression
        )
                .WithDescription(schedule.CronExpression)
         
                .Build();
            ((CronTriggerImpl)trigger).MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
           
            return trigger;
        }
    }
}
