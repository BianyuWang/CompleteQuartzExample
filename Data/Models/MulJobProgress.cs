using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.Models
{
    public class MulJobProgress
    {
        public int current { get; set; }
        public int total { get; set; }

        public string key { get; set; }
        public Animal animal { get; set; }

        public string color { get; set; }
        public ProgressBarStyle style { get; set; }

        public MulJobProgress()
        {
            animal = new Animal();
            style = ProgressBarStyle.Info;
        }
    }
}
