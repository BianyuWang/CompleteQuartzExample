using CompleteQuartzExample.Data.IJobs;
using CompleteQuartzExample.Data.Models;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data.Services
{
    public class AnimalService
    {

        public static readonly List<Animal> Animals = new List<Animal>()
     {
           new Animal(){ Name="Alligator",Sound ="Bark"},
            new Animal(){ Name="Ant",Sound ="Ant"},
          new Animal(){  Name="Bat",Sound ="poo"},
         new Animal(){  Name="Bear",Sound ="fink"},
         new Animal(){  Name="Bird",Sound ="twitter"},
         new Animal(){ Name="Cat",Sound ="meow"},
                new Animal(){  Name="Chicken",Sound ="chuck"},
                       new Animal(){  Name="Cat",Sound ="meow"},
                              new Animal(){ Name="Crane",Sound ="rar"},
                                     new Animal(){ Name="Deer",Sound ="bray"},
                                            new Animal(){  Name="Ghost",Sound ="Whispering...."},
                                             new Animal(){  Name="Monster",Sound ="Roaring...."},
                                              new Animal(){ Name="Bean Shooter",Sound ="Piu Piu...."}



        };

        static int _count = 0;
        internal readonly IScheduler scheduler;

        public AnimalService(IScheduler quartz)
        {
            scheduler = quartz;

        }
        public async Task<bool> AddJob(SingleJob singleJob)
        {
            
            _count = _count + 1;
            int runTime = 0;

            try
            {
                ITrigger trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(singleJob.interval).WithRepeatCount(singleJob.repeat))
                .StartNow().Build();

                IJobDetail job = JobBuilder.Create<QuartzMultiJob>().WithIdentity(_count.ToString(), "JobAdded")
                    .UsingJobData("time", runTime)
                    .UsingJobData("totalRepeat",singleJob.repeat)
                    .UsingJobData("animal",singleJob.animal.Name)
                       .UsingJobData("sound", singleJob.animal.Sound)
                        .UsingJobData("styleStr", singleJob.barStyle.ToString())
                    .Build();
              await  scheduler.ScheduleJob(job, trigger);
               await scheduler.Start();

            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        
        }


        //public List<string> GetTriggers()
        //{

        //    List<string> msg = new List<string>();


        //    //  var allJobKeys=   scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals("Group1") ).Result.ToList();
        //    var allJobKeys = scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();

        //    if (allJobKeys != null)
        //    {
        //        foreach (var v in allJobKeys)
        //        {
        //            if (v.Group == "JobAdded")
        //            {
        //                int time = scheduler.GetJobDetail(new JobKey(v.Name, "JobAdded")).Result.JobDataMap.GetInt("time");
        //                string m = $"job name :{ v.Name},run time: {time}";

        //                //   StaticList.multiJobTriggerInfo(v.Name, time);
        //                return StaticList._multiJobTriggerList;
        //            }

        //        }
        //    }

        //    return null;
        //    //    return StaticList._multiJobTriggerList;

        //}

    }
}
