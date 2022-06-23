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
            int currentTime = context.JobDetail.JobDataMap.GetInt("time");
            int totalRepeat = context.JobDetail.JobDataMap.GetInt("totalRepeat");
            var animal = context.JobDetail.JobDataMap.GetString("animal");
            var sound = context.JobDetail.JobDataMap.GetString("sound");
            var style = context.JobDetail.JobDataMap.GetString("styleStr");
            StaticList.multiJobTriggerInfo(jobKey.ToString(), currentTime);

            context.JobDetail.JobDataMap["time"] = ++currentTime;

            string msg = $"{DateTime.Now}, Job Name :{jobKey} Run, current {currentTime} run, {animal},{sound},";
            _hubContext.Clients.All.SendAsync("updateMultiTrigger", msg, jobKey.ToString(), currentTime, totalRepeat,animal,sound, style); ;
           
            return Task.CompletedTask;
        }
    }
}
