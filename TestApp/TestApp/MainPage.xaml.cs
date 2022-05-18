using ColorPicker.BaseCore;
using ColorPicker.BaseCore.ColorTriangle;
using ColorPicker.BaseCore.ColorWheel;
using ColorPicker.BaseCore.Slider;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace TestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ColoPickerBase _coloPicker;
        public MainPage()
        {
            InitializeComponent();
            _coloPicker = new RotatingColorTriangle();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateLocation();
        }

        private void VSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateLocation();
        }

        private void UpdateLocation()
        {
            var p = new AbstractPoint((float)HSlider.Value, -(float)VSlider.Value);
            var p1 = _coloPicker.FitToActiveAria(p, ColorWheel1.SelectedColor);
            var x = AbsoluteLayout.Width * (p1.X / 2 + 0.25);
            var y = AbsoluteLayout.Width * (p1.Y / 2 + 0.25);
            var rect1 = new Rectangle(x, y, 3, 3);
            BoxView1.Layout(rect1);
            LabelXY.Text = $"CP X: {p1.Y}; Y: {p1.Y}";
            LabelSlideXY.Text = $"SL X: {HSlider.Value}; Y: {-VSlider.Value}";
            Label1.Text = $"IsInActiveAria: {_coloPicker.IsInActiveAria(new AbstractPoint((float)HSlider.Value, -(float)VSlider.Value), ColorWheel1.SelectedColor)}";
            var color = _coloPicker.UpdateColor(p, ColorWheel1.SelectedColor);
            ColorWheel1.SelectedColor = color;
        }

        private void ColorWheel1_SelectedColorChanged(object sender, ColorPicker.BaseClasses.ColorPickerEventArgs.ColorChangedEventArgs e)
        {
            var p = _coloPicker.ColorToPoint(e.NewColor);
            var x = AbsoluteLayout.Width * (p.X / 2 + 0.25);
            var y = AbsoluteLayout.Width * (p.Y / 2 + 0.25);
            var rect2 = new Rectangle(x - 3, y - 3, 3, 3);
            BoxView2.Layout(rect2);
        }
    }
}
