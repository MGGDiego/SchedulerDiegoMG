﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class TimeConfigurationTest
    {
        [TestMethod]
        public void CalculateHours_OccursOnceIsChecked_Returns28Oct15H23m00s()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), false)
            {
                OnceTime = new DateTime(2022, 01, 01, 15, 23, 00)
            };

            var result = timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 17, 00, 00));

            Assert.AreEqual(new DateTime(2021, 10, 28, 15, 23, 00), result);
        }

        [TestMethod]
        public void CalculateHours_OccursEveryIsChecked_Returns28Oct19H00m00s()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };
            
            var result = timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 17, 00, 00));

            Assert.AreEqual(new DateTime(2021, 10, 28, 19, 00, 00), result);
        }

        [TestMethod]
        public void CalculateHours_OccursEveryIsChecked_Returns28Oct12H00m00s()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var result = timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 21, 00, 00));

            Assert.AreEqual(new DateTime(2021, 10, 28, 12, 00, 00), result);
        }

        [TestMethod]
        public void CalculateHours_OccursEveryIsChecked_Returns28Oct18H30m00s()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), true)
            {
                OccursTime = "Minute",
                OccursTimeValue = 30
            };

            var result = timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 18, 00, 00));

            Assert.AreEqual(new DateTime(2021, 10, 28, 18, 30, 00), result);
        }

        [TestMethod]
        public void CalculateHours_OccursEveryIsChecked_Returns28Oct18H00m30s()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), true)
            {
                OccursTime = "Second",
                OccursTimeValue = 30
            };

            var result = timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 18, 00, 00));

            Assert.AreEqual(new DateTime(2021, 10, 28, 18, 00, 30), result);
        }

        [TestMethod]
        public void CalculateHours_OccursOnceIsChecked_ReturnsException()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), false)
            {
                OnceTime = new DateTime(2022, 01, 01, 21, 23, 00)
            };

            var ex = Assert.ThrowsException<Exception>(() => timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 17, 00, 00)));

            Assert.AreEqual("The times are not in the range established in the Configuration.", ex.Message);
        }

        [TestMethod]
        public void CalculateHours_OccursEveryIsChecked_ReturnsException()
        {
            var timeConfiguration = new TimeConfiguration(
                new DateTime(2022, 01, 01, 12, 00, 00), new DateTime(2022, 01, 01, 20, 00, 00), true)
            {
                OccursTime = "Hour",
                OccursTimeValue = 2
            };

            var ex = Assert.ThrowsException<Exception>(() => timeConfiguration.CalculateHours(new DateTime(2021, 10, 28, 19, 00, 00)));

            Assert.AreEqual("The times are not in the range established in the Configuration.", ex.Message);
        }
    }
}
