using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class SchedulerTest
    {
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

            var result = string.Empty;
            try { scheduler.CalculateDates(); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("The dates are not in the range established in the Configuration.", result);
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

        [TestMethod]
        public void CalculateDates_TypeRecurringInputDate06OctOccursValue3_Returns09Oct()
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

            var result = string.Empty;
            try { scheduler.CalculateDates(); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("The dates are not in the range established in the Configuration.", result);
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

        [TestMethod]
        public void ValidateDates_CurrentDateNotRangeDates_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2022, 10, 06),
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };

            var result = string.Empty;
            try { scheduler.ValidateDates(scheduler.CurrentDate); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("The dates are not in the range established in the Configuration.", result);
        }

        [TestMethod]
        public void ValidateData_TypeIsEmpty_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                Type = string.Empty
            };

            var result = string.Empty;
            try { scheduler.ValidateData(); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("You must select a Type in the Configuration.", result);
        }

        [TestMethod]
        public void ValidateData_TypeIsOnceAndInputDateIsNull_ReturnsException()
        {
            var scheduler = new Scheduler
            {
                Type = "Once"
            };

            var result = string.Empty;
            try { scheduler.ValidateData(); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("You must input date to perform the calculation.", result);
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

            var result = string.Empty;
            try { scheduler.ValidateData(); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("The Current Date can not be greater than the one entered in the input.", result);
        }

        [TestMethod]
        public void ValidateData_TypeIsRecurringAndOccursIsEmpty_ReturException()
        {
            var scheduler = new Scheduler
            {
                Type = "Recurring"
            };

            var result = string.Empty;
            try { scheduler.ValidateData(); }
            catch (Exception ex) { result = ex.Message; }

            Assert.AreEqual("You must enter a value in the occurs field.", result);
        }
    }
}
