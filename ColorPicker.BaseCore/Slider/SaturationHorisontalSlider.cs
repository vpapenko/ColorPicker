using ColorMine.ColorSpaces;
using System;
using Xamarin.Forms;

namespace ColorPicker.BaseCore.Slider
{
    public class SaturationHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            return Color.FromHsla(color.Hue, newValue, color.Luminosity, color.A);
        }

        protected override float GetSliderValue(Color color)
        {
            return (float)color.Saturation;
        }
    }
}