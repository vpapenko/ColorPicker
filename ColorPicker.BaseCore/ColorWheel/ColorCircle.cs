using ColorMine.ColorSpaces;
using System;
using Xamarin.Forms;

namespace ColorPicker.BaseCore.ColorWheel
{
    public class ColorCircle : ColoPickerBase
    {
        public double Rotation { get; set; }

        public override AbstractPoint ColorToPoint(Color color)
        {
            var r = (float)color.Saturation / 2;
            var a = (float)(color.Hue * 2 * Math.PI) - (float)Rotation;
            var polar = new PolarPoint(r, a);
            var point = polar.ToAbstractPoint();
            point.X = -point.X;
            point = ShiftFromCenter(point);
            return point;
        }

        public override AbstractPoint FitToActiveAria(AbstractPoint point, Color color)
        {
            point = ShiftToCenter(point);
            var polar = point.ToPolarPoint();
            polar.Radius = polar.Radius > 0.5F ? 0.5F : polar.Radius;
            point = polar.ToAbstractPoint();
            point = ShiftFromCenter(point);
            return point;
        }

        public override bool IsInActiveAria(AbstractPoint point, Color color)
        {
            point = ShiftToCenter(point);
            var polar = point.ToPolarPoint();
            return polar.Radius <= 1;
        }

        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            point = FitToActiveAria(point, color);
            point = ShiftToCenter(point);
            point.Y = -point.Y;
            var polar = point.ToPolarPoint();
            polar.Angle += (float)Rotation;
            polar = polar.ToAbstractPoint().ToPolarPoint();
            var h = (polar.Angle + Math.PI) / (Math.PI * 2);
            var s = polar.Radius * 2;
            return Color.FromHsla(h, s, color.Luminosity, color.A);
        }
    }
}
