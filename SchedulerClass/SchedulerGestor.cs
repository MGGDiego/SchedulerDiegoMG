﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class SchedulerGestor
    {
        private Auxiliary AuxiliaryClass;
        public SchedulerGestor()
        {
            this.AuxiliaryClass = new Auxiliary();
        }

        public DateTime CalculateDates(Scheduler SchedulerClass)
        {
            this.ValidateData(SchedulerClass);
            this.ValidateDates(SchedulerClass.CurrentDate, SchedulerClass.StartDate, SchedulerClass.EndDate);

            StringBuilder sb = new StringBuilder(" Scheluder will be used on {0} starting on ");
            sb.Append(SchedulerClass.StartDate);
            if (SchedulerClass.EndDate.HasValue)
            {
                sb.Append($" and ending {SchedulerClass.EndDate}");
            }

            DateTime TheResult;
            if (SchedulerClass.Type.Equals("Once"))
            {
                TheResult = SchedulerClass.InputDate.Value;
                sb.Insert(0, "Occurs once.");
                sb.Replace("{0}", TheResult.ToString());
            }
            else
            {
                TheResult = CalculateRecurringDates(SchedulerClass, out StringBuilder FinalDescription);
                sb.Insert(0, FinalDescription);
                sb.Replace("{0}", TheResult.ToString());
            }

            this.ValidateDates(TheResult, SchedulerClass.StartDate, SchedulerClass.EndDate);
            SchedulerClass.OutDescription = sb.ToString();
            return TheResult;
        }

        public DateTime CalculateRecurringDates(Scheduler SchedulerClass, out StringBuilder AdditionalDescription)
        {
            AdditionalDescription = new StringBuilder("Occurs");
            DateTime TheResult = this.CalculateTime(SchedulerClass.CurrentDate, SchedulerClass.TimeConfiguration, out string TheTimeDescription);
            bool JumpTime = SchedulerClass.CurrentDate < TheResult;

            switch (SchedulerClass.Occurs)
            {
                case "Daily":
                    TheResult = JumpTime ? TheResult : TheResult.AddDays(SchedulerClass.OccursValue);
                    break;
                case "Weekly":
                    TheResult = this.CalculateWeek(TheResult, SchedulerClass.WeekValue,
                        SchedulerClass.OccursValue, JumpTime, SchedulerClass.TimeConfiguration?.StartTime);
                    AdditionalDescription.AppendFormat(
                        " every {0} weeks on {1}", SchedulerClass.OccursValue, string.Join(", ", SchedulerClass.WeekValue));
                    break;
                case "Monthly":
                    TheResult = new MonthlyConfigurationGestor().CalculateMonthlyConfiguration(
                        TheResult, SchedulerClass.MonthlyConfiguration.FrecuencyType, SchedulerClass.MonthlyConfiguration.WeeklyValue,
                        SchedulerClass.OccursValue, JumpTime, JumpTime);
                    AdditionalDescription.AppendFormat(
                        " the {0} {1} of every {2} months", SchedulerClass.MonthlyConfiguration.FrecuencyType,
                        SchedulerClass.MonthlyConfiguration.WeeklyValue, SchedulerClass.OccursValue);
                    break;
                case "Yearly":
                    TheResult = TheResult.AddYears(SchedulerClass.OccursValue);
                    break;
            }
            AdditionalDescription.Append(TheTimeDescription);
            return TheResult;
        }

        public void ValidateData(Scheduler SchedulerClass)
        {
            if (string.IsNullOrEmpty(SchedulerClass.Type))
            {
                throw new Exception("You must select a Type in the Configuration.");
            }
            if (SchedulerClass.Type.Equals("Once"))
            {
                if (SchedulerClass.InputDate.HasValue == false)
                {
                    throw new Exception("You must input date to perform the calculation.");
                }
                if (SchedulerClass.CurrentDate > SchedulerClass.InputDate.Value)
                {
                    throw new Exception("The Current Date can not be greater than the one entered in the input.");
                }
            }
            if (SchedulerClass.Type.Equals("Recurring"))
            {
                if (string.IsNullOrEmpty(SchedulerClass.Occurs))
                {
                    throw new Exception("You must enter a value in the occurs field.");
                }
                if (SchedulerClass.Occurs.Equals("Weekly") && SchedulerClass.WeekValue.Length == 0)
                {
                    throw new Exception("You must enter a value in the weeks field.");
                }
            }
        }

        public void ValidateDates(DateTime TheDate, DateTime StartDate, DateTime? EndDate)
        {
            if (TheDate < StartDate || (EndDate.HasValue && TheDate > EndDate.Value))
            {
                throw new Exception("The dates are not in the range established in the Configuration.");
            }
        }

        public DateTime CalculateTime(DateTime TheDate, TimeConfiguration TimeConfigurationClass, out string TheDescription)
        {
            TheDescription = " every date.";
            if (TimeConfigurationClass != null)
            {
                if (TimeConfigurationClass.OccursEvery)
                {
                    TheDescription = string.Format(" every {0} {1} between {2} and {3}.",
                        TimeConfigurationClass.OccursTimeValue, TimeConfigurationClass.OccursTime,
                        TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime);
                }
                return new TimeConfigurationGestor().CalculateHours(TimeConfigurationClass, TheDate);
            }
            return TheDate;
        }

        public DateTime CalculateWeek(DateTime TheDate, SchedulerWeek[] WeekValue, int NumberOfWeeks, bool JumpTime, TimeSpan? startTime)
        {
            int TheCurrentDay = (int)this.AuxiliaryClass.GetSchedulerWeek(TheDate);
            int TheResultPos = 0;
            int TheResultNeg = 0;

            foreach (int CadaDia in WeekValue)
            {
                if (TheCurrentDay < CadaDia && TheResultPos == 0)
                {
                    TheResultPos = CadaDia - TheCurrentDay;
                }
                if (TheCurrentDay > CadaDia && TheResultNeg == 0)
                {
                    TheResultNeg = CadaDia + (7 - TheCurrentDay);
                }
                if (TheCurrentDay == CadaDia && JumpTime)
                {
                    TheResultPos = 0;
                    TheResultNeg = 0;
                    break;
                }
            }

            DateTime TheResult;
            if (TheResultPos != 0)
            {
                TheResult = TheDate.AddDays(TheResultPos);
                if (startTime.HasValue)
                {
                    TheResult = this.AuxiliaryClass.JoinDateWithTime(TheResult, startTime.Value);
                }
            }
            else if (TheResultNeg != 0)
            {
                TheResult = TheDate.AddDays(TheResultNeg + (7 * (NumberOfWeeks - 1)));
                if (startTime.HasValue)
                {
                    TheResult = this.AuxiliaryClass.JoinDateWithTime(TheResult, startTime.Value);
                }
            }
            else
            {
                TheResult = TheDate.AddDays(7 * (NumberOfWeeks));
                if (JumpTime)
                {
                    TheResult = TheDate;
                }
            }
            return TheResult;
        }
    }
}
