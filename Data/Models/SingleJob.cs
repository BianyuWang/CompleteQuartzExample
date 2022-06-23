using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.Models
{
    public class SingleJob
    {
        public Animal animal { get; set; }
        public int repeat { get; set; }
        public int interval { get; set; }
        public SingleJob()
        {
                animal = new Animal();
        }
    }
}
