using CompleteQuartzExample.Data.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.Services
{
    public class PoemService
    {
      
 
        internal readonly IScheduler scheduler;
        public PoemService(IScheduler quartz)
        {
            scheduler = quartz;

        }

   
        public List<string> GetPoemLineList()
        {

            return PoemLineList._poem;

        }
    }
}
