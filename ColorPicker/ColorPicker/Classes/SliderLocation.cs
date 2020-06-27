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
        public float OffsetLocationMultiplier { get; set; }
        public float GetSliderOffset(float PickerRadiusPixels)
        {
            return PickerRadiusPixels * OffsetLocationMultiplier;
        }
    }
}
