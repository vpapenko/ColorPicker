using SkiaSharp;

using ColorPicker.BaseClasses;

namespace ColorPicker.Classes
{
    public class SliderLocation
    {
        public SliderLocation(SliderBase slider)
        {
            Slider = slider;
        }
        public SliderBase Slider { get; private set; }
        public long? LocationProgressId { get; set; }
        public SKPoint Location { get; set; } = new SKPoint();
        public float TopLocationMultiplier { get; set; }
        public float GetSliderTop(float PickerRadiusPixels)
        {
            return PickerRadiusPixels * TopLocationMultiplier;
        }
    }
}
