using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class MonthlyConfigurationTest
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

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekendDay_WhenTheFirstWeekendDayOfMonthIsOnlySunday_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 07, 10, 5, 0, 0),
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

            var result = this.CalculateDatesSerie(scheduler, 2);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(new DateTime(2021, 08, 01, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 09, 04, 5, 0, 0), result[1]);
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

        #region Scheduler_MonthlyConfiguration_ImputFirstWeekDay_StartInRangeAndAfterFirstWeekDayOfMonth_Recurring1And3
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

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekDay_StartInRangeFirstWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 08, 05, 5, 0, 0),
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

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 08, 06, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 09, 01, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 09, 02, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 09, 03, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 11, 01, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First WeekDay of every 1 months every date. Scheluder will be used on 01/11/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFirstWeekDay_StartAfterFirstWeekDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 27, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 3,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.First,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 5);

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 01, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 02, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 01, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 02, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 5, 0, 0), result[4]);
            Assert.AreEqual("Occurs the First WeekDay of every 3 months every date. Scheluder will be used on 01/10/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartBeforeAndInRangeAndAfterFirstWeekDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartBeforeFirstWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 10, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.TimeConfiguration = new TimeConfiguration(new TimeSpan(15, 0, 0), new TimeSpan(17, 30, 0), true)
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
            Assert.AreEqual(new DateTime(2021, 01, 01, 15, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 17, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 15, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 17, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 15, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 17, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 15, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekDay of every 1 months every 2 Hour between 15:00:00 and 17:30:00. " +
                "Scheluder will be used on 03/02/2021 15:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartInRangeTimeFirstWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 02, 03, 5, 0, 0),
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
            Assert.AreEqual(new DateTime(2021, 02, 03, 7, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 05, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 05, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the First WeekDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 01/03/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFirstWeekDay_StartAfterFirstWeekDayOfMonth_Recurring5()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 23, 12, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 5,
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
            Assert.AreEqual(new DateTime(2021, 06, 01, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 06, 01, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 06, 02, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 06, 02, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 03, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 03, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the First WeekDay of every 5 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 03/06/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputSecondWeekDay_StartBeforeAndAfterSecondWeekDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondWeekDay_StartBeforeSecondWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 01, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 08, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekDay of every 1 months every date. Scheluder will be used on 09/02/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondWeekDay_StartAfterSecondWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 19, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Second,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 08, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 09, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 10, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 11, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 12, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 08, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 09, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekDay of every 1 months every date. Scheluder will be used on 09/03/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputSecondWeekDay_StartBeforeSecondWeekDayOfMonth_Recurring2()
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 08, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Second WeekDay of every 2 months every date. Scheluder will be used on 08/03/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekDay_StartBeforeAndAfterSecondWeekDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekDay_StartBeforeSecondWeekDayOfMonth_Recurring1()
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 04, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 05, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 07/01/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekDay_StartInRangeTimeSecondWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 06, 3, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 06, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 06, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 07, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 01, 08, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 08, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Second WeekDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 08/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputSecondWeekDay_StartAfterSecondWeekDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 01, 11, 12, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 05, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 06, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 06, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 04, 07, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 07, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Second WeekDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 07/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputThirdWeekDay_StartBeforeAndAfterThirdWeekDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWeekDay_StartBeforeThirdWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 05, 04, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 05, 17, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 18, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 05, 19, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 20, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 21, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 14, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 06, 15, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekDay of every 1 months every date. Scheluder will be used on 15/06/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWeekDay_StartAfterThirdWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 05, 26, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 06, 14, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 06, 15, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 06, 16, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 06, 17, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 18, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 12, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 13, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekDay of every 1 months every date. Scheluder will be used on 13/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputThirdWeekDay_StartBeforeThirdWeekDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 04, 01, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Third,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 12, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 14, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 15, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 04, 16, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 14, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third WeekDay of every 2 months every date. Scheluder will be used on 14/06/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekDay_StartBeforeAndAfterThirdWeekDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekDay_StartBeforeThirdWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 03, 13, 15, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 03, 15, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 15, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 16, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 16, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 17, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 18, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 18/03/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekDay_StartInRangeTimeThirdWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 10, 12, 4, 0, 1),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 10, 12, 6, 0, 1), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 13, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 13, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 14, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 14, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 15, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 10, 15, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Third WeekDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 15/10/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputThirdWeekDay_StartAfterThirdWeekDayOfMonth_Recurring3()
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 04, 12, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 04, 12, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 13, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 04, 14, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 04, 14, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Third WeekDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 14/04/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputFourthWeekDay_StartBeforeAndInRangeAndAfterFourthWeekDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthWeekDay_StartBeforeFourthWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 08, 22, 23, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 08, 23, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 08, 24, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 08, 25, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 08, 26, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 08, 27, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 09, 20, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 09, 21, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekDay of every 1 months every date. Scheluder will be used on 21/09/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthWeekDay_StartInRangeFourthWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 06, 24, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 1,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Fourth,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 06, 25, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 07, 19, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 20, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 21, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 22, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 23, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 08, 23, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekDay of every 1 months every date. Scheluder will be used on 23/08/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputFourthWeekDay_StartBeforeFourthWeekDayOfMonth_Recurring2()
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 18, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 19, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 20, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 21, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 22, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 22, 5, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Fourth WeekDay of every 2 months every date. Scheluder will be used on 22/03/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekDay_StartBeforeAndAfterFourthWeekDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekDay_StartBeforeFourthWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 02, 01, 15, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 22, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 22, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 23, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 23, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 24, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 24, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 25/02/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekDay_StartInRangeTimeFourthWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 02, 24, 4, 0, 1),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 24, 6, 0, 1), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 22, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 22, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Fourth WeekDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 22/03/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputFourthWeekDay_StartAfterFourthWeekDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 02, 27, 12, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 6);

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(new DateTime(2021, 05, 24, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 24, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 05, 25, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 05, 25, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 26, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 05, 26, 6, 0, 0), result[5]);
            Assert.AreEqual("Occurs the Fourth WeekDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 26/05/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyConfiguration_ImputLastWeekDay_StartBeforeAndInRangeAndAfterLastWeekDayOfMonth_Recurring1And2
        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastWeekDay_StartBeforeLastWeekDayOfMonth_Recurring1()
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 01, 25, 23, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 26, 23, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 27, 23, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 28, 23, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 29, 23, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 02, 22, 23, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 02, 23, 23, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekDay of every 1 months every date. Scheluder will be used on 23/02/2021 23:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastWeekDay_StartAfterLastWeekDayOfMonth_Recurring1()
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 02, 22, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 23, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 24, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 25, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 02, 26, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 29, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 03, 30, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekDay of every 1 months every date. Scheluder will be used on 30/03/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyConfiguration_ImputLastWeekDay_StartInRangeLastWeekDayOfMonth_Recurring2()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 03, 30, 5, 0, 0),
                Type = "Recurring",
                Occurs = "Monthly",
                OccursValue = 2,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 12, 31)
            };
            scheduler.MonthlyConfiguration = new MonthlyConfiguration()
            {
                FrecuencyType = FrecuencyType.Last,
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 03, 31, 5, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 31, 5, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 07, 26, 5, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 07, 27, 5, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 07, 28, 5, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 07, 29, 5, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 07, 30, 5, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekDay of every 2 months every date. Scheluder will be used on 30/07/2021 5:00:00" +
                " starting on 01/01/2021 0:00:00 and ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion

        #region Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekDay_StartBeforeAndAfterLastWeekDayOfMonth_Recurring1And3
        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekDay_StartBeforeLastWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 05, 30, 15, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 05, 31, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 31, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 06, 28, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 06, 28, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 29, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 29, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 06, 30, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekDay of every 1 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 30/06/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekDay_StartInRangeTimeLastWeekDayOfMonth_Recurring1()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 11, 30, 4, 0, 1),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 11, 30, 6, 0, 1), result[0]);
            Assert.AreEqual(new DateTime(2021, 12, 27, 4, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 12, 27, 6, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 12, 28, 4, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 12, 28, 6, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 12, 29, 4, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 12, 29, 6, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekDay of every 1 months every 2 Hour between 04:00:00 and 07:30:00. " +
                "Scheluder will be used on 29/12/2021 6:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }

        [TestMethod]
        public void Scheduler_MonthlyAndTimeConfiguration_ImputLastWeekDay_StartAfterLastWeekDayOfMonth_Recurring3()
        {
            var scheduler = new Scheduler
            {
                CurrentDate = new DateTime(2021, 03, 31, 12, 0, 0),
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
                WeeklyValue = WeeklyValue.WeekDay
            };

            var result = this.CalculateDatesSerie(scheduler, 7);

            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(new DateTime(2021, 06, 28, 4, 0, 0), result[0]);
            Assert.AreEqual(new DateTime(2021, 06, 28, 6, 0, 0), result[1]);
            Assert.AreEqual(new DateTime(2021, 06, 29, 4, 0, 0), result[2]);
            Assert.AreEqual(new DateTime(2021, 06, 29, 6, 0, 0), result[3]);
            Assert.AreEqual(new DateTime(2021, 06, 30, 4, 0, 0), result[4]);
            Assert.AreEqual(new DateTime(2021, 06, 30, 6, 0, 0), result[5]);
            Assert.AreEqual(new DateTime(2021, 09, 27, 4, 0, 0), result[6]);
            Assert.AreEqual("Occurs the Last WeekDay of every 3 months every 2 Hour between 04:00:00 and 06:00:00. " +
                "Scheluder will be used on 27/09/2021 4:00:00 starting on 01/01/2021 0:00:00 and " +
                "ending 31/12/2021 0:00:00", scheduler.OutDescription);
        }
        #endregion
    }
}
