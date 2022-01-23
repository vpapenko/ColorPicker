using ColorMine.ColorSpaces;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class SaturationHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            var hsl = new Hsl() { H = color.GetHue() / 360, S = newValue, L = color.GetBrightness() };
            var rgb = hsl.To<Rgb>();
            return Color.FromArgb(color.A, (int)rgb.R, (int)rgb.G, (int)rgb.B);
        }
    }
}
