using System;
using Xamarin.Forms;

namespace ColorPicker.BaseCore.ColorTriangle
{
    public class RotatingColorTriangle : ColoPickerBase
    {
        private readonly ColorTriangle _colorTriangle = new ColorTriangle();
        private double lastHue = 0;

        public override AbstractPoint ColorToPoint(Color color)
        {
            SetAngle(color);
            return _colorTriangle.ColorToPoint(color);
        }

        public override AbstractPoint FitToActiveAria(AbstractPoint point, Color color)
        {
            SetAngle(color);
            return _colorTriangle.FitToActiveAria(point, color);
        }

        public override bool IsInActiveAria(AbstractPoint point, Color color)
        {
            SetAngle(color);
            return _colorTriangle.IsInActiveAria(point, color);
        }

        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            SetAngle(color);
            return _colorTriangle.UpdateColor(point, color);
        }

        private void SetAngle(Color color)
        {
            ColorTriangle.HSLToHSV(color, out double _, out double saturation, out double _);
            var hue = saturation > 0 ? color.Hue : lastHue;
            lastHue = saturation <= 0 ? lastHue : color.Hue;
            _colorTriangle.Rotation = -2.094395 - hue * 2 * Math.PI;
        }
    }
}
