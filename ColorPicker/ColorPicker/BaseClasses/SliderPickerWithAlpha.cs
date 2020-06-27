using Xamarin.Forms;

namespace ColorPicker.BaseClasses
{
    public abstract class SliderPickerWithAlpha : SliderPicker
    {
        public static readonly BindableProperty ShowAlphaSliderProperty = BindableProperty.Create(
           nameof(ShowAlphaSlider),
           typeof(bool),
           typeof(SliderPickerWithAlpha),
           true,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleShowLuminositySet));

        static void HandleShowLuminositySet(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
            {
                ((SliderPickerWithAlpha)bindable).UpdateSliders();
            }
        }

        public bool ShowAlphaSlider
        {
            get
            {
                return (bool)GetValue(ShowAlphaSliderProperty);
            }
            set
            {
                SetValue(ShowAlphaSliderProperty, value);
            }
        }
    }
}
