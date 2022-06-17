using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data
{
    public static class StaticList
    {
       public static List<string> _list = new List<string>();
        public static List<string> _multiJobTriggerList = new List<string>();
        public static void Record(string value)
        {
          
            _list.Add(value);
        }

        public static void multiJobTriggerInfo(string JobKey, int time)
        {

            _multiJobTriggerList.Add($"{DateTime.Now}, Job Name :{JobKey} Run, {time} run");
        }

    }
}
