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
        /*[TestMethod]
        public void CalculateMonthlyConfiguration_CurrentDateNotRangeDates_ReturnsException()
        {
            var MonthlyConf = new MonthlyConfiguration
            {
                Frecuency = FrecuencyType.First,
                WeeklyValue = WeeklyValue.Friday,
                NumberMonths = 1
            };

            var Result = new MonthlyConfigurationGestor().CalculateMonthlyConfiguration(
                new DateTime(2021, 11, 10), MonthlyConf.Frecuency, MonthlyConf.WeeklyValue);

            Assert.AreEqual(new DateTime(2021, 12, 03), Result);
        }*/
    }
}
