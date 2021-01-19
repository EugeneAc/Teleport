using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teleport.BusinessLogic;

namespace Teleport.Tests
{
    [TestClass]
    public class DistanceAlgorithmTests
    {
        private readonly int _maxTolerance = 10;

        [TestMethod]
        public void TestDistanceCalculationAmsAsw()
        {
            var distance = DistanceAlgorithm.GetDistanceBetweenPlaces(52.309069, 4.763385, 23.968022, 32.824818);
            Assert.IsTrue(Math.Abs(2313 - distance) <= _maxTolerance); // https://www.nhc.noaa.gov/
        }

        [TestMethod]
        public void TestDistanceCalculationAmsJfk()
        {
            var distance = DistanceAlgorithm.GetDistanceBetweenPlaces(52.309069, 4.763385, 40.642335, -73.78817);
            Assert.IsTrue(Math.Abs(4733 - distance) <= _maxTolerance); // https://www.nhc.noaa.gov/
        }
    }
}
