using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Xamarin.Forms.Platform.iOS;

namespace ColorPicker.iOS
{
    [Preserve(AllMembers = true)]
    public static class ColorPickerEffects
    {
#pragma warning disable 414
        private static List<PlatformEffect> effects = new List<PlatformEffect>();
#pragma warning restore 414

        /// <summary>
        /// This is needed to ensure iOS loads the assembly with the effects in it
        /// </summary>
        public static void Init()
        {
            effects = new List<PlatformEffect>(typeof(ColorPickerEffects).Assembly.GetTypes().Where(t => typeof(PlatformEffect)
                .IsAssignableFrom(t)).Select(t => (PlatformEffect)Activator.CreateInstance(t)));
        }
    }
}