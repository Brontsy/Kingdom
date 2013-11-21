using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kingdom.Core.Interfaces.Entities;

namespace Kingdom.Entities.Tests.Position
{
    [TestClass]
    public class IsoCoordinateTests
    {
        [TestMethod]
        public void To2dCoordinate()
        {
            IsoCoordinate coordinate = new IsoCoordinate(350, 175);

            IRegion region = new Region(0, 0);

            I2dCoordinate result = coordinate.To2dCoordinate(region);

            Assert.AreEqual(700, result.X);
            Assert.AreEqual(0, result.Y);
        }

        [TestMethod]
        public void To2dPosition1()
        {
            IsoCoordinate coordinate = new IsoCoordinate(350, 175);

            I2dPosition result = coordinate.To2dPosition();

            Assert.AreEqual(7, result.X);
            Assert.AreEqual(0, result.Y);
        }

        [TestMethod]
        public void To2dPosition2()
        {
            IsoCoordinate coordinate = new IsoCoordinate(300, 200);

            I2dPosition result = coordinate.To2dPosition();

            Assert.AreEqual(7, result.X);
            Assert.AreEqual(1, result.Y);
        }

        [TestMethod]
        public void To2dPosition3()
        {
            IsoCoordinate coordinate = new IsoCoordinate(350, 225);

            I2dPosition result = coordinate.To2dPosition();

            Assert.AreEqual(8, result.X);
            Assert.AreEqual(1, result.Y);
        }

        [TestMethod]
        public void To2dPosition4()
        {
            IsoCoordinate coordinate = new IsoCoordinate(400, 200);

            I2dPosition result = coordinate.To2dPosition();

            Assert.AreEqual(8, result.X);
            Assert.AreEqual(0, result.Y);
        }
    }
}
