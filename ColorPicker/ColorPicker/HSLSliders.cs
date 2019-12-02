using System.Collections.Generic;

namespace ColorPicker
{
    public class HSLSliders : SliderPicker
    {
        protected override IEnumerable<SliderBase> GetSliders()
        {
            var result = new List<Slider>() {
                new Slider(SliderFunctionsHSL.NewValueH, SliderFunctionsHSL.IsSelectedColorChangedH
                , SliderFunctionsHSL.GetNewColorH, SliderFunctionsHSL.GetPaintH),
                new Slider(SliderFunctionsHSL.NewValueS, SliderFunctionsHSL.IsSelectedColorChangedS
                , SliderFunctionsHSL.GetNewColorS, SliderFunctionsHSL.GetPaintS),
                new Slider(SliderFunctionsHSL.NewValueL, SliderFunctionsHSL.IsSelectedColorChangedL
                , SliderFunctionsHSL.GetNewColorL, SliderFunctionsHSL.GetPaintL)
            };

            if(ShowAlphaSlider)
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
