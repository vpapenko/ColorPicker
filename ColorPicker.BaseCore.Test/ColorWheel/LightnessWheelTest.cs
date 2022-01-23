using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider.Tests
{
    [TestClass()]
    public class LightnessWheelTest
    {
        [DataTestMethod]
        [DynamicData(nameof(FitToActiveAriaTestData), DynamicDataSourceType.Property)]
        public void FitToActiveAriaTest(AbstractPoint point, Color inputColor, float x, float y)
        {
            var coloPicker = new LightnessWheel();
            var p = coloPicker.FitToActiveAria(point, inputColor);
            var text = $"Input: [{point.X}; {point.Y}], Expected: [{x}; {y}], Actual: [{p.X}; {p.Y}]";
            Assert.AreEqual(x, p.X, Delta, $"Incorrect X. " + text);
            Assert.AreEqual(y, p.Y, Delta, $"Incorrect Y. " + text);
        }

        [DataTestMethod]
        [DynamicData(nameof(IsInActiveAriaTestData), DynamicDataSourceType.Property)]
        public void IsInActiveAriaTest(AbstractPoint point, Color inputColor, bool isInActive)
        {
            var coloPicker = new LightnessWheel();
            Assert.AreEqual(isInActive, coloPicker.IsInActiveAria(point, inputColor), $"Input: [{point.X}; {point.Y}]");
        }

        [DataTestMethod]
        [DynamicData(nameof(UpdateColorTestData), DynamicDataSourceType.Property)]
        public void UpdateColorTest(AbstractPoint point, Color inputColor, Color outputColor)
        {
            var coloPicker = new LightnessWheel();
            var c = coloPicker.UpdateColor(point, inputColor);
            Assert.AreEqual(outputColor, c, $"Input: [{point.X}; {point.Y}]");
        }

        private const double Delta = 0.000001;

        public static IEnumerable<object[]> FitToActiveAriaTestData
        {
            get
            {
                foreach (var color in _colors)
                {
                    yield return new object[] { new AbstractPoint(0.5F, 0.5F), color, 0.875F, 0.5F };

                    yield return new object[] { new AbstractPoint(0, 0.5F), color, 0.125F, 0.5F };
                    yield return new object[] { new AbstractPoint(0.5F, 1F), color, 0.5F, 0.875F };
                    yield return new object[] { new AbstractPoint(1, 0.5F), color, 0.875F, 0.5F };
                    yield return new object[] { new AbstractPoint(0.5F, 0F), color, 0.5F, 0.125F };

                    yield return new object[] { new AbstractPoint(0.1F, 0.5F), color, 0.125F, 0.5F };
                    yield return new object[] { new AbstractPoint(0.5F, 0.9F), color, 0.5F, 0.875F };
                    yield return new object[] { new AbstractPoint(0.9F, 0.5F), color, 0.875F, 0.5F };
                    yield return new object[] { new AbstractPoint(0.5F, 0.1F), color, 0.5F, 0.125F };

                    yield return new object[] { new AbstractPoint(0.4F, 0.5F), color, 0.125F, 0.5F };
                    yield return new object[] { new AbstractPoint(0.5F, 0.6F), color, 0.5F, 0.875F };
                    yield return new object[] { new AbstractPoint(0.6F, 0.5F), color, 0.875F, 0.5F };
                    yield return new object[] { new AbstractPoint(0.5F, 0.4F), color, 0.5F, 0.125F };

                    yield return new object[] { new AbstractPoint(0F, 0F), color, 0.2348349F, 0.2348349F };
                    yield return new object[] { new AbstractPoint(1F, 0F), color, 0.7651651F, 0.2348349F };
                    yield return new object[] { new AbstractPoint(1F, 1F), color, 0.7651651F, 0.7651651F };
                    yield return new object[] { new AbstractPoint(0F, 1F), color, 0.2348349F, 0.7651651F };
                }
            }
        }

        public static IEnumerable<object[]> IsInActiveAriaTestData
        {
            get
            {
                foreach (var color in _colors)
                {
                    yield return new object[] { new AbstractPoint(0.5F, 0.5F), color, false };

                    yield return new object[] { new AbstractPoint(0, 0.5F), color, true };
                    yield return new object[] { new AbstractPoint(0.5F, 1F), color, true };
                    yield return new object[] { new AbstractPoint(1, 0.5F), color, true };
                    yield return new object[] { new AbstractPoint(0.5F, 0F), color, true };

                    yield return new object[] { new AbstractPoint(0.1F, 0.5F), color, true };
                    yield return new object[] { new AbstractPoint(0.5F, 0.9F), color, true };
                    yield return new object[] { new AbstractPoint(0.9F, 0.5F), color, true };
                    yield return new object[] { new AbstractPoint(0.5F, 0.1F), color, true };

                    yield return new object[] { new AbstractPoint(0.4F, 0.5F), color, false };
                    yield return new object[] { new AbstractPoint(0.5F, 0.6F), color, false };
                    yield return new object[] { new AbstractPoint(0.6F, 0.5F), color, false };
                    yield return new object[] { new AbstractPoint(0.5F, 0.4F), color, false };

                    yield return new object[] { new AbstractPoint(0F, 0F), color, false };
                    yield return new object[] { new AbstractPoint(1F, 0F), color, false };
                    yield return new object[] { new AbstractPoint(1F, 1F), color, false };
                    yield return new object[] { new AbstractPoint(0F, 1F), color, false };
                }
            }
        }

        public static IEnumerable<object[]> UpdateColorTestData
        {
            get
            {
                var color = _colors[0];
                yield return new object[] { new AbstractPoint(0.5F, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };

                yield return new object[] { new AbstractPoint(0, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { new AbstractPoint(0.5F, 1F), color, Color.FromArgb(0, 254, 254, 254) };
                yield return new object[] { new AbstractPoint(1, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { new AbstractPoint(0.5F, 0F), color, Color.FromArgb(0, 0, 0, 0) };

                yield return new object[] { new AbstractPoint(0.1F, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { new AbstractPoint(0.5F, 0.9F), color, Color.FromArgb(0, 254, 254, 254) };
                yield return new object[] { new AbstractPoint(0.9F, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { new AbstractPoint(0.5F, 0.1F), color, Color.FromArgb(0, 0, 0, 0) };

                yield return new object[] { new AbstractPoint(0.4F, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { new AbstractPoint(0.5F, 0.6F), color, Color.FromArgb(0, 254, 254, 254) };
                yield return new object[] { new AbstractPoint(0.6F, 0.5F), color, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { new AbstractPoint(0.5F, 0.4F), color, Color.FromArgb(0, 0, 0, 0) };

                yield return new object[] { new AbstractPoint(0F, 0F), color, Color.FromArgb(0, 63, 63, 63) };
                yield return new object[] { new AbstractPoint(1F, 0F), color, Color.FromArgb(0, 63, 63, 63) };
                yield return new object[] { new AbstractPoint(1F, 1F), color, Color.FromArgb(0, 191, 191, 191) };
                yield return new object[] { new AbstractPoint(0F, 1F), color, Color.FromArgb(0, 191, 191, 191) };
            }
        }

        private static readonly Color[] _colors =
        {
            Color.FromArgb(0, 0, 0, 0),
            Color.FromArgb(0, 0, 0, 127),
            Color.FromArgb(0, 0, 0, 255),
            Color.FromArgb(127, 127, 127, 0),
            Color.FromArgb(127, 127, 127, 127),
            Color.FromArgb(127, 127, 127, 255),
            Color.FromArgb(255, 255, 255, 0),
            Color.FromArgb(255, 255, 255, 127),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(127, 0, 0, 0),
            Color.FromArgb(127, 0, 0, 127),
            Color.FromArgb(127, 0, 0, 255),
            Color.FromArgb(0, 127, 0, 0),
            Color.FromArgb(0, 127, 0, 127),
            Color.FromArgb(0, 127, 0, 255),
            Color.FromArgb(0, 0, 127, 0),
            Color.FromArgb(0, 0, 127, 127),
            Color.FromArgb(0, 0, 127, 255),
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 0, 0, 127),
            Color.FromArgb(255, 0, 0, 255),
            Color.FromArgb(0, 255, 0, 0),
            Color.FromArgb(0, 255, 0, 127),
            Color.FromArgb(0, 255, 0, 255),
            Color.FromArgb(0, 0, 255, 0),
            Color.FromArgb(0, 0, 255, 127),
            Color.FromArgb(0, 0, 255, 255),
            Color.FromArgb(34, 201, 155, 198)
        };
    }
}