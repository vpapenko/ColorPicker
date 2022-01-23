using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider.Tests
{
    [TestClass()]
    public class SliderTests
    {
        [DataTestMethod]
        [DynamicData(nameof(FitToActiveAriaTestData), DynamicDataSourceType.Property)]
        public void FitToActiveAriaTest(string typeName, bool isHorisontal, AbstractPoint point, Color inputColor, float x, float y)
        {
            if (!isHorisontal)
            {
                (x, y) = (y, x);
                (point.X, point.Y) = (point.Y, point.X);
            }
            var coloPicker = CreateColoPickerInstance(typeName);
            var p = coloPicker.FitToActiveAria(point, inputColor);
            Assert.AreEqual(x, p.X, "incorrect X");
            Assert.AreEqual(y, p.Y, "incorrect Y");
        }

        [DataTestMethod]
        [DynamicData(nameof(IsInActiveAriaTestData), DynamicDataSourceType.Property)]
        public void IsInActiveAriaTest(string typeName, AbstractPoint point, Color inputColor, bool isInActive)
        {
            var coloPicker = CreateColoPickerInstance(typeName);
            Assert.AreEqual(isInActive, coloPicker.IsInActiveAria(point, inputColor));
        }

        [DataTestMethod]
        [DynamicData(nameof(UpdateColorTestData), DynamicDataSourceType.Property)]
        public void UpdateColorTest(string typeName, AbstractPoint point, Color inputColor, Color outputColor)
        {
            var coloPicker = CreateColoPickerInstance(typeName);
            var c = coloPicker.UpdateColor(point, inputColor);
            Assert.AreEqual(outputColor, c);
        }

        public static IEnumerable<object[]> UpdateColorTestData
        {
            get
            {
                var typeName = _types[0].Name;
                var currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(127, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(127, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(127, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 0, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(127, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(127, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(127, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(127, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(127, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(127, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 201, 155, 198) };


                typeName = _types[1].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(127, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(127, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(127, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 0, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(127, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(127, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(127, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(127, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(127, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(127, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 201, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 201, 155, 198) };


                typeName = _types[2].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(0, 127, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(0, 127, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 127, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 255, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(255, 127, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(255, 127, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 127, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(34, 127, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(34, 127, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 127, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 255, 155, 198) };


                typeName = _types[3].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(0, 127, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(0, 127, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 127, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 255, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 255, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(255, 127, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(255, 127, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 127, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 0, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0F, 0.5F), currentColor, Color.FromArgb(34, 127, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1F, 0.5F), currentColor, Color.FromArgb(34, 127, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0F), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 1F), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 127, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 0, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 255, 155, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 255, 155, 198) };

                //ToDo
                typeName = _types[4].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 127, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 255, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 127, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 201, 127, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 201, 255, 198) };


                typeName = _types[5].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 127, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 255, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 127, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 201, 127, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 201, 0, 198) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 201, 255, 198) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 201, 255, 198) };


                typeName = _types[6].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 0, 255) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 201, 155, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 201, 155, 255) };


                typeName = _types[7].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 0, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 0, 255) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 201, 155, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 201, 155, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 201, 155, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 201, 155, 255) };


                typeName = _types[8].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 201, 155, 155) };


                typeName = _types[9].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 201, 155, 155) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 201, 155, 155) };


                typeName = _types[10].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 216, 140, 139) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 255, 103, 101) };


                typeName = _types[11].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 216, 140, 139) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 178, 178, 178) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 255, 103, 101) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 255, 103, 101) };


                typeName = _types[12].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 255, 255, 255) };

                currentColor = _colors[8];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(255, 127, 127, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(255, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(255, 255, 255, 255) };

                currentColor = _colors[27];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(34, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(34, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(34, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(34, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(34, 165, 90, 89) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(34, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(34, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(34, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(34, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(34, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(34, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(34, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(34, 255, 255, 255) };

                typeName = _types[13].Name;
                currentColor = _colors[0];
                yield return new object[] { typeName, new AbstractPoint(0, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, 1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0.5F, 0.5F), currentColor, Color.FromArgb(0, 127, 127, 127) };
                yield return new object[] { typeName, new AbstractPoint(-1, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(0, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(1, -1), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(2, 0), currentColor, Color.FromArgb(0, 0, 0, 0) };
                yield return new object[] { typeName, new AbstractPoint(-1, 1), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(0, 2), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(1, 2), currentColor, Color.FromArgb(0, 255, 255, 255) };
                yield return new object[] { typeName, new AbstractPoint(2, 2), currentColor, Color.FromArgb(0, 255, 255, 255) };
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

        private static readonly (string Name, bool IsHorisontal, SliderChanel chanel)[] _types = new[]
        {
            ("ColorPicker.BaseCore.Slider.AlphaHorisontalSlider", true, SliderChanel.Alpha),
            ("ColorPicker.BaseCore.Slider.AlphaVerticalSlider", false, SliderChanel.Alpha),
            ("ColorPicker.BaseCore.Slider.RedHorisontalSlider", true, SliderChanel.Red),
            ("ColorPicker.BaseCore.Slider.RedVerticalSlider", false, SliderChanel.Red),
            ("ColorPicker.BaseCore.Slider.GreenHorisontalSlider", true, SliderChanel.Green),
            ("ColorPicker.BaseCore.Slider.GreenVerticalSlider", false, SliderChanel.Green),
            ("ColorPicker.BaseCore.Slider.BlueHorisontalSlider", true, SliderChanel.Blue),
            ("ColorPicker.BaseCore.Slider.BlueVerticalSlider", false, SliderChanel.Blue),
            ("ColorPicker.BaseCore.Slider.HueHorisontalSlider", true, SliderChanel.Hue),
            ("ColorPicker.BaseCore.Slider.HueVerticalSlider", false, SliderChanel.Hue),
            ("ColorPicker.BaseCore.Slider.SaturationHorisontalSlider", true, SliderChanel.Saturation),
            ("ColorPicker.BaseCore.Slider.SaturationVerticalSlider", false, SliderChanel.Saturation),
            ("ColorPicker.BaseCore.Slider.LightnessHorisontalSlider", true, SliderChanel.Lightness),
            ("ColorPicker.BaseCore.Slider.LightnessVerticalSlider", false, SliderChanel.Lightness)
        };

        public static IEnumerable<object[]> IsInActiveAriaTestData
        {
            get
            {
                foreach (var (Name, IsHorisontal, _) in _types)
                {
                    foreach (var color in _colors)
                    {
                        yield return new object[] { Name, new AbstractPoint(0, 0), color, true };
                        yield return new object[] { Name, new AbstractPoint(1, 0), color, true };
                        yield return new object[] { Name, new AbstractPoint(0, 1), color, true };
                        yield return new object[] { Name, new AbstractPoint(1, 1), color, true };
                        yield return new object[] { Name, new AbstractPoint(0F, 0.5F), color, true };
                        yield return new object[] { Name, new AbstractPoint(1F, 0.5F), color, true };
                        yield return new object[] { Name, new AbstractPoint(0.5F, 0F), color, true };
                        yield return new object[] { Name, new AbstractPoint(0.5F, 1F), color, true };
                        yield return new object[] { Name, new AbstractPoint(0.5F, 0.5F), color, true };
                        yield return new object[] { Name, new AbstractPoint(-1, 0), color, false };
                        yield return new object[] { Name, new AbstractPoint(0, -1), color, false };
                        yield return new object[] { Name, new AbstractPoint(1, -1), color, false };
                        yield return new object[] { Name, new AbstractPoint(2, 0), color, false };
                        yield return new object[] { Name, new AbstractPoint(-1, 1), color, false };
                        yield return new object[] { Name, new AbstractPoint(0, 2), color, false };
                        yield return new object[] { Name, new AbstractPoint(1, 2), color, false };
                        yield return new object[] { Name, new AbstractPoint(2, 2), color, false };
                    }
                }
            }
        }

        public static IEnumerable<object[]> FitToActiveAriaTestData
        {
            get
            {
                foreach (var (Name, IsHorisontal, _) in _types)
                {
                    foreach (var color in _colors)
                    {
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0, 0), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(1, 0), color, 1F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0, 1), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(1, 1), color, 1F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0F, 0.5F), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(1F, 0.5F), color, 1F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0.5F, 0F), color, 0.5F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0.5F, 1F), color, 0.5F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0.5F, 0.5F), color, 0.5F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(-1, 0), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0, -1), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(1, -1), color, 1F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(2, 0), color, 1F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(-1, 1), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(0, 2), color, 0F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(1, 2), color, 1F, 0.5F };
                        yield return new object[] { Name, IsHorisontal, new AbstractPoint(2, 2), color, 1F, 0.5F };
                    }
                }
            }
        }

        public static ColoPickerBase CreateColoPickerInstance(string typeName)
        {
            return (ColoPickerBase)Activator.CreateInstance("ColorPicker.BaseCore", typeName).Unwrap();
        }

        private enum SliderChanel
        {
            Alpha,
            Red,
            Green,
            Blue,
            Hue,
            Saturation,
            Lightness
        }
    }
}