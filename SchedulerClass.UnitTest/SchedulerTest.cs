using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class SchedulerTest
    {
        public DateTime[] CalculateDatesSerie(Scheduler scheduler, int series)
        {
            DateTime[] Result = new DateTime[series];
            int contador = 0;
            while (contador < series)
            {
                Result[contador] = new SchedulerGestor().CalculateDates(scheduler);
                scheduler.CurrentDate = Result[contador];
                contador++;
            }

            return Result;
        }

        #region CalculateDates_TypeOnce
        [TestMethod]
        public void CalculateDates_TypeOnceInputDate06Oct_Returns06Oct()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2021, 10, 06), result);
        }

        [TestMethod]
        public void CalculateDates_TypeOnceInputDate06OctNotRangeDates_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2022, 10, 06),
                Type = "Once",
                InputDate = new DateTime(2022, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("The dates are not in the range established in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void CalculateDates_TypeOnceInputDate06Oct_ReturnsDescriptionWithoutEndDate()
        {
            string DescriptionWithoutEndDate =
                "Occurs once. Scheluder will be used on 06/10/2021 0:00:00 starting on 01/01/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01)
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithoutEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void CalculateDates_TypeOnceInputDate06Oct_ReturnsDescriptionWithEndDate()
        {
            string DescriptionWithEndDate =
                "Occurs once. Scheluder will be used on 06/10/2021 0:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 04),
                Type = "Once",
                InputDate = new DateTime(2021, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_DailyConfiguration
        [TestMethod]
        public void Scheduler_DailyConfiguration_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                InputDate = new DateTime(2021, 08, 17),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 10, 09), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 12), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 15), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 18), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 21), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 24), result[5]);
            Assert.AreEqual(new DateTime(2021, 10, 27), result[6]);
            Assert.AreEqual("Occurs every date. Scheluder will be used on 27/10/2021 0:00:00 " +
                "starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_DailyConfigurationilyAndTimeConfiguration_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06, 23, 50, 0),
                InputDate = new DateTime(2021, 08, 17),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(23, 30, 0), new TimeSpan(23, 59, 59), true)
            {
                OccursTime = "Minute",
                OccursTimeValue = 20
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 10, 09, 23, 30, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 09, 23, 50, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 12, 23, 30, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 12, 23, 50, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 15, 23, 30, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 15, 23, 50, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 10, 18, 23, 30, 0), result[6]);
            Assert.AreEqual("Occurs every 20 Minute between 23:30:00 and 23:59:59. Scheluder will be used " +
                "on 18/10/2021 23:30:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06OctNotRangeDates_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2022, 10, 06),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().CalculateDates(scheduler));

            Assert.AreEqual("The dates are not in the range established in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06Oct_ReturnsDescriptionWithoutEndDate()
        {
            string DescriptionWithoutEndDate =
                "Occurs every date. Scheluder will be used on 09/10/2021 0:00:00 starting on 01/01/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01)
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithoutEndDate, scheduler.OutDescription);
        }

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06Oct_ReturnsDescriptionWithEndDate()
        {
            string DescriptionWithEndDate =
                "Occurs every date. Scheluder will be used on 09/10/2021 0:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00";
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }
        #endregion

        #region CalculateDates_TypeRecurring
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06OctOccursValue3IsYearly_Returns09Oct()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Yearly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2022, 12, 31)
            };

            var result = new SchedulerGestor().CalculateDates(scheduler);

            Assert.AreEqual(new DateTime(2022, 10, 06), result);
        }
        #endregion

        [TestMethod]
        public void ValidateDates_CurrentDateNotRangeDates_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2022, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().ValidateDates(
                scheduler.CurrentDate, scheduler.StartDate, scheduler.EndDate));

            Assert.AreEqual("The dates are not in the range established in the Configuration.", ex.Message);
        }

        #region ValidateData
        [TestMethod]
        public void ValidateData_TypeIsEmpty_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                Type = string.Empty
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().ValidateData(scheduler));

            Assert.AreEqual("You must select a Type in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsOnceAndInputDateIsNull_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                Type = "Once"
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().ValidateData(scheduler));

            Assert.AreEqual("You must input date to perform the calculation.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsOnceAndCurrentDateIsGreaterThanInputDate_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                Type = "Once",
                InputDate = new DateTime(2021, 01, 01),
                CurrentDate = new DateTime(2021, 02, 01)
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().ValidateData(scheduler));

            Assert.AreEqual("The Current Date can not be greater than the one entered in the input.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsRecurringAndOccursIsEmpty_ReturException()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring"
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().ValidateData(scheduler));

            Assert.AreEqual("You must enter a value in the occurs field.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsRecurringAndOccursIsWeeklyButWeekValue0_ReturException()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring",
                Occurs = "Weekly",
                WeekValue = Array.Empty<SchedulerWeek>()
            };

            var ex = Assert.ThrowsException<Exception>(() => new SchedulerGestor().ValidateData(scheduler));

            Assert.AreEqual("You must enter a value in the weeks field.", ex.Message);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputOnlyDay_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMonday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Monday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 18/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputTuesday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Tuesday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Tuesday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 19/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputWednesday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Wednesday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Wednesday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 20/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputThursday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Thursday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 21/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFriday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 08, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 08, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Friday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 22/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputSaturday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 09, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Saturday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 09, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Saturday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 23/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Sunday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 24/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputOnlyDay_Recurring2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMonday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Monday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 01/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputTuesday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Tuesday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Tuesday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 02/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputWednesday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Wednesday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Wednesday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 03/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputThursday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Thursday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 04/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFriday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 08, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 08, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 05, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 05, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Friday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 05/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputSaturday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 09, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Saturday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 09, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Saturday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 06/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputSunday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 8, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 8, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Sunday every 2 Hour between 04:00:00 and 08:00:00. Scheluder will be used " +
                "on 07/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputAllWeek_StartMonday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputAllWeek_StartMonday_Recurring1()
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
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 07/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputAllWeek_StartMonday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 07/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyConfiguration_ImputAllWeek_StartMonday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyConfiguration_ImputAllWeek_StartMonday_Recurring1()
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
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every date. Scheluder will be used on 12/01/2021 4:00:00 starting on 01/01/2021 0:00:00 " +
                "and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyConfiguration_ImputAllWeek_StartMonday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every date. Scheluder will be used on 19/01/2021 4:00:00 starting on 01/01/2021 0:00:00 " +
                "and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputAllWeek_StartSunday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputAllWeek_StartSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 13/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputAllWeek_StartSunday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 20/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyConfiguration_ImputAllWeek_StartSunday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyConfiguration_ImputAllWeek_StartSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every date. Scheluder will be used on 18/01/2021 4:00:00 starting on 01/01/2021 0:00:00 " +
                "and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyConfiguration_ImputAllWeek_StartSunday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday,
                    SchedulerWeek.Thursday, SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday " +
                "every date. Scheluder will be used on 01/02/2021 4:00:00 starting on 01/01/2021 0:00:00 " +
                "and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputMondayToThursday_StartMonday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayToThursday_StartMonday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday, SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 11/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayToThursday_StartMonday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday, SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday, Thursday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 18/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputMondayToThursday_StartThursday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayToThursday_StartThursday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday, SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday, Thursday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 14/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayToThursday_StartThursday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday, SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday, Thursday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 21/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSunday_StartFriday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSunday_StartFriday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 08, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 08, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 16/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSunday_StartFriday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 08, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 08, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 23/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSunday_StartSunday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSunday_StartSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 22/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSunday_StartSunday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday, SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 6, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 02, 05, 4, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Friday, Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 05/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputTuesdayToThursday_StartMonday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputTuesdayToThursday_StartMonday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Tuesday, SchedulerWeek.Wednesday, SchedulerWeek.Thursday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Tuesday, Wednesday, Thursday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 12/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputMondayToWednesday_StartThursday_Recurring1And2
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayToWednesday_StartThursday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 6, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Tuesday, Wednesday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 18/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayToWednesday_StartThursday_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 4, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Tuesday, SchedulerWeek.Wednesday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 18, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 18, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 6, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 2 weeks on Monday, Tuesday, Wednesday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 01/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputSaturdayToSunday_StartFriday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputSaturdayToSunday_StartFriday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 08, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Saturday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 6, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Saturday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 17/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSaturday_StartSunday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputFridayToSaturday_StartSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Friday, SchedulerWeek.Saturday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 8);

            Assert.AreEqual(8, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 15, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[6]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 6, 0, 0), result[7]);
            Assert.AreEqual("Occurs every 1 weeks on Friday, Saturday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 23/01/2021 6:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputMondayWednesdayFridaySunday_StartMonday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayWednesdayFridaySunday_StartMonday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Wednesday, SchedulerWeek.Friday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Wednesday, Friday, Sunday " +
                "every 2 Hour between 04:00:00 and 06:00:00. Scheluder will be used " +
                "on 11/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputMondayWednesdayFridaySunday_StartSunday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputMondayWednesdayFridaySunday_StarSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 30, 0),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Monday, SchedulerWeek.Wednesday, SchedulerWeek.Friday, SchedulerWeek.Sunday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(5, 0, 0), true)
            {
                OccursTime = "Minute",
                OccursTimeValue = 30
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 4, 30, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 4, 30, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 13, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Monday, Wednesday, Friday, Sunday " +
                "every 30 Minute between 04:00:00 and 05:00:00. Scheluder will be used " +
                "on 13/01/2021 5:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputTuesdayThursdaySaturday_StartMonday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputTuesdayThursdaySaturday_StartMonday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 4, 0, 3),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Tuesday, SchedulerWeek.Thursday, SchedulerWeek.Saturday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(4, 0, 2), true)
            {
                OccursTime = "Second",
                OccursTimeValue = 1
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 1), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 2), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 1), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 2), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Tuesday, Thursday, Saturday " +
                "every 1 Second between 04:00:00 and 04:00:02. Scheluder will be used " +
                "on 12/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_WeeklyAndTimeConfiguration_ImputTuesdayThursdaySaturday_StartSunday_Recurring1
        [TestMethod]
        public void Scheduler_WeeklyAndTimeConfiguration_ImputTuesdayThursdaySaturday_StartSunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 4, 0, 3),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new SchedulerWeek[] { SchedulerWeek.Tuesday, SchedulerWeek.Thursday, SchedulerWeek.Saturday },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(4, 0, 2), true)
            {
                OccursTime = "Second",
                OccursTimeValue = 1
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 1), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 2), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 4, 0, 1), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 14, 4, 0, 2), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs every 1 weeks on Tuesday, Thursday, Saturday " +
                "every 1 Second between 04:00:00 and 04:00:02. Scheluder will be used " +
                "on 16/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00",
                scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFirstMonday_StartBeforeAndAfterFirstMondayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstMonday_StartBeforeFirstMondayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 03, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 07, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 05, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Monday of every 1 months every date. Scheluder will be used on 05/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstMonday_StartAfterFirstMondayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 01, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 03, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 07, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 05, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 02, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Monday of every 1 months every date. Scheluder will be used on 02/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstMonday_StartBeforeFirstMondayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 05, 03, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 05, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 09, 06, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 11, 01, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First Monday of every 2 months every date. Scheluder will be used on 01/11/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFirstMonday_StartBeforeAndAfterFirstMondayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstMonday_StartBeforeFirstMondayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 02, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Monday of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 05/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstMonday_StartInRangeTimeFirstMondayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Monday of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 05/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstMonday_StartAfterFirstMondayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Monday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 05, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 05, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 05, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 04, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 04, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First Monday of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 04/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputSecondTuesday_StartBeforeAndAfterSecondTuesdayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondTuesday_StartBeforeSecondTuesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 12, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 11, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 08, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 13, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Tuesday of every 1 months every date. Scheluder will be used on 13/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondTuesday_StartAfterSecondTuesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 15, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 09, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 11, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 08, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 13, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 10, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Tuesday of every 1 months every date. Scheluder will be used on 10/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondTuesday_StartAfterSecondTuesdayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 13, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = this.CalculateDatesSerie(scheduler, 5);

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(new DateTime(2021, 03, 09, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 11, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 13, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 09, 14, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 11, 09, 5, 0, 0), result[4]);
            Assert.AreEqual("Occurs the Second Tuesday of every 2 months every date. Scheluder will be used on 09/11/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputSecondTuesday_StartBeforeSecondTuesdayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondTuesday_StartBeforeSecondTuesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 02, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Tuesday of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 13/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondTuesday_StartInRangeTimeSecondTuesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 12, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 12, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Tuesday of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 13/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondTuesday_StartBeforeSecondTuesdayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 11, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Tuesday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 12, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 12, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 13, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 13, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Second Tuesday of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 13/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputThirdWednesday_StartBeforeAndAfterThirdWednesdayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWednesday_StartBeforeThirdWednesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 20, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 17, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 21, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 19, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 16, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 21, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Wednesday of every 1 months every date. Scheluder will be used on 21/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWednesday_StartAfterThirdWednesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 22, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 17, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 21, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 19, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 16, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 21, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 18, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Wednesday of every 1 months every date. Scheluder will be used on 18/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWednesday_StartBeforeThirdWednesdayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 17, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 20, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 05, 19, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 21, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 09, 15, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 11, 17, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third Wednesday of every 2 months every date. Scheluder will be used on 17/11/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputThirdWednesday_StartBeforeAndAfterThirdWednesdayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWednesday_StartBeforeThirdWednesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 14, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 20, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 17, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 17, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 21, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Wednesday of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 21/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWednesday_StartInRangeTimeThirdWednesdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 20, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 20, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 17, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 17, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 21, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 21, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Wednesday of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 21/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWednesday_StartAfterThirdWednesdayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 24, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Wednesday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 21, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 21, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 21, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 21, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 20, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 20, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third Wednesday of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 20/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFourthThursday_StartBeforeAndAfterFourthThursdayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthThursday_StartBeforeFourthThursdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 25, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 28, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 25, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 27, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 24, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 22, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Thursday of every 1 months every date. Scheluder will be used on 22/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthThursday_StartAfterFourthThursdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 29, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 25, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 25, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 27, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 24, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 22, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 26, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Thursday of every 1 months every date. Scheluder will be used on 26/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthThursday_StartAfterFourthThursdayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 30, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = this.CalculateDatesSerie(scheduler, 5);

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(new DateTime(2021, 03, 25, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 27, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 22, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 09, 23, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 11, 25, 5, 0, 0), result[4]);
            Assert.AreEqual("Occurs the Fourth Thursday of every 2 months every date. Scheluder will be used on 25/11/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFourthThursday_StartBeforeFourthThursdayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthThursday_StartBeforeFourthThursdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 12, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 28, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 28, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 25, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 25, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Thursday of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 22/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthThursday_StartInRangeTimeFourthThursdayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 28, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 28, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 25, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 25, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Thursday of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 22/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthThursday_StartBeforeFourthThursdayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 16, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Thursday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 28, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 28, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 22, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 22, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 22, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Fourth Thursday of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 22/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputLastFriday_StartBeforeAndAfterLastFridayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastFriday_StartBeforeLastFridayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 29, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 28, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 25, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 30, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Friday of every 1 months every date. Scheluder will be used on 30/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastFriday_StartAfterLastFridayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 30, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 26, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 28, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 25, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 30, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 27, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Friday of every 1 months every date. Scheluder will be used on 27/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastFriday_StartBeforeLastFridayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 24, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 29, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 05, 28, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 30, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 09, 24, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 11, 26, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Last Friday of every 2 months every date. Scheluder will be used on 26/11/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputLastFriday_StartBeforeAndAfterLastFridayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastFriday_StartBeforeLastFridayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 29, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 29, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Friday of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 30/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastFriday_StartInRangeTimeLastFridayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 29, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 29, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 26, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Friday of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 30/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastFriday_StartAfterLastFridayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 31, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Friday
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 30, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 30, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 30, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 29, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 29, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Last Friday of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 29/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFirstDay_StartAfterFirstDayOfMonth_Recurring1
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstDay_StartAfterFirstDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 01, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 01, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 01, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 01, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 01, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 01, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Day of every 1 months every date. Scheluder will be used on 01/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion
        
        #region Scheduler_MonthlyAndTimeConfiguration_ImputFirstDay_StartBeforeAndAfterFirstDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstDay_StartBeforeFirstDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 1, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 01, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 01, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Day of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 01/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstDay_StartInRangeTimeFirstDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 01, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 01, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 01, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First Day of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 01/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstDay_StartAfterFirstDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 01, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 01, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 01, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 01, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First Day of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 01/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputSecondDay_StartBeforeAndAfterSecondDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondDay_StartBeforeSecondDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 02, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 02, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 02, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Day of every 1 months every date. Scheluder will be used on 02/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondDay_StartAfterSecondDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 02, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 02, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 02, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 02, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 02, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Day of every 1 months every date. Scheluder will be used on 02/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputSecondDay_StartBeforeAndAfterSecondDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondDay_StartBeforeSecondDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 1, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 02, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Day of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 02/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondDay_StartInRangeTimeSecondDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 02, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second Day of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 02/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondDay_StartAfterSecondDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 02, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 02, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 02, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Second Day of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 02/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputThirdDay_StartBeforeAndAfterThirdDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdDay_StartBeforeThirdDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 03, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 03, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 03, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Day of every 1 months every date. Scheluder will be used on 03/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdDay_StartAfterThirdDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 03, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 03, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 03, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 03, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Day of every 1 months every date. Scheluder will be used on 03/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputThirdDay_StartBeforeAndAfterThirdDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdDay_StartBeforeThirdDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 1, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 03, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Day of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 03/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdDay_StartInRangeTimeThirdDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 03, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 03, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third Day of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 03/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdDay_StartAfterThirdDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 03, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 03, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 03, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third Day of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 03/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFourthDay_StartBeforeAndAfterFourthDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthDay_StartBeforeFourthDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 04, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 04, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 04, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Day of every 1 months every date. Scheluder will be used on 04/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthDay_StartAfterFourthDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 04, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 04, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 04, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 04, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 04, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Day of every 1 months every date. Scheluder will be used on 04/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFourthDay_StartBeforeAndAfterFourthDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthDay_StartBeforeFourthDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 1, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Day of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 04/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthDay_StartInRangeTimeFourthDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 04, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth Day of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 04/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthDay_StartAfterFourthDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 04, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 04, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 04, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 04, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 04, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Fourth Day of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 04/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputLastDay_StartBeforeAndAfterLastDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastDay_StartBeforeLastDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 17, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 31, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 31, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 31, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 30, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 31, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Day of every 1 months every date. Scheluder will be used on 31/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputLastDay_StartBeforeAndAfterLastDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastDay_StartBeforeLastDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 26, 1, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 31, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 31, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 31, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Day of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 30/04/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastDay_StartInRangeTimeLastDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 31, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 31, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 31, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 31, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last Day of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 30/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastDay_StartAfterLastDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 31, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.Day
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 30, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 30, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 31, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 31, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 31, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 31, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Last Day of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 31/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFirstWeekendDay_StartBeforeAndAfterFirstWeekendDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekendDay_StartBeforeFirstWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 06, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 07, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekendDay of every 1 months every date. Scheluder will be used on 03/04/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekendDay_StartAfterFirstWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 05, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 06, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 06, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 07, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 05, 01, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekendDay of every 1 months every date. Scheluder will be used on 01/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekendDay_StartBeforeFirstWeekendDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 06, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 07, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 01, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 05, 02, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First WeekendDay of every 2 months every date. Scheluder will be used on 02/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekendDay_StartBeforeAndAfterFirstWeekendDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekendDay_StartBeforeFirstWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 02, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekendDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 07/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekendDay_StartInRangeTimeFirstWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 02, 11, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 03, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 06, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekendDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 06/03/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekendDay_StartAfterFirstWeekendDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 03, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First WeekendDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 03/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputSecondWeekendDay_StartBeforeAndAfterSecondWeekendDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondWeekendDay_StartBeforeSecondWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 09, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 13, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 14, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 13, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 14, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 10, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekendDay of every 1 months every date. Scheluder will be used on 10/04/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondWeekendDay_StartAfterSecondWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 26, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 13, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 14, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 13, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 14, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 04, 10, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 11, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 05, 08, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekendDay of every 1 months every date. Scheluder will be used on 08/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondWeekendDay_StartBeforeSecondWeekendDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 09, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 13, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 14, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 08, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 05, 09, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Second WeekendDay of every 2 months every date. Scheluder will be used on 09/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekendDay_StartBeforeAndAfterSecondWeekendDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekendDay_StartBeforeSecondWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 09, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 09, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 13, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 13, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 14, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekendDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 14/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekendDay_StartInRangeTimeSecondWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 10, 3, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 10, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 10, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 13, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 13, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 14, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 14, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 13, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekendDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 13/03/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekendDay_StartAfterSecondWeekendDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 10, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 10, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 11, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 11, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 10, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 10, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Second WeekendDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 10/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputThirdWeekendDay_StartBeforeAndAfterThirdWeekendDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWeekendDay_StartBeforeThirdWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 16, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 20, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 21, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 20, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 21, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 17, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekendDay of every 1 months every date. Scheluder will be used on 17/04/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWeekendDay_StartAfterThirdWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 26, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 20, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 21, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 20, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 21, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 04, 17, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 18, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 05, 15, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekendDay of every 1 months every date. Scheluder will be used on 15/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWeekendDay_StartBeforeThirdWeekendDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 16, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 20, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 21, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 15, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 05, 16, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third WeekendDay of every 2 months every date. Scheluder will be used on 16/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekendDay_StartBeforeAndAfterThirdWeekendDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekendDay_StartBeforeThirdWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 13, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 16, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 16, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 20, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 20, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 21, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekendDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 21/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekendDay_StartInRangeTimeThirdWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 16, 4, 0, 1),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 16, 6, 0, 1), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 20, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 20, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 21, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 21, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekendDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 21/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekendDay_StartAfterThirdWeekendDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 17, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 17, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 18, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 18, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 17, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 17, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third WeekendDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 17/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFourthWeekendDay_StartBeforeAndInRangeAndAfterFourthWeekendDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthWeekendDay_StartBeforeFourthWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 18, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 23, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 28, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 24, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekendDay of every 1 months every date. Scheluder will be used on 24/04/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthWeekendDay_StartInRangeFourthWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 24, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 28, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 24, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 25, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekendDay of every 1 months every date. Scheluder will be used on 25/04/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthWeekendDay_StartBeforeFourthWeekendDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 11, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 23, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 28, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 22, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 05, 23, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Fourth WeekendDay of every 2 months every date. Scheluder will be used on 23/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekendDay_StartBeforeAndAfterFourthWeekendDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekendDay_StartBeforeFourthWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 13, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 23, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 23, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 24, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekendDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 28/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekendDay_StartInRangeTimeFourthWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 24, 4, 0, 1),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 24, 6, 0, 1), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekendDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 27/03/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekendDay_StartAfterFourthWeekendDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 25, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 24, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 24, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 25, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 25, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 24, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 24, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Fourth WeekendDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 24/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputLastWeekendDay_StartBeforeAndInRangeAndAfterLastWeekendDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastWeekendDay_StartBeforeLastWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 18, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 30, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 28, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 24, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekendDay of every 1 months every date. Scheluder will be used on 24/04/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastWeekendDay_StartInRangeLastWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 30, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 31, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 28, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 24, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 04, 25, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekendDay of every 1 months every date. Scheluder will be used on 25/04/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastWeekendDay_StartBeforeLastWeekendDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 11, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 30, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 27, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 28, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 29, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 05, 30, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Last WeekendDay of every 2 months every date. Scheluder will be used on 30/05/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekendDay_StartBeforeAndAfterLastWeekendDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekendDay_StartBeforeLastWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 13, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 30, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 30, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekendDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 28/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekendDay_StartInRangeTimeLastWeekendDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 30, 4, 0, 1),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 30, 6, 0, 1), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 27, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekendDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 28/02/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekendDay_StartAfterLastWeekendDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 31, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekendDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 24, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 24, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 25, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 25, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 31, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 31, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 10, 30, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekendDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 30/10/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFirstWeekDay_StartAfterFirstWeekDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekDay_StartAfterFirstWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 07, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 01, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 05, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekDay of every 1 months every date. Scheluder will be used on 02/03/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        //[TestMethod]
        //public void Scheduler_MonthlyConfiguration_ImputFirstWeekDay_StartAfterFirstWeekDayOfMonth_Recurring3()
        //{
        //    var scheduler = new Scheduler
        //    {
        //        CurrentDate = new DateTime(2021, 01, 02, 5, 0, 0),
        //        Type = "Recurring",
        //        Occurs = "Monthly",
        //        OccursValue = 3,
        //        StartDate = new DateTime(2021, 01, 01),
        //        EndDate = new DateTime(2021, 12, 31)
        //    };
        //    scheduler.MonthlyConfiguration = new MonthlyConfiguration()
        //    {
        //        FrecuencyType = FrecuencyType.First,
        //        WeeklyValue = WeeklyValue.WeekDay
        //    };

        //    var result = this.CalculateDatesSerie(scheduler, 6);

        //    Assert.AreEqual(6, result.Length);
        //    Assert.AreEqual(new DateTime(2021, 01, 01, 5, 0, 0), result[0]);
        //    Assert.AreEqual(new DateTime(2021, 04, 01, 5, 0, 0), result[1]);
        //    Assert.AreEqual(new DateTime(2021, 04, 02, 5, 0, 0), result[2]);
        //    Assert.AreEqual(new DateTime(2021, 07, 01, 5, 0, 0), result[3]);
        //    Assert.AreEqual(new DateTime(2021, 07, 02, 5, 0, 0), result[4]);
        //    Assert.AreEqual(new DateTime(2021, 10, 01, 5, 0, 0), result[5]);
        //    Assert.AreEqual("Occurs the First WeekDay of every 3 months every date. Scheluder will be used on 01/10/2021 5:00:00" +
        //        " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        //}
        #endregion
        /*
        #region Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartBeforeAndAfterFirstWeekDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartBeforeFirstWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 02, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 02, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 07/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartInRangeTimeFirstWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 02, 11, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(7, 30, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 03, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 03, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 06, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 07, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 06, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 06/03/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartAfterFirstWeekDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(4, 0, 0), new TimeSpan(6, 0, 0), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 03, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 03, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 04, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 03, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First WeekDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 03/07/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion*/
    }
}