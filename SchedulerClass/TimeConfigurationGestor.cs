using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class TimeConfigurationGestor
    {
        public DateTime CalculateHours(TimeConfiguration TimeConfigurationClass, DateTime TheDate)
        {
            if (TimeConfigurationClass.OccursEvery)
            {
                return CalculateHoursOccursEvery(TheDate, TimeConfigurationClass);
            }
            else
            {
                return CalculateHoursOccursOnce(TheDate, TimeConfigurationClass);
            }
        }

        public DateTime CalculateHoursOccursEvery(DateTime TheDate, TimeConfiguration TimeConfigurationClass)
        {
            DateTime ResaultDate = this.AddTime(TheDate, TimeConfigurationClass);
            if (this.TimeNotInRange(ResaultDate.TimeOfDay, TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime))
            {
                ResaultDate = new DateTime(TheDate.Year, TheDate.Month, TheDate.Day,
                    TimeConfigurationClass.StartTime.Hours, TimeConfigurationClass.StartTime.Minutes, TimeConfigurationClass.StartTime.Seconds);
            }
            return ResaultDate;
        }

        public DateTime AddTime(DateTime TheDate, TimeConfiguration TimeConfigurationClass)
        {
            switch (TimeConfigurationClass.OccursTime)
            {
                case "Second":
                    TheDate = TheDate.AddSeconds(TimeConfigurationClass.OccursTimeValue);
                    break;
                case "Minute":
                    TheDate = TheDate.AddMinutes(TimeConfigurationClass.OccursTimeValue);
                    break;
                case "Hour":
                    TheDate = TheDate.AddHours(TimeConfigurationClass.OccursTimeValue);
                    break;
            }
            return TheDate;
        }

        public DateTime CalculateHoursOccursOnce(DateTime TheDate, TimeConfiguration TimeConfigurationClass)
        {
            DateTime TheResultTime = new DateTime(TheDate.Year, TheDate.Month, TheDate.Day,
                TimeConfigurationClass.OnceTime.Value.Hours, TimeConfigurationClass.OnceTime.Value.Minutes, TimeConfigurationClass.OnceTime.Value.Seconds);
            if (this.TimeNotInRange(TheResultTime.TimeOfDay, TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime))
            {
                throw new Exception("The times are not in the range established in the Configuration.");
            }
            return TheResultTime;
        }

        public bool TimeNotInRange(TimeSpan TheTime, TimeSpan TheStartTime, TimeSpan TheEndTime)
        {
            return TheTime < TheStartTime || TheTime > TheEndTime;
        }
    }
}
