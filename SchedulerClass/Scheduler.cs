using System;
using System.Collections.Generic;
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

        public DateTime CalculateDates()
        {
            this.ValidateData();
            this.ValidateDates(this.CurrentDate);

            string TextDescription = "Occurs {0}. Scheluder will be used on {1} starting on {2}";
            if (this.EndDate.HasValue)
            {
                TextDescription += string.Format(" and ending {0}", this.EndDate);
            }

            if (this.Type.Equals("Once"))
            {
                this.ValidateDates(this.InputDate.Value);
                this.OutDescription = string.Format(TextDescription, "once", this.InputDate, this.StartDate);
                return this.InputDate.Value;
            }
            else
            {
                DateTime OccursDate;
                switch (this.Occurs)
                {
                    case "Daily":
                        OccursDate = this.CurrentDate.AddDays(this.OccursValue);
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
                this.ValidateDates(OccursDate);
                this.OutDescription = string.Format(TextDescription, "every date", OccursDate, this.StartDate);
                return OccursDate;
            }
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
            if (this.Type.Equals("Recurring") && string.IsNullOrEmpty(this.Occurs))
            {
                throw new Exception("You must enter a value in the occurs field.");
            }
        }

        public void ValidateDates(DateTime TheDate)
        {
            if (TheDate < this.StartDate || (this.EndDate.HasValue && TheDate > this.EndDate.Value))
            {
                throw new Exception("The dates are not in the range established in the Configuration.");
            }
        }
    }
}
