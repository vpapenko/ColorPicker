using System;
using SkiaSharp;
using Xamarin.Forms;

namespace ColorPicker.BaseClasses
{
    public class Slider : SliderBase
    {
        private readonly Func<Color, float> newValue;
        private readonly Func<Color, bool> isSelectedColorChanged;
        private readonly Func<float, Color, Color> getNewColor;
        private readonly Func<Color, SKPoint, SKPoint, SKPaint> getPaint;

        public Slider(Func<Color, float> newValue, Func<Color, bool> isSelectedColorChanged
            , Func<float, Color, Color> getNewColor, Func<Color, SKPoint, SKPoint, SKPaint> getPaint)
        {
            this.newValue = newValue;
            this.isSelectedColorChanged = isSelectedColorChanged;
            this.getNewColor = getNewColor;
            this.getPaint = getPaint;
        }

        public override Color GetNewColor(float newValue, Color oldColor)
        {
            return getNewColor(newValue, oldColor);
        }

        public override SKPaint GetPaint(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            return getPaint(color, startPoint, endPoint);
        }

        public override bool IsSelectedColorChanged(Color color)
        {
            return isSelectedColorChanged(color);
        }

        public override float NewValue(Color color)
        {
            return newValue(color);
        }
    }
}
