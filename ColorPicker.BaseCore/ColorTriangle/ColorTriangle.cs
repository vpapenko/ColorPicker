using ColorMine.ColorSpaces;
using ColorPicker.BaseCore.Extensions;
using System;
using Xamarin.Forms;

namespace ColorPicker.BaseCore.ColorTriangle
{
    public class ColorTriangle : ColoPickerBase
    {
        private const float triangleHeight = 0.75f;
        private const float triangleSide = 0.8660254f;
        private double lastHue = 0;

        public double Rotation { get; set; } = 0.523599;

        public override AbstractPoint ColorToPoint(Color color)
        {
            HSLToHSV(color, out double _, out double saturation, out double value);
            saturation -= 0.5;
            saturation *= triangleSide * (value);
            var point = new AbstractPoint((float)value * triangleHeight, (float)(saturation));
            point.Y = -point.Y;
            return Rotate(point, (float)Rotation).AddY(0.5f);
        }

        public override AbstractPoint FitToActiveAria(AbstractPoint point, Color color)
        {
            var result = Rotate(point.Clone().AddY(-0.5f), -(float)Rotation);
            result.X = result.X.Clamp(0, triangleHeight);
            var maxY = triangleSide * (result.X / triangleHeight) / 2;
            result.Y = result.Y.Clamp(-maxY, maxY);
            return Rotate(result, (float)Rotation).AddY(0.5f);
        }

        public override bool IsInActiveAria(AbstractPoint point, Color color)
        {
            var p = Rotate(point.Clone().AddY(-0.5f), (float)Rotation);
            if (p.X > triangleHeight)
            {
                return false;
            }
            var MaxY = triangleSide * p.X;
            return Math.Abs(p.Y) < MaxY;
        }

        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var p = FitToActiveAria(point, color);
            p = Rotate(p.AddY(-0.5f), -(float)Rotation);
            p.Y = -p.Y;
            var value = p.X / triangleHeight;
            var maxY = triangleSide * value;
            var y = p.Y + maxY / 2;
            var saturation = y / maxY;
            return Color.FromHsva(GetHue(color), saturation, value, color.A);
        }

        public static void HSLToHSV(Color color, out double h, out double s, out double v)
        {
            var hsl = new Hsl { H = color.Hue, S = color.Saturation, L = color.Luminosity };
            var hsv = hsl.To<Hsv>();
            h = hsv.H;
            s = hsv.S;
            v = hsv.V;
        }

        private AbstractPoint Rotate(AbstractPoint point, float angle)
        {
            point.AddX(-0.5f);
            var polar = point.ToPolarPoint();
            polar.AddAngle(angle);
            var p = polar.ToAbstractPoint();
            point.X = p.X;
            point.Y = p.Y;
            point.AddX(0.5f);
            return point;
        }

        private double GetHue(Color color)
        {
            ColorTriangle.HSLToHSV(color, out double _, out double saturation, out double _);
            var hue = saturation > 0 ? color.Hue : lastHue;
            lastHue = saturation <= 0 ? lastHue : color.Hue;
            return hue;
        }
    }
}
