using ColorPicker.Interfaces;
using Xamarin.Forms;

namespace ColorPicker.BaseClasses
{
    public abstract class ColorPickerViewBase : Layout<View>, IColorPicker
    {
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
           nameof(SelectedColor),
           typeof(Color),
           typeof(IColorPicker),
           Color.FromHsla(0, 0, 0.5),
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleSelectedColorSet));

        static void HandleSelectedColorSet(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                ((ColorPickerViewBase)bindable).SelectedColorChanged((Color)newValue);
                if (((ColorPickerViewBase)bindable).ConnectedColorPicker != null)
                {
                    ((ColorPickerViewBase)bindable).ConnectedColorPicker.SelectedColor = (Color)newValue;
                }
            }
        }

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        public static readonly BindableProperty ConnectedColorPickerProperty = BindableProperty.Create(
           nameof(ConnectedColorPicker),
           typeof(IColorPicker),
           typeof(IColorPicker),
           null,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleConnectedColorPickerSet));

        static void HandleConnectedColorPickerSet(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null)
            {
                ((IColorPicker)oldValue).PropertyChanged -= ((ColorPickerViewBase)bindable).BindedIColorPicker_PropertyChanged;
            }
            if (newValue != null)
            {
                ((IColorPicker)newValue).PropertyChanged += ((ColorPickerViewBase)bindable).BindedIColorPicker_PropertyChanged;
                ((IColorPicker)newValue).SelectedColor = ((ColorPickerViewBase)bindable).SelectedColor;
            }
        }

        public IColorPicker ConnectedColorPicker
        {
            get
            {
                return (IColorPicker)GetValue(ConnectedColorPickerProperty);
            }
            set
            {
                SetValue(ConnectedColorPickerProperty, value);
            }
        }

        protected abstract void SelectedColorChanged(Color color);

        private void BindedIColorPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedColor))
            {
                SelectedColor = ((IColorPicker)sender).SelectedColor;
            }
        }
    }
}
