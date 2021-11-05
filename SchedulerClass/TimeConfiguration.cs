using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class TimeConfiguration
    {
        public TimeConfiguration(DateTime StartTime, DateTime EndTime, bool OccursEvery)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.OccursEvery = OccursEvery;
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool OccursEvery { get; set; }
        public DateTime? OnceTime { get; set; }
        public string OccursTime { get; set; }
        public int OccursTimeValue { get; set; }

        public DateTime CalculateHours(DateTime TheDate)
        {
            if (this.OccursEvery)
            {
                return CalculateHoursOccursEvery(TheDate, this.OccursTime, this.OccursTimeValue);
            }
            else
            {
                return CalculateHoursOccursOnce(TheDate, this.OnceTime.Value);
            }
        }

        public DateTime CalculateHoursOccursEvery(DateTime TheDate, string OccursTime, int OccursTimeValue)
        {
            if (this.ValidateTime(TheDate))
            {
                TheDate = new DateTime(TheDate.Year, TheDate.Month, TheDate.Day,
                this.StartTime.Hour, this.StartTime.Minute, this.StartTime.Second);
            }
            else
            {
                switch (OccursTime)
                {
                    case "Second":
                        TheDate = TheDate.AddSeconds(OccursTimeValue);
                        break;
                    case "Minute":
                        TheDate = TheDate.AddMinutes(OccursTimeValue);
                        break;
                    case "Hour":
                        TheDate = TheDate.AddHours(OccursTimeValue);
                        break;
                }
                if (this.ValidateTime(TheDate))
                {
                    throw new Exception("The times are not in the range established in the Configuration.");
                }
            }
            return TheDate;
        }

        public DateTime CalculateHoursOccursOnce(DateTime TheDate, DateTime TheHour)
        {
            DateTime TheResultTime = new DateTime(TheDate.Year, TheDate.Month, TheDate.Day, TheHour.Hour, TheHour.Minute, TheHour.Second);
            if (this.ValidateTime(TheResultTime))
            {
                throw new Exception("The times are not in the range established in the Configuration.");
            }
            return TheResultTime;
        }

        private bool ValidateTime(DateTime TheTime)
        {
            DateTime TheStartTime = new DateTime(TheTime.Year, TheTime.Month, TheTime.Day,
                this.StartTime.Hour, this.StartTime.Minute, this.StartTime.Second);
            DateTime TheEndTime = new DateTime(TheTime.Year, TheTime.Month, TheTime.Day,
                this.EndTime.Hour, this.EndTime.Minute, this.EndTime.Second);
            if (TheTime < TheStartTime || TheTime > TheEndTime)
            {
                return true;
            }
            return false;
        }
    }
}
