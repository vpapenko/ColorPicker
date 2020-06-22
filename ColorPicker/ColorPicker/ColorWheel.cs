using System;
using Xamarin.Forms;

using ColorPicker.BaseClasses;
using ColorPicker.Interfaces;

namespace ColorPicker
{
    public class ColorWheel : ColorPickerViewBase
    {
        private readonly ColorCircle colorCircle = new ColorCircle();
        private readonly AlphaSlider alphaSlider = new AlphaSlider();
        private readonly LuminositySlider luminositySlider = new LuminositySlider();

        protected const double LuminositySliderRowHeight = 12;
        protected const double AlphaSliderRowHeight = 12;

        public ColorWheel()
        {
            colorCircle.ConnectedColorPicker = this;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            Children.Add(colorCircle);

            UpdateAlphaSlider(ShowAlphaSlider);

            UpdateLuminositySlider(ShowLuminositySlider);
        }

        public static readonly BindableProperty ShowLuminosityWheelProperty = BindableProperty.Create(
           nameof(ShowLuminosityWheel),
           typeof(bool),
           typeof(ColorWheel),
           true,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleShowLuminositySet));

        static void HandleShowLuminositySet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorWheel)bindable).colorCircle.ShowLuminosityWheel = (bool)newValue;
        }

        public bool ShowLuminosityWheel
        {
            get
            {
                return (bool)GetValue(ShowLuminosityWheelProperty);
            }
            set
            {
                SetValue(ShowLuminosityWheelProperty, value);
            }
        }

        public static readonly BindableProperty ShowLuminositySliderProperty = BindableProperty.Create(
           nameof(ShowLuminositySlider),
           typeof(bool),
           typeof(ColorWheel),
           false,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleShowLuminositySliderSet));

        static void HandleShowLuminositySliderSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorWheel)bindable).UpdateLuminositySlider((bool)newValue);
        }

        public bool ShowLuminositySlider
        {
            get
            {
                return (bool)GetValue(ShowLuminositySliderProperty);
            }
            set
            {
                SetValue(ShowLuminositySliderProperty, value);
            }
        }

        public static readonly BindableProperty ShowAlphaSliderProperty = BindableProperty.Create(
           nameof(ShowAlphaSlider),
           typeof(bool),
           typeof(ColorWheel),
           false,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleShowAlphaSliderSet));

        static void HandleShowAlphaSliderSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorWheel)bindable).UpdateAlphaSlider((bool)newValue);
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

        protected override void SelectedColorChanged(Color color)
        {
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (Double.IsPositiveInfinity(widthConstraint) &&
                Double.IsPositiveInfinity(heightConstraint))
            {
                throw new InvalidOperationException(
                     "AspectRatioLayout cannot be used with both dimensions unconstrained.");
            }

            double aspectRatio = 1;
            if (ShowAlphaSlider)
            {
                aspectRatio -= 0.1;
            }
            if (ShowLuminositySlider)
            {
                aspectRatio -= 0.1;
            }
            double minWidth = Math.Min(widthConstraint, aspectRatio * heightConstraint);
            double minHeight = minWidth / aspectRatio;

            return new SizeRequest(new Size(minWidth, minHeight));
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var bottom = y;
            colorCircle.Layout(new Rectangle(x, bottom, width, width));
            bottom += width;

            var sliderHeight = colorCircle.GetPickerRadiusPixels(new SkiaSharp.SKSize((float)width, (float)height)) * 2.4F;
            if (ShowLuminositySlider)
            {
                luminositySlider.Layout(new Rectangle(x, bottom, width, sliderHeight));
                bottom += sliderHeight;
            }
            if (ShowAlphaSlider)
            {
                alphaSlider.Layout(new Rectangle(x, bottom, width, sliderHeight));
            }
        }

        private void BindedIColorPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedColor))
            {
                SelectedColor = ((IColorPicker)sender).SelectedColor;
            }
        }

        private void UpdateAlphaSlider(bool show)
        {
            if (show)
            {
                alphaSlider.ConnectedColorPicker = this;
                Children.Add(alphaSlider);
            }
            else
            {
                alphaSlider.ConnectedColorPicker = null;
                Children.Remove(alphaSlider);
            }
        }

        private void UpdateLuminositySlider(bool show)
        {
            if (show)
            {
                luminositySlider.ConnectedColorPicker = this;
                Children.Add(luminositySlider);
            }
            else
            {
                luminositySlider.ConnectedColorPicker = null;
                Children.Remove(luminositySlider);
            }
        }
    }
}