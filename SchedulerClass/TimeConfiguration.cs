using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class TimeConfiguration
    {
        public TimeConfiguration(TimeSpan StartTime, TimeSpan EndTime, bool OccursEvery)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.OccursEvery = OccursEvery;
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool OccursEvery { get; set; }
        public TimeSpan? OnceTime { get; set; }
        public string OccursTime { get; set; }
        public int OccursTimeValue { get; set; }
    }
}
