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
        public TimeConfiguration TimeConfiguration { get; set; }
        public string[] WeekValue { get; set; }

        public DateTime CalculateDates()
        {
            this.ValidateData();
            this.ValidateDates(this.CurrentDate);
            DateTime TheResult;
            string TheDescription = string.Empty;

            string TextDescription = "Occurs {0}. Scheluder will be used on {1} starting on {2}";
            if (this.EndDate.HasValue)
            {
                TextDescription += string.Format(" and ending {0}", this.EndDate);
            }

            if (this.Type.Equals("Once"))
            {
                TheResult = this.InputDate.Value;
                TheDescription = "once";
            }
            else
            {
                DateTime OccursDate;
                switch (this.Occurs)
                {
                    case "Daily":
                        OccursDate = this.CurrentDate.AddDays(this.OccursValue);
                        break;
                    case "Weekly":
                        OccursDate = this.CalculateWeek(this.CurrentDate);
                        TheDescription = string.Format("every {0} weeks on {1} ", this.OccursValue, string.Join(", ", this.WeekValue));
                        break;
                    case "Monthly":
                        OccursDate = this.CurrentDate.AddMonths(this.OccursValue);
                        break;
                    case "Yearly":
                        OccursDate = this.CurrentDate.AddYears(this.OccursValue);
                        break;
                    default:
                        OccursDate = this.CurrentDate;
                        break;
                }
                string TheTimeDescription;
                TheResult = this.CalculateTime(OccursDate, out TheTimeDescription);
                TheDescription += TheTimeDescription;
            }

            this.ValidateDates(TheResult);
            this.OutDescription = string.Format(TextDescription, TheDescription, TheResult, this.StartDate);
            return TheResult;
        }

        public void ValidateData()
        {
            if (string.IsNullOrEmpty(this.Type))
            {
                throw new Exception("You must select a Type in the Configuration.");
            }
            if (this.Type.Equals("Once"))
            {
                if (this.InputDate.HasValue == false)
                {
                    throw new Exception("You must input date to perform the calculation.");
                }
                if (this.CurrentDate > this.InputDate.Value)
                {
                    throw new Exception("The Current Date can not be greater than the one entered in the input.");
                }
            }
            if (this.Type.Equals("Recurring"))
            {
                if (string.IsNullOrEmpty(this.Occurs))
                {
                    throw new Exception("You must enter a value in the occurs field.");
                }
                if (this.Occurs.Equals("Weekly") && this.WeekValue.Length == 0)
                {
                    throw new Exception("You must enter a value in the weeks field.");
                }
            }
        }

        public void ValidateDates(DateTime TheDate)
        {
            if (TheDate < this.StartDate || (this.EndDate.HasValue && TheDate > this.EndDate.Value))
            {
                throw new Exception("The dates are not in the range established in the Configuration.");
            }
        }

        public DateTime CalculateTime(DateTime TheDate, out string TheDescription)
        {
            TheDescription = "every date";
            if (this.TimeConfiguration != null)
            {
                if (this.TimeConfiguration.OccursEvery)
                {
                    TheDescription = string.Format(" every {0} {1} between {2} and {3}",
                        this.TimeConfiguration.OccursTimeValue, this.TimeConfiguration.OccursTime,
                        this.TimeConfiguration.StartTime.ToString("HH:mm:ss tt"), this.TimeConfiguration.EndTime.ToString("HH:mm:ss tt"));
                }
                return this.TimeConfiguration.CalculateHours(TheDate);
            }
            return TheDate;
        }

        public DateTime CalculateWeek(DateTime TheDate) 
        {
            string[] jj = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string dd = TheDate.ToString("dddd", new CultureInfo("en-EN"));
            int TheCurrentDay = 0;
            List<int> TheDaysOfWeek = new List<int>();

            int TheResultPos = 0;
            int TheResultNeg = 0;

            for (int i = 0; i < 7; i++)
            {
                if (jj[i] == dd)
                {
                    TheCurrentDay = i + 1;
                }
                foreach (string item in this.WeekValue)
                {
                    if (jj[i] == item)
                    {
                        TheDaysOfWeek.Add(i + 1);
                    }
                }
            }

            foreach (int CadaDia in TheDaysOfWeek)
            {
                if (TheCurrentDay < CadaDia && TheResultPos == 0)
                {
                    TheResultPos = CadaDia - TheCurrentDay;
                }
                if (TheCurrentDay > CadaDia && TheResultNeg == 0)
                {
                    TheResultNeg = CadaDia + (7 - TheCurrentDay);
                }
            }

            DateTime TheResult;
            if (TheResultPos != 0)
            {
                TheResult = TheDate.AddDays(TheResultPos);
            }
            else if (TheResultNeg != 0)
            {
                TheResult = TheDate.AddDays(TheResultNeg + (7 * (this.OccursValue - 1)));
            }
            else
            {
                TheResult = TheDate.AddDays(7 * (this.OccursValue));
            }
            return TheResult;
        }
    }
}
