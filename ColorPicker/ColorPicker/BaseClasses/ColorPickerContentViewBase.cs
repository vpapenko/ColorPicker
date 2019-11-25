using System;
using System.Collections.Generic;
using System.Text;
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
           null);

        public IColorPicker ConnectedColorPicker
        {
            get
            {
                return (IColorPicker)GetValue(ConnectedColorPickerProperty);
            }
            set
            {
                IColorPicker current = (IColorPicker)GetValue(ConnectedColorPickerProperty);
                if (value != current)
                {
                    if (current != null)
                    {
                        current.PropertyChanged -= BindedIColorPicker_PropertyChanged;
                    }
                    if (value != null)
                    {
                        value.PropertyChanged += BindedIColorPicker_PropertyChanged;
                        value.SelectedColor = SelectedColor;
                    }
                    SetValue(ConnectedColorPickerProperty, value);
                }
            }
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
           nameof(SelectedColor),
           typeof(Color),
           typeof(IColorPicker),
           Color.Black,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandlePickerRadiusSet));

        static void HandlePickerRadiusSet(BindableObject bindable, object oldValue, object newValue)
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
    }
}
