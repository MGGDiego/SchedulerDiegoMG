using System;
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
        private TextTranslateManager TranslateClass;
        public SchedulerGestor()
        {
            this.AuxiliaryClass = new Auxiliary();
        }

        public DateTime CalculateDates(Scheduler SchedulerClass)
        {
            this.TranslateClass = new TextTranslateManager(SchedulerClass.Culture);
            CultureInfo.CurrentCulture = new CultureInfo(this.TranslateClass.GetText("CULTURE"));
            this.ValidateData(SchedulerClass);

            StringBuilder sb = new StringBuilder(this.TranslateClass.GetText("SCHEDULER_WILL_USED"));
            sb.Append(SchedulerClass.StartDate.ToString("G"));
            if (SchedulerClass.EndDate.HasValue)
            {
                sb.Append(this.TranslateClass.GetText("END_DATE") + SchedulerClass.EndDate?.ToString("G"));
            }

            DateTime TheResult;
            if (SchedulerClass.Type.Equals("Once"))
            {
                TheResult = SchedulerClass.InputDate.Value;
                sb.Insert(0, this.TranslateClass.GetText("OCCURS_ONCE"));
                sb.Replace("{0}", TheResult.ToString("G"));
            }
            else
            {
                TheResult = CalculateRecurringDates(SchedulerClass, out StringBuilder FinalDescription);
                sb.Insert(0, FinalDescription);
                sb.Replace("{0}", TheResult.ToString("G"));
            }

            this.ValidateDates(TheResult, SchedulerClass.StartDate, SchedulerClass.EndDate);
            SchedulerClass.OutDescription = sb.ToString();
            return TheResult;
        }

        public DateTime CalculateRecurringDates(Scheduler SchedulerClass, out StringBuilder AdditionalDescription)
        {
            AdditionalDescription = new StringBuilder(this.TranslateClass.GetText("OCCURS"));
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
                    List<string> WeekValues = new List<string>();
                    foreach (var item in SchedulerClass.WeekValue)
                    {
                        WeekValues.Add(this.TranslateClass.GetText(item.ToString()));
                    }
                    AdditionalDescription.AppendFormat(
                        this.TranslateClass.GetText("EVERY_WEEKS"), SchedulerClass.OccursValue, string.Join(", ", WeekValues));
                    break;
                case "Monthly":
                    TheResult = new MonthlyConfigurationGestor().CalculateMonthlyConfiguration(
                        TheResult, SchedulerClass.MonthlyConfiguration.FrecuencyType, SchedulerClass.MonthlyConfiguration.WeeklyValue,
                        SchedulerClass.OccursValue, JumpTime, JumpTime);
                    AdditionalDescription.AppendFormat(
                        this.TranslateClass.GetText("EVERY_MONTHS"), 
                        this.TranslateClass.GetText(SchedulerClass.MonthlyConfiguration.FrecuencyType.ToString()),
                        this.TranslateClass.GetText(SchedulerClass.MonthlyConfiguration.WeeklyValue.ToString()), SchedulerClass.OccursValue);
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
                throw new Exception(this.TranslateClass.GetText("TYPE_IS_NOT_SELECTED"));
            }
            if (SchedulerClass.Type.Equals("Once"))
            {
                if (SchedulerClass.InputDate.HasValue == false)
                {
                    throw new Exception(this.TranslateClass.GetText("INPUT_DATE_WITHOUT_VALUE"));
                }
                if (SchedulerClass.CurrentDate > SchedulerClass.InputDate.Value)
                {
                    throw new Exception(this.TranslateClass.GetText("CURRENT_DATE_GREATER_THAN_INPUT_DATE"));
                }
            }
            if (SchedulerClass.Type.Equals("Recurring"))
            {
                if (string.IsNullOrEmpty(SchedulerClass.Occurs))
                {
                    throw new Exception(this.TranslateClass.GetText("OCCURS_FIELD_WITHOUT_VALUE"));
                }
                if (SchedulerClass.Occurs.Equals("Weekly") && SchedulerClass.WeekValue.Length == 0)
                {
                    throw new Exception(this.TranslateClass.GetText("WEEKS_FIELD_WITHOUT_VALUE"));
                }
            }
        }

        public void ValidateDates(DateTime TheDate, DateTime StartDate, DateTime? EndDate)
        {
            if (TheDate < StartDate || (EndDate.HasValue && TheDate > EndDate.Value))
            {
                throw new Exception(this.TranslateClass.GetText("DATE_NOT_RANGE"));
            }
        }

        public DateTime CalculateTime(DateTime TheDate, TimeConfiguration TimeConfigurationClass, out string TheDescription)
        {
            TheDescription = this.TranslateClass.GetText("EVERY_DATE");
            if (TimeConfigurationClass != null)
            {
                if (TimeConfigurationClass.OccursEvery)
                {
                    TheDescription = string.Format(this.TranslateClass.GetText("EVERY_TIME"),
                        TimeConfigurationClass.OccursTimeValue, TimeConfigurationClass.OccursTime,
                        TimeConfigurationClass.StartTime, TimeConfigurationClass.EndTime);
                }
                return new TimeConfigurationGestor().CalculateHours(TimeConfigurationClass, TheDate, this.TranslateClass);
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
