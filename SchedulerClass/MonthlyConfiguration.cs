using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class MonthlyConfiguration
    {
        public MonthlyConfiguration()
        { }

        public FrecuencyType FrecuencyType { get; set; }
        public WeeklyValue WeeklyValue { get; set; }
    }
}
