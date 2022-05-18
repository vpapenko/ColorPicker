using Xamarin.Forms;

namespace ColorPicker.BaseCore.Slider
{
    public class RedHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            return Color.FromRgba(newValue, color.G, color.B, color.A);
        }

        protected override float GetSliderValue(Color color)
        {
            return (float)color.R;
        }
    }
}
