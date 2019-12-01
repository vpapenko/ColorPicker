using System;
using SkiaSharp;
using Xamarin.Forms;

namespace ColorPicker
{
    public class Slider : SliderBase
    {
        private Func<Color, float> _newValue;
        private Func<Color, bool> _isSelectedColorChanged;
        private Func<float, Color, Color> _getNewColor;
        private Func<Color, SKPoint, SKPoint, SKPaint> _getPaint;

        public Slider(Func<Color, float> newValue,Func<Color, bool> isSelectedColorChanged
            ,Func<float, Color, Color> getNewColor, Func<Color, SKPoint, SKPoint, SKPaint> getPaint)
        {
            _newValue = newValue;
            _isSelectedColorChanged = isSelectedColorChanged;
            _getNewColor = getNewColor;
            _getPaint = getPaint;
        }

        public override Color GetNewColor(float newValue, Color oldColor)
        {
            return _getNewColor(newValue, oldColor);
        }

        public override SKPaint GetPaint(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            return _getPaint(color, startPoint, endPoint);
        }

        public override bool IsSelectedColorChanged(Color color)
        {
            return _isSelectedColorChanged(color);
        }

        public override float NewValue(Color color)
        {
            return _newValue(color);
        }
    }
}
