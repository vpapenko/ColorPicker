using Xamarin.Forms;

namespace ColorPicker.BaseCore
{
    public abstract class ColoPickerBase
    {
        public abstract bool IsInActiveAria(AbstractPoint point, Color color);
        public abstract AbstractPoint FitToActiveAria(AbstractPoint point, Color color);
        public abstract Color UpdateColor(AbstractPoint point, Color color);
        public abstract AbstractPoint ColorToPoint(Color color);

        protected AbstractPoint ShiftToCenter(AbstractPoint point)
        {
            point.X -= 0.5F;
            point.Y -= 0.5F;
            return point;
        }

        protected AbstractPoint ShiftFromCenter(AbstractPoint point)
        {
            point.X += 0.5F;
            point.Y += 0.5F;
            return point;
        }
    }
}
