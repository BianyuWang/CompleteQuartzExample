using CompleteQuartzExample.Hubs;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.IJobs
{
    [PersistJobDataAfterExecution]
    public class QuartzMultiJob : IJob
    {
        IHubContext<UpdateHub> _hubContext { get; set; }

        public QuartzMultiJob(IHubContext<UpdateHub> hubContext)
        {
            _hubContext = hubContext;

            //IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<UpdateHub>();
        }
        public  Task Execute(IJobExecutionContext context)
        {
            var jobKey = context.JobDetail.Key;
            var triggerKey = context.Trigger.Key;
          var jobdetail=  context.JobDetail.JobDataMap;
            int time = context.JobDetail.JobDataMap.GetInt("time");
           
         
          StaticList.multiJobTriggerInfo(jobKey.ToString(), time);

            context.JobDetail.JobDataMap["time"] = ++time;

            string msg = $"{DateTime.Now}, Job Name :{jobKey} Run, {time} run";
            _hubContext.Clients.All.SendAsync("updateMultiTrigger",msg);
           
            return Task.CompletedTask;
        }
    }
}
