using ColorMine.ColorSpaces;
using System;
using Xamarin.Forms;

namespace ColorPicker.BaseCore.Slider
{
    public class LightnessHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            return Color.FromHsla(color.Hue, color.Saturation, newValue, color.A);
        }

        protected override float GetSliderValue(Color color)
        {
            return (float)color.Luminosity;
        }
    }
}
