using CompleteQuartzExample.Hubs;

using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.JobFactory
{
    public class QuartzMultiFactory : IJobFactory
    {
      
       
        //  private UpdateHub myhub { get; set; }

 

        private readonly IServiceProvider _serviceProvider;
        public QuartzMultiFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
         
        }

    

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            var job = (IJob)_serviceProvider.GetService(jobDetail.JobType);

            return job;
        }

        public void ReturnJob(IJob job)
        {
          
        }
    }
}
