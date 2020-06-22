using System.Collections.Generic;

using ColorPicker.BaseClasses;
using ColorPicker.Classes;

namespace ColorPicker
{
    public class LuminositySlider : SliderPicker
    {
        protected override IEnumerable<SliderBase> GetSliders()
        {
            return new SliderBase[]
            {
                new Slider(SliderFunctionsHSL.NewValueL, SliderFunctionsHSL.IsSelectedColorChangedL
                , SliderFunctionsHSL.GetNewColorL, SliderFunctionsHSL.GetPaintL)
            };
        }
    }
}
