using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class TextTranslateManager
    {
        private List<TextTranslate> _TheTranslatedTexts;
        private TextCulture CurrentCulture;
        private string[] TextCode = new string[] { "CULTURE", "SCHEDULER_WILL_USED", "END_DATE", "OCCURS_ONCE", "EVERY_WEEKS", "EVERY_MONTHS", "TYPE_IS_NOT_SELECTED",
                "INPUT_DATE_WITHOUT_VALUE", "CURRENT_DATE_GREATER_THAN_INPUT_DATE", "OCCURS_FIELD_WITHOUT_VALUE", "WEEKS_FIELD_WITHOUT_VALUE",
                "DATE_NOT_RANGE", "EVERY_DATE", "EVERY_TIME", "OCCURS", "TIME_NOT_RANGE", "First", "Second", "Third", "Fourth", "Last",
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Day", "WeekDay", "WeekendDay" };
        private string[] TextUK = new string[] { "en-GB", " Scheduler will be used on {0} starting on ", " and ending ", "Occurs once.", " every {0} weeks on {1}",
                " the {0} {1} of every {2} months", "You must select a Type in the Configuration.", "You must input date to perform the calculation.",
                "The Current Date can not be greater than the one entered in the input.", "You must enter a value in the Occurs field.",
                "You must enter a value in the Weeks field.", "The dates are not in the range established in the Configuration.", " every date.",
                " every {0} {1} between {2} and {3}.", "Occurs", "The times are not in the range established in the Configuration.",
                "First", "Second", "Third", "Fourth", "Last", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday",
                "Day", "WeekDay", "WeekendDay" };
        private string[] TextUS = new string[] { "en-US", " Scheduler will be used on {0} starting on ", " and ending ", "Occurs once.", " every {0} weeks on {1}",
                " the {0} {1} of every {2} months", "You must select a Type in the Configuration.", "You must input date to perform the calculation.",
                "The Current Date can not be greater than the one entered in the input.", "You must enter a value in the Occurs field.",
                "You must enter a value in the Weeks field.", "The dates are not in the range established in the Configuration.", " every date.",
                " every {0} {1} between {2} and {3}.", "Occurs", "The times are not in the range established in the Configuration.",
                "First", "Second", "Third", "Fourth", "Last", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday",
                "Day", "WeekDay", "WeekendDay" };
        private string[] TextESP = new string[] { "es-ES", " Scheduler se usará en {0} empezando en ", " y acabando ", "Ocurre una vez.", " cada {0} semanas en {1}",
                " el {0} {1} de cada {2} meses", "Debe seleccionar un Tipo en la Configuración.", "Debe introducir una fecha para realizar el calculo.",
                "La Fecha Actual no puede ser mayor a la ingresada en la entrada.", "Debe introducir un valor en el campo Cada.",
                "Debe introducir un valor en el campo Semanas.", "Las fechas no están en el rango establecido en la Configuración.", " cada día.",
                " cada {0} {1} entre {2} y {3}.", "Ocurre", "Las horas no están en el rango establecido en la Configuración.",
                "Primer", "Segundo", "Tercer", "Cuarto", "Último", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo",
                "Día", "Entre Semana", "Fin de Semana" };

        public TextTranslateManager(TextCulture CurrentCulture = TextCulture.UK)
        {
            this.CurrentCulture = CurrentCulture;
        }

        private List<TextTranslate> TheTranslatedTexts
        {
            get
            {
                if (this._TheTranslatedTexts == null)
                {
                    this._TheTranslatedTexts = this.ReloadTranslations();
                }
                return this._TheTranslatedTexts;
            }
        }

        public List<TextTranslate> ReloadTranslations()
        {
            List<TextTranslate> ListTranslate = new List<TextTranslate>();
            for (int i = 0; i < this.TextCode.Length; i++)
            {
                ListTranslate.Add(new TextTranslate(this.TextCode[i], this.TextUK[i], this.TextUS[i], this.TextESP[i]));
            }
            return ListTranslate;
        }

        public string GetText(string CultureCode)
        {
            foreach (TextTranslate item in this.TheTranslatedTexts)
            {
                if (item.CultureCode == CultureCode)
                {
                    return this.GetTextCulture(item);
                }
            }
            return string.Empty;
        }

        public string GetTextCulture(TextTranslate item)
        {
            switch (this.CurrentCulture)
            {
                case TextCulture.UK:
                    return item.CultureUK;
                case TextCulture.US:
                    return item.CultureUS;
                case TextCulture.ESP:
                    return item.CultureESP;
                default:
                    return string.Empty;
            }
        }
    }
}
