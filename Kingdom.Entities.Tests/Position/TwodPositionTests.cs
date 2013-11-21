using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kingdom.Core.Interfaces.Entities;

namespace Kingdom.Entities.Tests.Position
{
    [TestClass]
    public class TwodPositionTests
    {
        [TestMethod]
        public void To2dCoordinate()
        {
            TwodPosition position = new TwodPosition(7, 0);

            IRegion region = new Region(0, 0);

            IIsoCoordinate result = position.ToIsoCoordinate(region);

            Assert.AreEqual(300, result.X);
            Assert.AreEqual(175, result.Y);
        }


        [TestMethod]
        public void To2dCoordinate1()
        {
            TwodPosition position = new TwodPosition(7, 0);

            IRegion region = new Region(0, 0);

            I2dCoordinate result = position.To2dCoordinate(region);

            Assert.AreEqual(700, result.X);
            Assert.AreEqual(0, result.Y);
        }


        [TestMethod]
        public void To2dCoordinate2()
        {
            TwodPosition position = new TwodPosition(0, 5);

            IRegion region = new Region(0, 0);

            I2dCoordinate result = position.To2dCoordinate(region);

            Assert.AreEqual(0, result.X);
            Assert.AreEqual(500, result.Y);
        }

        [TestMethod]
        public void To2dCoordinate3()
        {
            TwodPosition position = new TwodPosition(15, 12);

            IRegion region = new Region(0, 0);

            I2dCoordinate result = position.To2dCoordinate(region);

            Assert.AreEqual(1500, result.X);
            Assert.AreEqual(1200, result.Y);
        }

     
    }
}
