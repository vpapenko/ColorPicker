using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class AlphaHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = (int)(GetSliderValue(point, color) * 255);
            return Color.FromArgb(newValue, color.R, color.G, color.B);
        }
    }
}
