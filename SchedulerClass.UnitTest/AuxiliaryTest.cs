using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass.UnitTest
{
    [TestClass]
    public class AuxiliaryTest
    {
        [TestMethod]
        public void ValidateDateTime_InputValidateDateTime_ReturnTrue()
        {
            var auxiliary = new Auxiliary();

            var TheDate = new DateTime(2021, 12, 04);
            var result = auxiliary.ValidateDateTime(TheDate);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateDateTime_InputNullDateTime_ReturnFalse()
        {
            var auxiliary = new Auxiliary();

            var TheDate = new DateTime?();
            var result = auxiliary.ValidateDateTime(TheDate);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDateTime_InputNotRangeDateTime_ReturnFalse()
        {
            var auxiliary = new Auxiliary();

            var TheDate = new DateTime(9999, 12, 01);
            var result = auxiliary.ValidateDateTime(TheDate);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInteger_InputValidateInt_ReturnTrue()
        {
            var auxiliary = new Auxiliary();

            var TheInt = 23;
            var result = auxiliary.ValidateInteger(TheInt);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateInteger_InputNullInt_ReturnFalse()
        {
            var auxiliary = new Auxiliary();

            var TheInt = new int?();
            var result = auxiliary.ValidateInteger(TheInt);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInteger_InputNotRangeInt_ReturnFalse()
        {
            var auxiliary = new Auxiliary();

            var TheInt = 101;
            var result = auxiliary.ValidateInteger(TheInt);

            Assert.IsFalse(result);
        }

        //[TestMethod]
        //public void ValidateString_InputValidateString_ReturnTrue()
        //{
        //    var auxiliary = new Auxiliary();

        //    string TheString = string.;
        //    var result = auxiliary.ValidateString(TheString);

        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ValidateString_InputNotRangeString_ReturnFalse()
        //{
        //    var auxiliary = new Auxiliary();

        //    var TheString = "s";
        //    var result = auxiliary.ValidateString(TheString);

        //    Assert.IsFalse(result);
        //}
    }
}
