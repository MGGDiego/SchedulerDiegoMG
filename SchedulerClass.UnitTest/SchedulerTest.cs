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
    }
}