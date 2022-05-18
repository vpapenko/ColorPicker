using Xamarin.Forms;

namespace ColorPicker.BaseCore.Slider
{
    public class BlueHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            return Color.FromRgba(color.R, color.G, newValue, color.A);
        }

        protected override float GetSliderValue(Color color)
        {
            return (float)color.B;
        }
    }
}
