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
    }
}
