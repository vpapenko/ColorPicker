using ColorMine.ColorSpaces;
using System;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class ColorCircle : ColoPickerBase
    {
        public override AbstractPoint FitToActiveAria(AbstractPoint point, Color color)
        {
            point = ShiftToCenter(point);
            var polar = new PolarPoint(point);
            polar.Radius = polar.Radius > 1 ? 1 : polar.Radius;
            point = polar.ToAbstractPoint();
            point = ShiftFromCenter(point);
            return point;
        }

        public override bool IsInActiveAria(AbstractPoint point, Color color)
        {
            point = ShiftToCenter(point);
            var polar = new PolarPoint(point);
            return polar.Radius <= 1;
        }

        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            point = FitToActiveAria(point, color);
            var polar = new PolarPoint(point);
            var h = (Math.PI - polar.Angle) / (2 * Math.PI);
            var s = polar.Radius;
            var hsl = new Hsl() { H = h, S = s, L = color.GetBrightness() };
            var rgb = hsl.To<Rgb>();
            return Color.FromArgb(color.A, (int)(rgb.R * 255F), (int)(rgb.G * 255F), (int)(rgb.B * 255F));
        }
    }
}
