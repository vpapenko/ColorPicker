using System.Drawing;

namespace ColorPicker.BaseCore.Slider
{
    public class GreenHorisontalSlider : SliderBase
    {
        public override Color UpdateColor(AbstractPoint point, Color color)
        {
            var newValue = (int)(GetSliderValue(point, color) * 255);
            return Color.FromArgb(color.A, color.R, newValue, color.B);
        }
    }
}
