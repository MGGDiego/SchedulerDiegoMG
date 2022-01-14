using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class TimeConfigurationGestor
    {
        private Auxiliary AuxiliaryClass;
        private TextTranslateManager TranslateClass;
        public TimeConfigurationGestor()
        {
            this.AuxiliaryClass = new Auxiliary();
        }

        public DateTime CalculateHours(TimeConfiguration TimeConfigurationClass, DateTime TheDate, TextTranslateManager TranslateClass)
        {
            this.TranslateClass = TranslateClass;
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
            bool CurrentDateNotInRange = this.TimeNotInRange(TheDate.TimeOfDay, TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime);
            DateTime ResultDate = this.AddTime(TheDate, TimeConfigurationClass);
            bool ResultDateNotInRange = this.TimeNotInRange(ResultDate.TimeOfDay, TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime);
            if (CurrentDateNotInRange || ResultDateNotInRange)
            {
                ResultDate = this.AuxiliaryClass.JoinDateWithTime(TheDate, TimeConfigurationClass.StartTime);
            }
            return ResultDate;
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
            DateTime TheResultTime = this.AuxiliaryClass.JoinDateWithTime(TheDate, TimeConfigurationClass.OnceTime.Value);
            if (this.TimeNotInRange(TheResultTime.TimeOfDay, TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime))
            {
                throw new Exception(this.TranslateClass.GetText("TIME_NOT_RANGE"));
            }
            return TheResultTime;
        }

        public bool TimeNotInRange(TimeSpan TheTime, TimeSpan TheStartTime, TimeSpan TheEndTime)
        {
            return TheTime < TheStartTime || TheTime > TheEndTime;
        }
    }
}
