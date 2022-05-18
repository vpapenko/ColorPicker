using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace ColorPicker.BaseCore.Tests
{
    [TestClass]
    public class PolarPointTests
    {
        private const float Precision = 0.000001F;

        [TestMethod]
        public void PolarPointTestConstructor1()
        {
            var point = new PolarPoint(0, 0);
            Assert.AreEqual(0, point.Radius);
            Assert.AreEqual(0, point.Angle);
        }

        [TestMethod]
        public void PolarPointTestConstructor2()
        {
            var point = new PolarPoint(1, 0);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(0, point.Angle);
        }

        [TestMethod]
        public void PolarPointTestConstructor3()
        {
            var point = new PolarPoint(-1, 0);
            Assert.AreEqual(-1, point.Radius);
            Assert.AreEqual(0, point.Angle);
        }

        [TestMethod]
        public void PolarPointTestConstructor4()
        {
            var point = new PolarPoint(10, 0);
            Assert.AreEqual(10, point.Radius);
            Assert.AreEqual(0, point.Angle);
        }

        [TestMethod]
        public void PolarPointTestConstructor5()
        {
            var point = new PolarPoint(1, (float)Math.PI);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual((float)Math.PI, Math.Abs(point.Angle), Precision);
        }

        [TestMethod]
        public void PolarPointTestConstructor6()
        {
            var point = new PolarPoint(1, 2 * (float)Math.PI);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(0, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestConstructor7()
        {
            var point = new PolarPoint(1, 3 * (float)Math.PI);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual((float)Math.PI, Math.Abs(point.Angle), Precision);
        }

        [TestMethod]
        public void PolarPointTestAbstractPointConstructor1()
        {
            var abstractPoint = new AbstractPoint(0, 0);
            var point = new PolarPoint(abstractPoint);
            Assert.AreEqual(0, point.Radius);
            Assert.AreEqual(0, point.Angle);
        }

        [TestMethod]
        public void PolarPointTestAbstractPointConstructor2()
        {
            var abstractPoint = new AbstractPoint(0, 1);
            var point = new PolarPoint(abstractPoint);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual((float)Math.PI / 2, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestAbstractPointConstructor3()
        {
            var abstractPoint = new AbstractPoint(0, -1);
            var point = new PolarPoint(abstractPoint);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(-(float)Math.PI / 2, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestAbstractPointConstructor4()
        {
            var abstractPoint = new AbstractPoint(1, 0);
            var point = new PolarPoint(abstractPoint);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(0, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestAbstractPointConstructor5()
        {
            var abstractPoint = new AbstractPoint(-1, 0);
            var point = new PolarPoint(abstractPoint);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(-(float)Math.PI, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestAbstractPointConstructor6()
        {
            var abstractPoint = new AbstractPoint(0.49999997F, 0.866025448F);
            var point = new PolarPoint(abstractPoint);
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual((float)Math.PI / 3, point.Angle, Precision);
        }
        [TestMethod]
        public void PolarPointTestToPolarPoint1()
        {
            var abstractPoint = new AbstractPoint(0, 0);
            var point = abstractPoint.ToPolarPoint();
            Assert.AreEqual(0, point.Radius);
            Assert.AreEqual(0, point.Angle);
        }

        [TestMethod]
        public void PolarPointTestToPolarPoint2()
        {
            var abstractPoint = new AbstractPoint(0, 1);
            var point = abstractPoint.ToPolarPoint();
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual((float)Math.PI / 2, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestToPolarPoint3()
        {
            var abstractPoint = new AbstractPoint(0, -1);
            var point = abstractPoint.ToPolarPoint();
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(-(float)Math.PI / 2, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestToPolarPoint4()
        {
            var abstractPoint = new AbstractPoint(1, 0);
            var point = abstractPoint.ToPolarPoint();
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(0, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestToPolarPoint5()
        {
            var abstractPoint = new AbstractPoint(-1, 0);
            var point = abstractPoint.ToPolarPoint();
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual(-(float)Math.PI, point.Angle, Precision);
        }

        [TestMethod]
        public void PolarPointTestToPolarPoint6()
        {
            var abstractPoint = new AbstractPoint(0.49999997F, 0.866025448F);
            var point = abstractPoint.ToPolarPoint();
            Assert.AreEqual(1, point.Radius);
            Assert.AreEqual((float)Math.PI / 3, point.Angle, Precision);
        }

        [TestMethod]
        public void ToAbstractPointTest1()
        {
            var point = new PolarPoint(1, 0);
            var abstractPoint = point.ToAbstractPoint();
            Assert.AreEqual(1F, abstractPoint.X);
            Assert.AreEqual(0, abstractPoint.Y);
        }

        [TestMethod]
        public void ToAbstractPointTest2()
        {
            var point = new PolarPoint(1, (float)Math.PI / 2);
            var abstractPoint = point.ToAbstractPoint();
            Assert.AreEqual(0, abstractPoint.X, Precision);
            Assert.AreEqual(1, abstractPoint.Y);
        }

        [TestMethod]
        public void ToAbstractPointTest3()
        {
            var point = new PolarPoint(1, (float)Math.PI);
            var abstractPoint = point.ToAbstractPoint();
            Assert.AreEqual(-1, abstractPoint.X);
            Assert.AreEqual(0, abstractPoint.Y, Precision);
        }

        [TestMethod]
        public void ToAbstractPointTest4()
        {
            var point = new PolarPoint(1, (float)Math.PI * 1.5F);
            var abstractPoint = point.ToAbstractPoint();
            Assert.AreEqual(0, abstractPoint.X, Precision);
            Assert.AreEqual(-1, abstractPoint.Y);
        }

        [TestMethod]
        public void ToAbstractPointTest5()
        {
            var point = new PolarPoint(1, (float)Math.PI / 3);
            var abstractPoint = point.ToAbstractPoint();
            Assert.AreEqual(0.49999997F, abstractPoint.X);
            Assert.AreEqual(0.866025448F, abstractPoint.Y);
        }

        [TestMethod]
        public void AngleTest1()
        {
            var point = new PolarPoint(0, 0)
            {
                Radius = 23.3563F,
                Angle = 4.6346F
            };
            Assert.AreEqual(23.3563F, point.Radius);
            Assert.AreEqual(-1.6485852003097534, point.Angle);
        }

        [TestMethod]
        public void AngleTest2()
        {
            var point = new PolarPoint(0, 0)
            {
                Radius = 23.3563F,
                Angle = 46.6346F
            };
            Assert.AreEqual(23.3563F, point.Radius);
            Assert.AreEqual(2.6523044F, point.Angle);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var point = new PolarPoint(4.6433F, 2.65435F);
            var separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Assert.AreEqual($"R: 4{separator}6433; A: 2{separator}65435", point.ToString());
        }
    }
}