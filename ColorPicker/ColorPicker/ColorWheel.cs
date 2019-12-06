using ColorPicker.Forms;
using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ColorPicker
{
    public class ColorWheel : ColorPickerSkiaSharpBase
    {
        private long? locationHSProgressId = null;
        private long? locationLProgressId = null;
        private SKPoint locationHS = new SKPoint();
        private SKPoint locationL = new SKPoint();
        private float CanvasRadius { get => CanvasView.CanvasSize.Width / 2F; }
        private float WheelHSRadius { get => CanvasRadius - 3 * PickerRadiusPixels - 2; }
        private float WheelLRadius { get => CanvasRadius - PickerRadiusPixels; }
        private readonly object  _alphaSlider = new AlphaSlider() { IsVisible = false, BackgroundColor = Color.Aqua };
        private readonly LuminositySlider _luminositySlider = new LuminositySlider() { IsVisible = false, BackgroundColor = Color.BurlyWood };
        protected const double LuminositySliderRowHeight = 10;
        protected const double AlphaSliderRowHeight = 10;

        private RowDefinition _alphaSliderRowDefinition = new RowDefinition { Height = new GridLength(AlphaSliderRowHeight, GridUnitType.Star) };
        private RowDefinition _luminositySliderRowDefinition = new RowDefinition { Height = new GridLength(LuminositySliderRowHeight, GridUnitType.Star) };

        public static readonly BindableProperty WheelBackgroundColorProperty = BindableProperty.Create(
           nameof(WheelBackgroundColor),
           typeof(Color),
           typeof(IColorPicker),
           Color.Transparent);

        public Color WheelBackgroundColor
        {
            get
            {
                return (Color)GetValue(WheelBackgroundColorProperty);
            }
            set
            {
                Color current = (Color)GetValue(WheelBackgroundColorProperty);
                if (value != current)
                {
                    SetValue(WheelBackgroundColorProperty, value);
                    CanvasView.InvalidateSurface();
                }
            }
        }

        protected override void UpdateSliders()
        {
            _alphaSlider.IsVisible = ShowAlphaSlider;
            _luminositySlider.IsVisible = ShowAlphaSlider;
            if (ShowAlphaSlider)
            {
                mainGrid.RowDefinitions.Add(_alphaSliderRowDefinition);
                mainGrid.RowDefinitions.Add(_luminositySliderRowDefinition);
                mainGrid.Children.Add(_luminositySlider, 0, 1);
                mainGrid.Children.Add(_alphaSlider, 0, 2);
                _luminositySlider.SelectedColor = SelectedColor;
                _alphaSlider.SelectedColor = SelectedColor;
                _luminositySlider.SetBinding(ConnectedColorPickerProperty, new Binding() { Source = this });
                _alphaSlider.SetBinding(ConnectedColorPickerProperty, new Binding() { Source = this });
                //SetBinding(ConnectedColorPickerProperty, new Binding() { Source = _luminositySlider });
                //SetBinding(ConnectedColorPickerProperty, new Binding() { Source = _alphaSlider });
            }
            else
            {
                mainGrid.Children.Remove(_luminositySlider);
                mainGrid.Children.Remove(_alphaSlider);
                if (mainGrid.RowDefinitions.Contains(_alphaSliderRowDefinition))
                {
                    mainGrid.RowDefinitions.Remove(_alphaSliderRowDefinition);
                }
                if (mainGrid.RowDefinitions.Contains(_luminositySliderRowDefinition))
                {
                    mainGrid.RowDefinitions.Remove(_luminositySliderRowDefinition);
                }
                _luminositySlider.RemoveBinding(ConnectedColorPickerProperty);
                _alphaSlider.RemoveBinding(ConnectedColorPickerProperty);
            }
            var width = Width;
            var height = Height;
            SetCanvasViewSize(ref width, ref height);
            _luminositySlider.WidthRequest = width;
            _alphaSlider.WidthRequest = width;
            WidthRequest = width;
            HeightRequest = height;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            SetCanvasViewSize(ref width, ref height);
            base.OnSizeAllocated(width, height);
        }

        protected void SetCanvasViewSize(ref double width, ref double height)
        {
            var multiplier = CanvasViewRowHeight;
            if(ShowAlphaSlider)
            {
                multiplier += LuminositySliderRowHeight;
                multiplier += AlphaSliderRowHeight;
            }
            multiplier /= CanvasViewRowHeight;

            var size = width < height / multiplier ? width : height / multiplier;
            width = size;
            height = size * multiplier;
            CanvasView.WidthRequest = size;
            CanvasView.HeightRequest = size;

            if (PickerRadius == null)
            {
                PickerRadiusProtected = GetDefaultPickerRadius(size);
            }
        }

        protected override void OnTouchActionPressed(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHSProgressId == null && IsInHSArea(point))
            {
                locationHSProgressId = args.Id;
                locationHS = LimitToHSRadius(point);
                UpdateColors();
            }
            else if (locationLProgressId == null && IsInLArea(point))
            {
                locationLProgressId = args.Id;
                locationL = LimitToLRadius(point);
                UpdateColors();
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHSProgressId == args.Id)
            {
                locationHS = LimitToHSRadius(point);
                UpdateColors();
            }
            else if (locationLProgressId == args.Id)
            {
                locationL = LimitToLRadius(point);
                UpdateColors();
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHSProgressId == args.Id)
            {
                locationHSProgressId = null;
                locationHS = LimitToHSRadius(point);
                UpdateColors();
            }
            else if (locationLProgressId == args.Id)
            {
                locationLProgressId = null;
                locationL = LimitToLRadius(point);
                UpdateColors();
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            if (locationHSProgressId == args.Id)
            {
                locationHSProgressId = null;
            }
            else if (locationLProgressId == args.Id)
            {
                locationLProgressId = null;
            }
        }

        protected override void OnPaintSurface(SKCanvas canvas)
        {
            SelectedColorChanged(SelectedColor);
            canvas.Clear();
            PaintBackground(canvas);
            PaintLGradient(canvas);
            PaintColorSweepGradient(canvas);
            PaintGrayRadialGradient(canvas);
            PaintPicker(canvas, locationHS);
            PaintPicker(canvas, locationL);
        }

        protected override void SelectedColorChanged(Color color)
        {
            if (color.Luminosity != 0 || !IsInHSArea(locationHS))
            {
                var angleHS = (0.5 - color.Hue) * (2 * Math.PI);
                var radiusHS = WheelHSRadius * color.Saturation;

                var resultHS = FromPolar(new PolarPoint((float)radiusHS, (float)angleHS));
                resultHS.X += CanvasRadius;
                resultHS.Y += CanvasRadius;
                locationHS = resultHS;
            }

            var polarL = ToPolar(ToWheelLCoordinates(locationL));
            polarL.Angle -= (float)Math.PI / 2F;
            var signOld = polarL.Angle <= 0 ? 1 : -1;
            var angleL = color.Luminosity * Math.PI * signOld;

            var resultL = FromPolar(new PolarPoint(WheelLRadius, (float)(angleL - Math.PI / 2)));
            resultL.X += CanvasRadius;
            resultL.Y += CanvasRadius;
            locationL = resultL;

            CanvasView.InvalidateSurface();
        }

        protected override float GetDefaultPickerRadius()
        {
            return GetDefaultPickerRadius(CanvasView.Height);
        }

        private float GetDefaultPickerRadius(double canvasViewHeight)
        {
            return (float)(canvasViewHeight / 20d); 
        }

        private void UpdateColors()
        {
            var wheelHSPoint = ToWheelHSCoordinates(locationHS);
            var wheelLPoint = ToWheelLCoordinates(locationL);
            var newColor = WheelPointToColor(wheelHSPoint, wheelLPoint);
            SetSelectedColor(newColor);
            CanvasView.InvalidateSurface();
        }

        private bool IsInHSArea(SKPoint point)
        {
            var polar = ToPolar(new SKPoint(point.X - CanvasRadius, point.Y - CanvasRadius));
            return polar.Radius <= WheelHSRadius;
        }

        private bool IsInLArea(SKPoint point)
        {
            var polar = ToPolar(new SKPoint(point.X - CanvasRadius, point.Y - CanvasRadius));
            return polar.Radius <= WheelLRadius + PickerRadiusPixels / 2F && polar.Radius >= WheelLRadius - PickerRadiusPixels / 2F;
        }

        private void PaintBackground(SKCanvas canvas)
        {
            SKPoint center = new SKPoint(CanvasRadius, CanvasRadius);

            var paint = new SKPaint
            {
                Color = WheelBackgroundColor.ToSKColor()
            };

            canvas.DrawCircle(center, CanvasRadius, paint);
        }

        private void PaintLGradient(SKCanvas canvas)
        {
            SKPoint center = new SKPoint(CanvasRadius, CanvasRadius);

            var colors = new List<SKColor>()
            {
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0.5).ToSKColor(),
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 1).ToSKColor(),
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0.5).ToSKColor(),
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0).ToSKColor(),
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0.5).ToSKColor()
            };
            
            var shader = SKShader.CreateSweepGradient(center, colors.ToArray(), null);

            var paint = new SKPaint
            {
                Shader = shader,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = PickerRadiusPixels
            };
            canvas.DrawCircle(center, WheelLRadius, paint);
        }

        private void PaintColorSweepGradient(SKCanvas canvas)
        {
            SKPoint center = new SKPoint(CanvasRadius, CanvasRadius);

            var colors = new List<SKColor>();
            for (int i = 128; i >= -127; i--)
            {
                colors.Add(Color.FromHsla((i < 0 ? 255 + i : i) / 255D, 1, 0.5).ToSKColor());
            }

            var shader = SKShader.CreateSweepGradient(center, colors.ToArray(), null);

            var paint = new SKPaint
            {
                Shader = shader,
                Style = SKPaintStyle.Fill
            };
            canvas.DrawCircle(center, WheelHSRadius, paint);
        }

        private void PaintGrayRadialGradient(SKCanvas canvas)
        {
            SKPoint center = new SKPoint(CanvasRadius, CanvasRadius);

            var colors = new SKColor[] {
                SKColors.Gray,
                SKColors.Transparent
            };

            var shader = SKShader.CreateRadialGradient(center, WheelHSRadius, colors, null, SKShaderTileMode.Clamp);

            var paint = new SKPaint
            {
                Shader = shader,
                Style = SKPaintStyle.Fill
            };
            canvas.DrawPaint(paint);
        }
        
        private SKPoint ToWheelHSCoordinates(SKPoint point)
        {
            var result = new SKPoint(point.X, point.Y);
            result.X -= CanvasRadius;
            result.Y -= CanvasRadius;
            result.X /= WheelHSRadius;
            result.Y /= WheelHSRadius;
            return result;
        }

        private SKPoint ToWheelLCoordinates(SKPoint point)
        {
            var result = new SKPoint(point.X, point.Y);
            result.X -= CanvasRadius;
            result.Y -= CanvasRadius;
            result.X /= WheelLRadius;
            result.Y /= WheelLRadius;
            return result;
        }

        private Color WheelPointToColor(SKPoint pointHS, SKPoint pointL)
        {
            var polarHS = ToPolar(pointHS);
            var polarL = ToPolar(pointL);
            polarL.Angle += (float)Math.PI / 2F;
            polarL = ToPolar(FromPolar(polarL));
            var h = (Math.PI - polarHS.Angle) / (2 * Math.PI);
            var s = polarHS.Radius;
            var l = Math.Abs(polarL.Angle) / Math.PI;
            return Color.FromHsla(h, s, l, SelectedColor.A);
        }

        private SKPoint LimitToHSRadius(SKPoint point)
        {
            var polar = ToPolar(new SKPoint(point.X - CanvasRadius, point.Y - CanvasRadius));
            polar.Radius = polar.Radius < WheelHSRadius ? polar.Radius : WheelHSRadius;
            var result = FromPolar(polar);
            result.X += CanvasRadius;
            result.Y += CanvasRadius;
            return result;
        }

        private SKPoint LimitToLRadius(SKPoint point)
        {
            var polar = ToPolar(new SKPoint(point.X - CanvasRadius, point.Y - CanvasRadius));
            polar.Radius = WheelLRadius;
            var result = FromPolar(polar);
            result.X += CanvasRadius;
            result.Y += CanvasRadius;
            return result;
        }

        private PolarPoint ToPolar(SKPoint point)
        {
            float radius = (float)Math.Sqrt(point.X * point.X + point.Y * point.Y);
            float angle = (float)Math.Atan2(point.Y, point.X);
            return new PolarPoint(radius, angle);
        }

        private SKPoint FromPolar(PolarPoint point)
        {
            float x = (float)(point.Radius * Math.Cos(point.Angle));
            float y = (float)(point.Radius * Math.Sin(point.Angle));
            return new SKPoint(x, y);
        }
    }
}
