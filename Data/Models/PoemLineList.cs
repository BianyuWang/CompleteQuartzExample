using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.Models
{
  public  static class PoemLineList
    {
        public static List<string> _poem = new List<string>();

        public static void RecordLine(string value)
        {

            _poem.Add(value);
        }

        public static void CleanLines( )
        {

            _poem.Clear();
        }
    }
}
