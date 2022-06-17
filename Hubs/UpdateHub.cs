using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Hubs
{
      public  class UpdateHub : Hub
    {
        public  void UpdateList()
        {

             Clients.All.SendAsync("updateList");
         }

        public void UpdateTriggerList()
        {

            Clients.All.SendAsync("updateTriggerList");
        }
    }
}
