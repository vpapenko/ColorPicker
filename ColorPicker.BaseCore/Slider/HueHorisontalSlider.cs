using ColorMine.ColorSpaces;
using System;
using Xamarin.Forms;

namespace ColorPicker.BaseCore.Slider
{
    public class HueHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            return Color.FromHsla(newValue, color.Saturation, color.Luminosity, color.A);
        }

        protected override float GetSliderValue(Color color)
        {
            return (float)color.Hue;
        }
    }
}
