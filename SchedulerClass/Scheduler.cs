using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class Scheduler
    {
        public Scheduler() { }

        public string Type { get; set; }
        public string Occurs { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime? InputDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int OccursValue { get; set; }
        public string OutDescription { get; set; }
        public SchedulerWeek[] WeekValue { get; set; }
        public TimeConfiguration TimeConfiguration { get; set; }
        public MonthlyConfiguration MonthlyConfiguration { get; set; }
        public TextCulture Culture { get; set; }
    }
}
