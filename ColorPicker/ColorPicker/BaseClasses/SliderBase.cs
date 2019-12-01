
using SkiaSharp;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class SliderBase
    {
        public abstract float NewValue(Color color);
        public abstract bool IsSelectedColorChanged(Color color);
        public abstract Color GetNewColor(float newValue, Color oldColor);
        public abstract SKPaint GetPaint(Color color, SKPoint startPoint, SKPoint endPoint);
    }
}
