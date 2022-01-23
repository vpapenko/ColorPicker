using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public abstract class SliderBase : ColoPickerBase
    {
        protected virtual Orientation Orientation { get => Orientation.Horisontal; }

        public override bool IsInActiveAria(AbstractPoint point, Color color)
        {
            return point.X >= 0 && point.X <= 1 && point.Y >= 0 && point.Y <= 1;
        }

        public override AbstractPoint FitToActiveAria(AbstractPoint point, Color color)
        {
            if (Orientation == Orientation.Horisontal)
            {
                point.X = LimitToSize(point.X);
                point.Y = 0.5f;
            }
            else
            {
                point.X = 0.5f;
                point.Y = LimitToSize(point.Y);
            }
            return point;
        }

        protected float GetSliderValue(AbstractPoint point, Color color)
        {
            point = FitToActiveAria(point, color);
            return Orientation == Orientation.Horisontal ? point.X : point.Y;
        }

        private float LimitToSize(float coordinate)
        {
            coordinate = coordinate < 0 ? 0 : coordinate;
            coordinate = coordinate > 1 ? 1 : coordinate;
            return coordinate;
        }
    }
}
