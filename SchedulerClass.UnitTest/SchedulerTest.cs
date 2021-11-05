using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class SchedulerTest
    {
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

            var result = scheduler.CalculateDates();

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

            var ex = Assert.ThrowsException<Exception>(() => scheduler.CalculateDates());

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

            var result = scheduler.CalculateDates();

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

            scheduler.CalculateDates();

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }
        #endregion

        #region CalculateDates_TypeRecurring
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06OctOccursValue3IsDaily_Returns09Oct()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 10, 09), result);
        }

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06OctOccursValue3IsMonthly_Returns06Nov()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 11, 06), result);
        }

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

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2022, 10, 06), result);
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

            var ex = Assert.ThrowsException<Exception>(() => scheduler.CalculateDates());

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

            scheduler.CalculateDates();

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

            scheduler.CalculateDates();

            Assert.AreEqual(DescriptionWithEndDate, scheduler.OutDescription);
        }

        /*
         * Weekly execution mode, Wednesday and Friday are selected, it should leave Friday as the next day.
         */
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate28OctOccursWeeklyAndWeekValueWF_Returns29Oct()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 28),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new string[] { "Wednesday", "Friday" },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 10, 29), result);
        }

        /*
         * Weekly execution mode, Tuesday and Saturday are selected, it should leave Tuesday as the next day.
         */
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate30OctOccursWeeklyAndWeekValueTS_Returns02Nov()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 30),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 1,
                WeekValue = new string[] { "Tuesday", "Saturday" },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 11, 02), result);
        }

        /*
         * Weekly execution mode, Wednesday and Friday are selected, it should leave Friday as the next day.
         */
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate28OctOccurs2WeeksAndWeekValueWF_Returns29Oct()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 28),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new string[] { "Wednesday", "Friday" },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 10, 29), result);
        }

        /*
         * Weekly execution mode, Tuesday and Saturday are selected, it should leave Tuesday as the next day.
         */
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate30OctOccurs2WeeksAndWeekValueTS_Returns09Nov()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 30),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new string[] { "Tuesday", "Saturday" },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 11, 09), result);
        }

        /*
         * Weekly execution mode, Tuesday and Saturday are selected, it should leave Tuesday as the next day.
         */
        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate30OctOccurs2WeeksAndWeekValueTS_Returns13Nov()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 30),
                Type = "Recurring",
                Occurs = "Weekly",
                OccursValue = 2,
                WeekValue = new string[] { "Saturday" },
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 11, 13), result);
        }

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06Oct15HOccursValue3_Returns09Oct17H()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01, 0, 0, 0),
                EndDate = new DateTime(2021, 12, 31, 0, 0, 0),
                TimeConfiguration = new TimeConfiguration(
                    new DateTime(2020, 01, 01, 12, 0, 0), new DateTime(2020, 01, 01, 22, 0, 0), false)
            };
            scheduler.TimeConfiguration.OnceTime = new DateTime(2021, 10, 06, 17, 0, 0);

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 10, 09, 17, 0, 0), result);
        }

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06Oct15HOccursValue3IsEveryTime_Returns09Oct17H()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 06, 15, 0, 0),
                Type = "Recurring",
                Occurs = "Daily",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01, 0, 0, 0),
                EndDate = new DateTime(2021, 12, 31, 0, 0, 0),
                TimeConfiguration = new TimeConfiguration(
                    new DateTime(2020, 01, 01, 12, 0, 0), new DateTime(2020, 01, 01, 22, 0, 0), true)
            };
            scheduler.TimeConfiguration.OccursTime = "Hour";
            scheduler.TimeConfiguration.OccursTimeValue = 2;

            var result = scheduler.CalculateDates();

            Assert.AreEqual(new DateTime(2021, 10, 09, 17, 0, 0), result);
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

            var ex = Assert.ThrowsException<Exception>(() => scheduler.ValidateDates(scheduler.CurrentDate));

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

            var ex = Assert.ThrowsException<Exception>(() => scheduler.ValidateData());

            Assert.AreEqual("You must select a Type in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsOnceAndInputDateIsNull_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                Type = "Once"
            };

            var ex = Assert.ThrowsException<Exception>(() => scheduler.ValidateData());

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

            var ex = Assert.ThrowsException<Exception>(() => scheduler.ValidateData());

            Assert.AreEqual("The Current Date can not be greater than the one entered in the input.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsRecurringAndOccursIsEmpty_ReturException()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring"
            };

            var ex = Assert.ThrowsException<Exception>(() => scheduler.ValidateData());

            Assert.AreEqual("You must enter a value in the occurs field.", ex.Message);
        }

        [TestMethod]
        public void ValidateData_TypeIsRecurringAndOccursIsWeeklyButWeekValue0_ReturException()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring",
                Occurs = "Weekly",
                WeekValue = new string[0]
            };

            var ex = Assert.ThrowsException<Exception>(() => scheduler.ValidateData());

            Assert.AreEqual("You must enter a value in the weeks field.", ex.Message);
        }
        #endregion
    }
}
