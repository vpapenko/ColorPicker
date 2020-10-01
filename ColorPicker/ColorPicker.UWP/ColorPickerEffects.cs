using ColorPicker.UWP.Effects;
using System.Reflection;

namespace ColorPicker.UWP
{
    public static class ColorPickerEffects
    {
        public static Assembly[] GetRendererAssemblies()
        {
            return new Assembly[] { typeof(ColorPickerTouchEffectUWP).GetTypeInfo().Assembly };
        }
    }
}