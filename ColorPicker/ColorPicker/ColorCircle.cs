using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

using ColorPicker.BaseClasses;
using ColorPicker.Classes;
using ColorPicker.Effects;
using ColorPicker.Interfaces;

namespace ColorPicker
{
    public class ColorCircle : SkiaSharpPickerBase
    {
        private long? locationHSProgressId = null;
        private long? locationLProgressId = null;
        private SKPoint locationHS = new SKPoint();
        private SKPoint locationL = new SKPoint();
        private readonly SKColor[] sweepGradientColors = new SKColor[256];

        public ColorCircle() : base()
        {
            for (int i = 128; i >= -127; i--)
            {
                sweepGradientColors[255 - (i + 127)] = Color.FromHsla((i < 0 ? 255 + i : i) / 255D, 1, 0.5).ToSKColor();
            }
        }

        public static readonly BindableProperty ShowLuminosityWheelProperty = BindableProperty.Create(
           nameof(ShowLuminosityWheel),
           typeof(bool),
           typeof(SkiaSharpPickerBase),
           true,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleShowLuminositySet));

        static void HandleShowLuminositySet(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
            {
                ((ColorCircle)bindable).InvalidateSurface();
            }
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

        public static readonly BindableProperty WheelBackgroundColorProperty = BindableProperty.Create(
           nameof(WheelBackgroundColor),
           typeof(Color),
           typeof(IColorPicker),
           Color.Transparent,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleWheelBackgroundColorSet));

        static void HandleWheelBackgroundColorSet(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
            {
                ((ColorCircle)bindable).InvalidateSurface();
            }
        }

        public Color WheelBackgroundColor
        {
            get
            {
                return (Color)GetValue(WheelBackgroundColorProperty);
            }
            set
            {
                SetValue(WheelBackgroundColorProperty, value);
            }
        }

        public override float GetPickerRadiusPixels()
        {
            return GetPickerRadiusPixels(GetCanvasSize());
        }

        public override float GetPickerRadiusPixels(SKSize canvasSize)
        {
            return GetSize(canvasSize) * PickerRadiusScale;
        }

        protected override void OnTouchActionPressed(ColorPickerTouchActionEventArgs args)
        {
            var canvasRadius = GetCanvasSize().Width / 2F;
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHSProgressId == null && IsInHSArea(point, canvasRadius))
            {
                locationHSProgressId = args.Id;
                locationHS = LimitToHSRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
            else if (locationLProgressId == null && IsInLArea(point, canvasRadius))
            {
                locationLProgressId = args.Id;
                locationL = LimitToLRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            var canvasRadius = GetCanvasSize().Width / 2F;
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHSProgressId == args.Id)
            {
                locationHS = LimitToHSRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
            else if (locationLProgressId == args.Id)
            {
                locationL = LimitToLRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            var canvasRadius = GetCanvasSize().Width / 2F;
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHSProgressId == args.Id)
            {
                locationHSProgressId = null;
                locationHS = LimitToHSRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
            else if (locationLProgressId == args.Id)
            {
                locationLProgressId = null;
                locationL = LimitToLRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
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

        protected override void OnPaintSurface(SKCanvas canvas, int width, int height)
        {
            var canvasRadius = GetSize() / 2F;
            UpdateLocations(SelectedColor, canvasRadius);
            canvas.Clear();
            PaintBackground(canvas, canvasRadius);
            if (ShowLuminosityWheel)
            {
                PaintLGradient(canvas, canvasRadius);
                PaintPicker(canvas, locationL);
            }
            PaintColorSweepGradient(canvas, canvasRadius);
            PaintGrayRadialGradient(canvas, canvasRadius);
            PaintPicker(canvas, locationHS);
        }

        protected override void SelectedColorChanged(Color color)
        {
            InvalidateSurface();
        }

        protected override SizeRequest GetMeasure(double widthConstraint, double heightConstraint)
        {
            if (Double.IsPositiveInfinity(widthConstraint) &&
                Double.IsPositiveInfinity(heightConstraint))
            {
                throw new InvalidOperationException(
                     "AspectRatioLayout cannot be used with both dimensions unconstrained.");
            }

            double size = Math.Min(widthConstraint, heightConstraint);

            return new SizeRequest(new Size(size, size));
        }

        protected override float GetSize()
        {
            return GetSize(GetCanvasSize());
        }

        protected override float GetSize(SKSize canvasSize)
        {
            return canvasSize.Width;
        }

        private void UpdateLocations(Color color, float canvasRadius)
        {
            if (color.Luminosity != 0 || !IsInHSArea(locationHS, canvasRadius))
            {
                var angleHS = (0.5 - color.Hue) * (2 * Math.PI);
                var radiusHS = WheelHSRadius(canvasRadius) * color.Saturation;

                var resultHS = FromPolar(new PolarPoint((float)radiusHS, (float)angleHS));
                resultHS.X += canvasRadius;
                resultHS.Y += canvasRadius;
                locationHS = resultHS;
            }

            var polarL = ToPolar(ToWheelLCoordinates(locationL, canvasRadius));
            polarL.Angle -= (float)Math.PI / 2F;
            var signOld = polarL.Angle <= 0 ? 1 : -1;
            var angleL = color.Luminosity * Math.PI * signOld;

            var resultL = FromPolar(new PolarPoint(WheelLRadius(canvasRadius), (float)(angleL - Math.PI / 2)));
            resultL.X += canvasRadius;
            resultL.Y += canvasRadius;
            locationL = resultL;
        }

        private void UpdateColors(float canvasRadius)
        {
            var wheelHSPoint = ToWheelHSCoordinates(locationHS, canvasRadius);
            var wheelLPoint = ToWheelLCoordinates(locationL, canvasRadius);
            var newColor = WheelPointToColor(wheelHSPoint, wheelLPoint);
            SelectedColor = newColor;
        }

        private bool IsInHSArea(SKPoint point, float canvasRadius)
        {
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            return polar.Radius <= WheelHSRadius(canvasRadius);
        }

        private bool IsInLArea(SKPoint point, float canvasRadius)
        {
            if (!ShowLuminosityWheel)
            {
                return false;
            }
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            return polar.Radius <= WheelLRadius(canvasRadius) + GetPickerRadiusPixels() / 2F && polar.Radius >= WheelLRadius(canvasRadius) - GetPickerRadiusPixels() / 2F;
        }

        private void PaintBackground(SKCanvas canvas, float canvasRadius)
        {
            SKPoint center = new SKPoint(canvasRadius, canvasRadius);

            var paint = new SKPaint
            {
                IsAntialias = true,
                Color = WheelBackgroundColor.ToSKColor()
            };

            canvas.DrawCircle(center, canvasRadius, paint);
        }

        private void PaintLGradient(SKCanvas canvas, float canvasRadius)
        {
            SKPoint center = new SKPoint(canvasRadius, canvasRadius);

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
                IsAntialias = true,
                Shader = shader,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = GetPickerRadiusPixels()
            };
            canvas.DrawCircle(center, WheelLRadius(canvasRadius), paint);
        }

        private void PaintColorSweepGradient(SKCanvas canvas, float canvasRadius)
        {
            SKPoint center = new SKPoint(canvasRadius, canvasRadius);

            var shader = SKShader.CreateSweepGradient(center, sweepGradientColors, null);

            var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = shader,
                Style = SKPaintStyle.Fill
            };
            canvas.DrawCircle(center, WheelHSRadius(canvasRadius), paint);
        }

        private void PaintGrayRadialGradient(SKCanvas canvas, float canvasRadius)
        {
            SKPoint center = new SKPoint(canvasRadius, canvasRadius);

            var colors = new SKColor[] {
                SKColors.Gray,
                SKColors.Transparent
            };

            var shader = SKShader.CreateRadialGradient(center, WheelHSRadius(canvasRadius), colors, null, SKShaderTileMode.Clamp);

            var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = shader,
                Style = SKPaintStyle.Fill
            };
            canvas.DrawPaint(paint);
        }

        private SKPoint ToWheelHSCoordinates(SKPoint point, float canvasRadius)
        {
            var result = new SKPoint(point.X, point.Y);
            result.X -= canvasRadius;
            result.Y -= canvasRadius;
            result.X /= WheelHSRadius(canvasRadius);
            result.Y /= WheelHSRadius(canvasRadius);
            return result;
        }

        private SKPoint ToWheelLCoordinates(SKPoint point, float canvasRadius)
        {
            var result = new SKPoint(point.X, point.Y);
            result.X -= canvasRadius;
            result.Y -= canvasRadius;
            result.X /= WheelLRadius(canvasRadius);
            result.Y /= WheelLRadius(canvasRadius);
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

        private SKPoint LimitToHSRadius(SKPoint point, float canvasRadius)
        {
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            polar.Radius = polar.Radius < WheelHSRadius(canvasRadius) ? polar.Radius : WheelHSRadius(canvasRadius);
            var result = FromPolar(polar);
            result.X += canvasRadius;
            result.Y += canvasRadius;
            return result;
        }

        private SKPoint LimitToLRadius(SKPoint point, float canvasRadius)
        {
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            polar.Radius = WheelLRadius(canvasRadius);
            var result = FromPolar(polar);
            result.X += canvasRadius;
            result.Y += canvasRadius;
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

        private float WheelHSRadius(float canvasRadius)
        {
            return !ShowLuminosityWheel ? canvasRadius - GetPickerRadiusPixels() : canvasRadius - 3 * GetPickerRadiusPixels() - 2;
        }

        private float WheelLRadius(float canvasRadius)
        {
            return canvasRadius - GetPickerRadiusPixels();
        }
    }
}
