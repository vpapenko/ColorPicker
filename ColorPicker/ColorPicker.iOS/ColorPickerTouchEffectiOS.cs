using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UIKit;
using ColorPicker.Effects;
using ColorPicker.iOS.Effects;

[assembly: ResolutionGroupName("ColorPickerPlatformEffect")]
[assembly: ExportEffect(typeof(ColorPickerTouchEffectiOS), "ColorPickerTouchEffect")]

namespace ColorPicker.iOS.Effects
{
    public class ColorPickerTouchEffectiOS : PlatformEffect
    {
        UIView view;
        ColorPickerTouchRecognizeriOS touchRecognizer;

        protected override void OnAttached()
        {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            view = Control ?? Container;

            // Get access to the TouchEffect class in the .NET Standard library
            ColorPickerTouchEffect effect = (ColorPickerTouchEffect)Element.Effects.FirstOrDefault(e => e is ColorPickerTouchEffect);

            if (effect != null && view != null)
            {
                // Create a TouchRecognizer for this UIView
                touchRecognizer = new ColorPickerTouchRecognizeriOS(Element, view, effect);
                view.AddGestureRecognizer(touchRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (touchRecognizer != null)
            {
                // Clean up the TouchRecognizer object
                touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                view.RemoveGestureRecognizer(touchRecognizer);
            }
        }
    }
}