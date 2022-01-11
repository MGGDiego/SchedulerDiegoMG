using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class Auxiliary
    {
        public Auxiliary() { }

        public bool ValidateDateTime(DateTime? TheDate)
        {
            bool result = true;
            if (TheDate.HasValue == false) { result = false; }
            else if (TheDate.Value > new DateTime(9998,12,31) || TheDate.Value < new DateTime(1753,01,01))
            {
                result = false;
            }
            return result;
        }

        public bool ValidateInteger(int? TheInteger)
        {
            bool result = true;
            if (TheInteger.HasValue == false) { result = false; }
            else if (TheInteger.Value < 1 || TheInteger.Value > 100)
            {
                result = false;
            }
            return result;
        }

        public bool ValidateString(String TheString)
        {
            bool result = true;
            if (TheString.Length > 40) { result = false; }
            return result;
        }

        public SchedulerWeek GetSchedulerWeek(DateTime TheDate)
        {
            return TheDate.DayOfWeek == 0 ? SchedulerWeek.Sunday : (SchedulerWeek)TheDate.DayOfWeek;
        }
        
        public DateTime ChangeDay(DateTime TheDate, int NewDay)
        {
            return new DateTime(TheDate.Year, TheDate.Month, NewDay, TheDate.Hour, TheDate.Minute, TheDate.Second);
        }

        public DateTime JoinDateWithTime(DateTime TheDate, TimeSpan TheTime)
        {
            return new DateTime(TheDate.Year, TheDate.Month, TheDate.Day, TheTime.Hours, TheTime.Minutes, TheTime.Seconds);
        }
    }
}
