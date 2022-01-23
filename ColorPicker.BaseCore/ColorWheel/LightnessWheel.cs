using ColorMine.ColorSpaces;
using System;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class LightnessWheel : ColoPickerBase
    {
        private float _internalRadius = 0.25F;
        private float MiddleRadius => (0.5F - _internalRadius) / 2 + _internalRadius;

        public LightnessWheel()
        {

        }

        public LightnessWheel(float internalRadius)
        {
            InternalRadius = internalRadius;
        }

        public float InternalRadius
        {
            get
            {
                return _internalRadius;
            }
            set
            {
                value = value < 0 ? 0 : value;
                value = value > 0.5F ? 0.5F : value;
                _internalRadius = value;
            }
        }

        public override AbstractPoint FitToActiveAria(AbstractPoint point, Color color)
        {
            var polar = ToPolarPoint(point);
            point = polar.ToAbstractPoint();
            point = ShiftFromCenter(point);
            return point;
        }

        public override bool IsInActiveAria(AbstractPoint point, Color color)
        {
            point = ShiftToCenter(point);
            var polar = new PolarPoint(point);
            return polar.Radius <= 0.5 && polar.Radius >= _internalRadius;
        }

        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var polar = ToPolarPoint(point);

            polar.Angle += (float)Math.PI / 2F;
            polar = new PolarPoint(polar.ToAbstractPoint());
            var l = Math.Abs(polar.Angle) / Math.PI;

            var hsl = new Hsl() { H = color.GetHue(), S = color.GetSaturation(), L = l };
            var rgb = hsl.To<Rgb>();
            return Color.FromArgb(color.A, (int)rgb.R, (int)rgb.G, (int)rgb.B);
        }

        private PolarPoint ToPolarPoint(AbstractPoint point)
        {
            point = ShiftToCenter(point);
            return new PolarPoint(point)
            {
                Radius = MiddleRadius
            };
        }
    }
}
