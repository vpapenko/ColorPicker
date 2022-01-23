using ColorMine.ColorSpaces;
using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class LightnessHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = GetSliderValue(point, color);
            var hsl = new Hsl() { H = color.GetHue() / 360, S = color.GetSaturation(), L = newValue };
            var rgb = hsl.To<Rgb>();
            return Color.FromArgb(color.A, (int)rgb.R, (int)rgb.G, (int)rgb.B);
        }
    }
}
