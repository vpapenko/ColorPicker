using Xamarin.Forms;

namespace ColorPicker.BaseCore.Slider
{
    public class AlphaHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            return Color.FromRgba(color.R, color.G, color.B, newValue);
        }

        protected override float GetSliderValue(Color color)
        {
            return (float)color.A;
        }
    }
}
