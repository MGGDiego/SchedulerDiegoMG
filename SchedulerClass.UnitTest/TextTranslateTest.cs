using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class TextTranslateTest
    {
        #region TextTranslate_CultureUS
        [TestMethod]
        public void TextTranslate_DatesOutOffRange_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2022, 10, 06),
                Type = "Once",
                InputDate = new DateTime(2022, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("The dates are not in the range established in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_OccursOnceWithoutEnding_CultureUS()
        {
            string DescriptionWithoutEndDate =
                "Occurs once. Scheduler will be used on 10/6/2021 12:00:00 AM starting on 1/1/2021 12:00:00 AM";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                Culture = TextCulture.US
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithoutEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_OccursOnceWithEnding_CultureUS()
        {
            string DescriptionWithEndDate =
                "Occurs once. Scheduler will be used on 10/6/2021 12:00:00 AM starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_OccursEveryWithTimeConfiguration_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06, 23, 50, 0),
                InputDate = new DateTime(2021, 08, 17),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(23, 30, 0), new TimeSpan(23, 59, 59), true)
            {
                OccursTime = "Minute",
                OccursTimeValue = 20
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 10, 09, 23, 30, 0), result);
            Assert.AreEqual("Occurs every 20 Minute between 23:30:00 and 23:59:59. Scheduler will be used " +
                "on 10/9/2021 11:30:00 PM starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_OccursEveryWithoutTimeConfiguration_CultureUS()
        {
            string DescriptionWithEndDate =
                "Occurs every date. Scheduler will be used on 10/9/2021 12:00:00 AM starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_FieldTypeIsNull_CultureUS()
        {
            var scheduler = new Scheduler
            {
                Type = string.Empty,
                Culture = TextCulture.US
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("You must select a Type in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_InputDateIsNull_CultureUS()
        {
            var scheduler = new Scheduler
            {
                Type = "Once",
                Culture = TextCulture.US
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("You must input date to perform the calculation.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_InTypeOnceCurrentDateIsGreaterInputDate_CultureUS()
        {
            var scheduler = new Scheduler
            {
                Type = "Once",
                InputDate = new DateTime(2021, 01, 01),
                CurrentDate = new DateTime(2021, 02, 01),
                Culture = TextCulture.US
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("The Current Date can not be greater than the one entered in the input.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_FieldOccursIsNull_CultureUS()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring",
                Culture = TextCulture.US
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("You must enter a value in the Occurs field.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_FieldWeeksIsNull_CultureUS()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring",
                Occurs = "Weekly",
                WeekValue = Array.Empty<SchedulerWeek>(),
                Culture = TextCulture.US
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("You must enter a value in the Weeks field.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_OccursEveryWeekWithTimeConfiguration_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every date. Scheduler will be used on 1/5/2021 4:00:00 AM starting on 1/1/2021 12:00:00 AM " +
                "and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_FirstMonday_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 01, 04, 5, 0, 0), result);
            Assert.AreEqual("Occurs the First Monday of every 1 months every date. Scheduler will be used on 1/4/2021 5:00:00 AM" +
                " starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_SecondTuesday_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 01, 12, 5, 0, 0), result);
            Assert.AreEqual("Occurs the Second Tuesday of every 1 months every date. Scheduler will be used on 1/12/2021 5:00:00 AM" +
                " starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_ThirdWednesday_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 01, 20, 5, 0, 0), result);
            Assert.AreEqual("Occurs the Third Wednesday of every 1 months every date. Scheduler will be used on 1/20/2021 5:00:00 AM" +
                " starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_FourthThursday_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 25, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 01, 28, 5, 0, 0), result);
            Assert.AreEqual("Occurs the Fourth Thursday of every 1 months every date. Scheduler will be used on 1/28/2021 5:00:00 AM" +
                " starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_LastFriday_CultureUS()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.US
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 01, 29, 5, 0, 0), result);
            Assert.AreEqual("Occurs the Last Friday of every 1 months every date. Scheduler will be used on 1/29/2021 5:00:00 AM" +
                " starting on 1/1/2021 12:00:00 AM and ending 12/31/2021 12:00:00 AM", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_TimesOutOffRange_CultureUS()
        {
            var timeConfiguration = new TimeConfiguration(
                new TimeSpan(12, 00, 00), new TimeSpan(20, 00, 00), false)
            {
                OnceTime = new TimeSpan(21, 23, 00)
            };

            var ex = Assert.ThrowsException<Exception>(() => new TimeConfigurationGestor().CalculateHours(
                timeConfiguration, new DateTime(2021, 10, 28, 17, 00, 00), new TextTranslateManager(TextCulture.US)));

            Assert.AreEqual("The times are not in the range established in the Configuration.", ex.Message);
        }
        #endregion

        #region TextTranslate_CultureESP
        [TestMethod]
        public void TextTranslate_DatesOutOffRange_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2022, 10, 06),
                Type = "Once",
                InputDate = new DateTime(2022, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("Las fechas no están en el rango establecido en la Configuración.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_OccursOnceWithoutEnding_CultureESP()
        {
            string DescriptionWithoutEndDate =
                "Ocurre una vez. Scheduler se usará en 06/10/2021 0:00:00 empezando en 01/01/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                Culture = TextCulture.ESP
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithoutEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_OccursOnceWithEnding_CultureESP()
        {
            string DescriptionWithEndDate =
                "Ocurre una vez. Scheduler se usará en 06/10/2021 0:00:00 empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_OccursEveryWithTimeConfiguration_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06, 23, 50, 0),
                InputDate = new DateTime(2021, 08, 17),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(23, 30, 0), new TimeSpan(23, 59, 59), true)
            {
                OccursTime = "Minute",
                OccursTimeValue = 20
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre cada 20 Minute entre 23:30:00 y 23:59:59. Scheduler se usará " +
                "en 09/10/2021 23:30:00 empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_OccursEveryWithoutTimeConfiguration_CultureESP()
        {
            string DescriptionWithEndDate =
                "Ocurre cada día. Scheduler se usará en 09/10/2021 0:00:00 empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_FieldTypeIsNull_CultureESP()
        {
            var scheduler = new Scheduler
            {
                Type = string.Empty,
                Culture = TextCulture.ESP
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("Debe seleccionar un Tipo en la Configuración.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_InputDateIsNull_CultureESP()
        {
            var scheduler = new Scheduler
            {
                Type = "Once",
                Culture = TextCulture.ESP
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("Debe introducir una fecha para realizar el calculo.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_InTypeOnceCurrentDateIsGreaterInputDate_CultureESP()
        {
            var scheduler = new Scheduler
            {
                Type = "Once",
                InputDate = new DateTime(2021, 01, 01),
                CurrentDate = new DateTime(2021, 02, 01),
                Culture = TextCulture.ESP
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("La Fecha Actual no puede ser mayor a la ingresada en la entrada.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_FieldOccursIsNull_CultureESP()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring",
                Culture = TextCulture.ESP
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("Debe introducir un valor en el campo Cada.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_FieldWeeksIsNull_CultureESP()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring",
                Occurs = "Weekly",
                WeekValue = Array.Empty<SchedulerWeek>(),
                Culture = TextCulture.ESP
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("Debe introducir un valor en el campo Semanas.", ex.Message);
        }

        [TestMethod]
        public void TextTranslate_OccursEveryWeekWithTimeConfiguration_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre cada 1 semanas en Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo " +
                "cada día. Scheduler se usará en 05/01/2021 4:00:00 empezando en 01/01/2021 0:00:00 " +
                "y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_FirstMonday_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre el Primer Lunes de cada 1 meses cada día. Scheduler se usará en 04/01/2021 5:00:00" +
                " empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_SecondTuesday_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre el Segundo Martes de cada 1 meses cada día. Scheduler se usará en 12/01/2021 5:00:00" +
                " empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_ThirdWednesday_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre el Tercer Miercoles de cada 1 meses cada día. Scheduler se usará en 20/01/2021 5:00:00" +
                " empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_FourthThursday_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 25, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre el Cuarto Jueves de cada 1 meses cada día. Scheduler se usará en 28/01/2021 5:00:00" +
                " empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_LastFriday_CultureESP()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31),
                Culture = TextCulture.ESP
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual("Ocurre el Último Viernes de cada 1 meses cada día. Scheduler se usará en 29/01/2021 5:00:00" +
                " empezando en 01/01/2021 0:00:00 y acabando 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void TextTranslate_TimesOutOffRange_CultureESP()
        {
            var timeConfiguration = new TimeConfiguration(
                new TimeSpan(12, 00, 00), new TimeSpan(20, 00, 00), false)
            {
                OnceTime = new TimeSpan(21, 23, 00)
            };

            var ex = Assert.ThrowsException<Exception>(() => new TimeConfigurationGestor().CalculateHours(
                timeConfiguration, new DateTime(2021, 10, 28, 17, 00, 00), new TextTranslateManager(TextCulture.ESP)));

            Assert.AreEqual("Las horas no están en el rango establecido en la Configuración.", ex.Message);
        }
        #endregion
    }
}