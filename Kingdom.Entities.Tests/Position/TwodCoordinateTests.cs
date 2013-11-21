using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kingdom.Core.Interfaces.Entities;

namespace Kingdom.Entities.Tests.Position
{
    [TestClass]
    public class TwodCoordinateTests
    {
        [TestMethod]
        public void To2dPosition()
        {
            TwodCoordinate coordinate = new TwodCoordinate(400, 0);

            IRegion region = new Region(0, 0);

            I2dPosition result = coordinate.To2dPosition();

            Assert.AreEqual(4, result.X);
            Assert.AreEqual(0, result.Y);
        }

        [TestMethod]
        public void To2dPosition1()
        {
            TwodCoordinate coordinate = new TwodCoordinate(700, 0);

            IRegion region = new Region(0, 0);

            IIsoCoordinate result = coordinate.ToIsoCoordinate(region);

            Assert.AreEqual(300, result.X);
            Assert.AreEqual(175, result.Y);
        }
    }
}
