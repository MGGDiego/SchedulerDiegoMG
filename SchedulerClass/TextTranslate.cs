using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class TextTranslate
    {
        public TextTranslate(string cultureCode, string cultureUK, string cultureUS, string cultureESP)
        {
            CultureCode = cultureCode;
            CultureUK = cultureUK;
            CultureUS = cultureUS;
            CultureESP = cultureESP;
        }

        public string CultureCode { get; set; }
        public string CultureUK { get; set; }
        public string CultureUS { get; set; }
        public string CultureESP { get; set; }
    }

    public enum TextCulture
    {
        UK,
        US,
        ESP
    }
}
