using ColorMine.ColorSpaces;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class HueHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            var hsl = new Hsl() { H = newValue, S = color.GetSaturation(), L = color.GetBrightness() };
            var rgb = hsl.To<Rgb>();
            return Color.FromArgb(color.A, (int)rgb.R, (int)rgb.G, (int)rgb.B);
        }
    }
}
