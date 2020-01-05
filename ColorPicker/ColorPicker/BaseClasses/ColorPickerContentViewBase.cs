using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class ColorPickerContentViewBase : ContentView, IColorPicker
    {
        public ColorPickerContentViewBase()
        {
            SelectedColorChanged(SelectedColor);
        }
        
        public static readonly BindableProperty ConnectedColorPickerProperty = BindableProperty.Create(
           nameof(ConnectedColorPicker),
           typeof(IColorPicker),
           typeof(IColorPicker),
           null,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleConnectedColorPickerSet));

        public IColorPicker ConnectedColorPicker
        {
            get
            {
                return (IColorPicker)GetValue(ConnectedColorPickerProperty);
            }
            set
            {
                IColorPicker current = (IColorPicker)GetValue(ConnectedColorPickerProperty);
                SetConnectedColorPicker(current, value);
            }
        }

        static void HandleConnectedColorPickerSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorPickerSkiaSharpBase)bindable).SetConnectedColorPicker((IColorPicker)oldValue, (IColorPicker)newValue);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
           nameof(SelectedColor),
           typeof(Color),
           typeof(IColorPicker),
           Color.Gray,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleSelectedColorSet));

        static void HandleSelectedColorSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorPickerSkiaSharpBase)bindable).SetSelectedColor((Color)newValue);
            ((ColorPickerSkiaSharpBase)bindable).SelectedColorChanged((Color)newValue);
        }

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }
            set
            {
                Color current = (Color)GetValue(SelectedColorProperty);
                if (value != current)
                {
                    SetSelectedColor(value);
                    SelectedColorChanged(value);
                }
            }
        }

        protected abstract void SelectedColorChanged(Color color);

        protected void SetSelectedColor(Color color)
        {
            SetValue(SelectedColorProperty, color);
            if (ConnectedColorPicker != null)
            {
                ConnectedColorPicker.SelectedColor = color;
            }
        }

        private void BindedIColorPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedColor))
            {
                SelectedColor = ((IColorPicker)sender).SelectedColor;
            }
        }

        private void SetConnectedColorPicker(IColorPicker oldValue, IColorPicker newValue)
        {
            if (newValue != oldValue)
            {
                if (oldValue != null)
                {
                    oldValue.PropertyChanged -= BindedIColorPicker_PropertyChanged;
                }
                if (newValue != null)
                {
                    newValue.PropertyChanged += BindedIColorPicker_PropertyChanged;
                    newValue.SelectedColor = SelectedColor;
                }
                SetValue(ConnectedColorPickerProperty, newValue);
            }
        }
    }
}
