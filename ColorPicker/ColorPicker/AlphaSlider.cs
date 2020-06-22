using System.Collections.Generic;

using ColorPicker.BaseClasses;
using ColorPicker.Classes;

namespace ColorPicker
{
    public class AlphaSlider : SliderPicker
    {
        protected override IEnumerable<SliderBase> GetSliders()
        {
            return new SliderBase[]
            {
                new Slider(SliderFunctionsAlpha.NewValueAlpha, SliderFunctionsAlpha.IsSelectedColorChangedAlpha
                , SliderFunctionsAlpha.GetNewColorAlpha, SliderFunctionsAlpha.GetPaintAlpha)
                {
                    PaintChessPattern = true
                }
            };
        }
    }
}
