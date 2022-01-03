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

    public enum FrecuencyType
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last
    }

    public enum WeeklyValue
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7,
        Day = 8,
        WeekDay = 9,
        WeekendDay = 10
    }
}
