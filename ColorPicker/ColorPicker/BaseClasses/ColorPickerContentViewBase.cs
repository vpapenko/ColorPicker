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
            BindingContextChanged += ColorPickerContentViewBase_BindingContextChanged;
            SelectedColorChanged(SelectedColor);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
           nameof(SelectedColor),
           typeof(Color),
           typeof(IColorPicker),
           Color.Black);

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
            if (BindingContext != null)
            {
                if (BindingContext is IColorPicker)
                {
                    ((IColorPicker)BindingContext).SelectedColor = color;
                }
            }
        }

        private void ColorPickerContentViewBase_BindingContextChanged(object sender, EventArgs e)
        {
            if (((ColorPickerContentViewBase)sender).BindingContext != null)
            {
                if (((ColorPickerContentViewBase)sender).BindingContext is IColorPicker)
                {
                    ((IColorPicker)((ColorPickerContentViewBase)sender).BindingContext).PropertyChanged += BindedIColorPicker_PropertyChanged;
                }
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
