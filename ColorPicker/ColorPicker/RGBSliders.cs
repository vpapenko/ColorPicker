using System.Collections.Generic;

using ColorPicker.BaseClasses;
using ColorPicker.Classes;

namespace ColorPicker
{
    public class RGBSliders : SliderPickerWithAlpha
    {
        protected override IEnumerable<SliderBase> GetSliders()
        {
            var result = new List<Slider>() {
                new Slider(SliderFunctionsRGB.NewValueR, SliderFunctionsRGB.IsSelectedColorChanged
                , SliderFunctionsRGB.GetNewColorR, SliderFunctionsRGB.GetPaintR),
                new Slider(SliderFunctionsRGB.NewValueG, SliderFunctionsRGB.IsSelectedColorChanged
                , SliderFunctionsRGB.GetNewColorG, SliderFunctionsRGB.GetPaintG),
                new Slider(SliderFunctionsRGB.NewValueB, SliderFunctionsRGB.IsSelectedColorChanged
                , SliderFunctionsRGB.GetNewColorB, SliderFunctionsRGB.GetPaintB)
            };

            if (ShowAlphaSlider)
            {
                var slider = new Slider(SliderFunctionsAlpha.NewValueAlpha, SliderFunctionsAlpha.IsSelectedColorChangedAlpha
                    , SliderFunctionsAlpha.GetNewColorAlpha, SliderFunctionsAlpha.GetPaintAlpha)
                {
                    PaintChessPattern = true
                };
                result.Add(slider);
            }

            return result;
        }
    }
}
