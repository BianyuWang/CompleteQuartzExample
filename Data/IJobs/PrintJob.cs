using CompleteQuartzExample.Data.Models;
using CompleteQuartzExample.Hubs;

using Microsoft.AspNetCore.SignalR;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.IJobs
{
    [DisallowConcurrentExecution]  // prevents Quartz.NET from trying to run the same job concurrently.
    public class PrintJob : IJob
    {
        private readonly IHubContext<UpdateHub> _hubContext;
        static int count=0;
      //  private UpdateHub myhub { get; set; }

        public PrintJob(IHubContext<UpdateHub> hubContext)
        {
         
          _hubContext = hubContext;

            //IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<UpdateHub>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            if((count+8)%8==0) //will print first line
            {
                if (PoemLineList._poem.Count == 8)
                {
                    PoemLineList.CleanLines();
                }
               
            }

            PoemLineList.RecordLine(Poem.PoemLine[(count + 8) % 8]);
            count++;


            _hubContext.Clients.All.SendAsync("updatePoemList", "test");
       
            return Task.CompletedTask;
        }
    }
}
