using Xamarin.Forms;

namespace ColorPicker.Effects
{
    public class ColorPickerTouchEffect : RoutingEffect
    {
        public event ColorPickerTouchActionEventHandler TouchAction;

        public ColorPickerTouchEffect() : base("ColorPickerPlatformEffect.ColorPickerTouchEffect")
        {
        }

        public bool Capture { set; get; }

        public void OnTouchAction(Element element, ColorPickerTouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}
