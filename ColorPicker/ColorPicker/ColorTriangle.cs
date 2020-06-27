using ColorMine.ColorSpaces;
using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

using ColorPicker.BaseClasses;
using ColorPicker.Classes;
using ColorPicker.Effects;
using ColorPicker.Interfaces;

namespace ColorPicker
{
    public class ColorTriangle : SkiaSharpPickerBase
    {
        private double lastHue = 0;
        private bool zeroSL = false;
        private long? locationSVProgressId = null;
        private long? locationHProgressId = null;
        private SKPoint locationSV = new SKPoint();
        private SKPoint locationH1 = new SKPoint();
        private SKPoint locationH2 = new SKPoint();
        private readonly SKColor[] sweepGradientColors = new SKColor[256];

        public ColorTriangle() : base()
        {
            PickerRadiusScale = 0.035F;
            for (int i = 128; i >= -127; i--)
            {
                sweepGradientColors[255 - (i + 127)] = Color.FromHsla((i < 0 ? 255 + i : i) / 255D, 1, 0.5).ToSKColor();
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
                ((ColorTriangle)bindable).InvalidateSurface();
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

        public static readonly BindableProperty RotateTriangleByHueProperty = BindableProperty.Create(
           nameof(RotateTriangleByHue),
           typeof(bool),
           typeof(ColorTriangle),
           true,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleRotateTriangleByHueSet));

        static void HandleRotateTriangleByHueSet(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
            {
                ((ColorTriangle)bindable).InvalidateSurface();
            }
        }

        public bool RotateTriangleByHue
        {
            get
            {
                return (bool)GetValue(RotateTriangleByHueProperty);
            }
            set
            {
                SetValue(RotateTriangleByHueProperty, value);
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
            if (locationSVProgressId == null && IsInSVArea(point, canvasRadius))
            {
                locationSVProgressId = args.Id;
                locationSV = LimitToSVTriangle(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
            else if (locationHProgressId == null && IsInHArea(point, canvasRadius))
            {
                locationHProgressId = args.Id;
                LimitToHRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            var canvasRadius = GetCanvasSize().Width / 2F;
            SKPoint point = ConvertToPixel(args.Location);
            if (locationSVProgressId == args.Id)
            {
                locationSV = LimitToSVTriangle(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
            else if (locationHProgressId == args.Id)
            {
                LimitToHRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            var canvasRadius = GetCanvasSize().Width / 2F;
            SKPoint point = ConvertToPixel(args.Location);
            if (locationSVProgressId == args.Id)
            {
                locationSVProgressId = null;
                locationSV = LimitToSVTriangle(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
            else if (locationHProgressId == args.Id)
            {
                locationHProgressId = null;
                LimitToHRadius(point, canvasRadius);
                UpdateColors(canvasRadius);
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            if (locationSVProgressId == args.Id)
            {
                locationSVProgressId = null;
            }
            else if (locationHProgressId == args.Id)
            {
                locationHProgressId = null;
            }
        }

        protected override void OnPaintSurface(SKCanvas canvas, int width, int height)
        {
            var canvasRadius = GetSize() / 2F;
            UpdateLocations(SelectedColor, canvasRadius);
            canvas.Clear();
            PaintBackground(canvas, canvasRadius);

            PaintHGradient(canvas, canvasRadius);
            PaintLinePicker(canvas);

            PaintSVTriangle(canvas, canvasRadius);
            PaintPicker(canvas, locationSV);
        }

        protected override void SelectedColorChanged(Color color)
        {
            if (color.Saturation > 0.00390625D)
            {
                lastHue = color.Hue;
                zeroSL = false;
            }
            else
            {
                zeroSL = true;
            }
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
            ColorToHSV(color, out _, out double saturation, out double value);

            var luminosityX = -(float)(2 * triangleSide * saturation - triangleSide);
            var luminosityY = triangleHeight;
            var tmp = ToPolar(new SKPoint(luminosityX, luminosityY));
            tmp.Radius *= (float)value;
            locationSV = FromPolar(tmp);
            locationSV.X = -locationSV.X;
            locationSV.Y -= 1;
            locationSV.X *= WheelSVRadius(canvasRadius);
            locationSV.Y *= WheelSVRadius(canvasRadius);

            var tmp4 = ToPolar(new SKPoint(locationSV.X, locationSV.Y));
            tmp4.Angle -= (float)(2 * Math.PI / 3);
            locationSV = FromPolar(tmp4);

            locationSV.X += canvasRadius;
            locationSV.Y += canvasRadius;

            if (RotateTriangleByHue)
            {
                SKMatrix rotationHue = SKMatrix.MakeRotation((float)(-2D * Math.PI * lastHue - Math.PI / 2D), canvasRadius, canvasRadius);
                locationSV = rotationHue.MapPoint(locationSV);
            }

            var angleH = lastHue * Math.PI * 2;

            locationH1 = FromPolar(new PolarPoint(WheelHRadius(canvasRadius) + GetPickerRadiusPixels(), (float)(Math.PI - angleH)));
            locationH1.X += canvasRadius;
            locationH1.Y += canvasRadius;

            locationH2 = FromPolar(new PolarPoint(WheelHRadius(canvasRadius) - GetPickerRadiusPixels(), (float)(Math.PI - angleH)));
            locationH2.X += canvasRadius;
            locationH2.Y += canvasRadius;
        }

        private void UpdateColors(float canvasRadius)
        {
            var wheelSVPoint = ToWheelSVCoordinates(locationSV, canvasRadius);
            var wheelHPoint = ToWheelHCoordinates(locationH1, canvasRadius);
            var newColor = WheelPointToColor(wheelSVPoint, wheelHPoint);
            if (zeroSL && (newColor.Saturation > 0))
            {
                newColor = Color.FromHsla(lastHue, newColor.Saturation, newColor.Luminosity, newColor.A);
            }
            SelectedColor = newColor;
        }

        private bool IsInSVArea(SKPoint point, float canvasRadius)
        {
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            return polar.Radius <= WheelSVRadius(canvasRadius);
        }

        private bool IsInHArea(SKPoint point, float canvasRadius)
        {
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            return polar.Radius <= WheelHRadius(canvasRadius) + GetPickerRadiusPixels() && polar.Radius >= WheelHRadius(canvasRadius) - GetPickerRadiusPixels();
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

        private void PaintHGradient(SKCanvas canvas, float canvasRadius)
        {
            SKPoint center = new SKPoint(canvasRadius, canvasRadius);

            var shader = SKShader.CreateSweepGradient(center, sweepGradientColors, null);

            var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = shader,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = GetPickerRadiusPixels() * 2
            };
            canvas.DrawCircle(center, WheelHRadius(canvasRadius), paint);
        }

        private void PaintSVTriangle(SKCanvas canvas, float canvasRadius)
        {
            canvas.Save();

            SKMatrix rotationHue = SKMatrix.MakeRotation((float)(-2D * Math.PI * lastHue - Math.PI / 2D), canvasRadius, canvasRadius);

            if (RotateTriangleByHue)
            {
                canvas.SetMatrix(rotationHue);
            }

            var point1 = new SKPoint(canvasRadius, canvasRadius - WheelSVRadius(canvasRadius));
            var point2 = new SKPoint(canvasRadius + triangleSide * WheelSVRadius(canvasRadius), canvasRadius + triangleVerticalOffset * WheelSVRadius(canvasRadius));
            var point3 = new SKPoint(canvasRadius - triangleSide * WheelSVRadius(canvasRadius), canvasRadius + triangleVerticalOffset * WheelSVRadius(canvasRadius));
            using (SKPath pathTriangle = new SKPath())
            {
                pathTriangle.MoveTo(point1);
                pathTriangle.LineTo(point2);
                pathTriangle.LineTo(point3);
                canvas.ClipPath(pathTriangle, SKClipOperation.Intersect, true);
            }

            SKMatrix matrix = SKMatrix.MakeRotation(-(float)Math.PI / 3F, point3.X, point3.Y);

            if (RotateTriangleByHue)
            {
                SKMatrix.Concat(ref matrix, rotationHue, matrix);
            }

            var shader = SKShader.CreateSweepGradient(point3, new SKColor[] { Color.FromHsla(lastHue, 1, 0.5).ToSKColor()
                , Color.White.ToSKColor(),Color.FromHsla(lastHue, 1, 0.5).ToSKColor() }
            , new float[] { 0F, 0.16666666666666F, 1F });

            var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = shader,
                Style = SKPaintStyle.Fill
            };
            canvas.SetMatrix(matrix);
            canvas.DrawCircle(point3, WheelSVRadius(canvasRadius) * 2, paint);

            if (RotateTriangleByHue)
            {
                canvas.SetMatrix(rotationHue);
            }
            else
            {
                canvas.ResetMatrix();
            }

            var colors = new SKColor[] {
                SKColors.Black,
                SKColors.Transparent
            };
            PaintGradient(canvas, canvasRadius, colors, point3);

            canvas.ResetMatrix();
            canvas.Restore();
        }

        private void PaintGradient(SKCanvas canvas, float canvasRadius, SKColor[] colors, SKPoint centerGradient)
        {
            SKPoint center = new SKPoint(canvasRadius, canvasRadius);

            var polar = ToPolar(new SKPoint(center.X - centerGradient.X, center.Y - centerGradient.Y));
            polar.Radius *= triangleHeight;
            var p2 = FromPolar(polar);
            p2.X += centerGradient.X;
            p2.Y += centerGradient.Y;

            var shader = SKShader.CreateLinearGradient(centerGradient, p2, colors, null, SKShaderTileMode.Clamp);

            var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = shader,
                Style = SKPaintStyle.Fill
            };

            canvas.DrawCircle(center, WheelSVRadius(canvasRadius), paint);
        }

        private SKPoint ToWheelSVCoordinates(SKPoint point, float canvasRadius)
        {
            var result = new SKPoint(point.X, point.Y);
            result.X -= canvasRadius;
            result.Y -= canvasRadius;
            result.X /= WheelSVRadius(canvasRadius);
            result.Y /= WheelSVRadius(canvasRadius);
            return result;
        }

        private SKPoint ToWheelHCoordinates(SKPoint point, float canvasRadius)
        {
            var result = new SKPoint(point.X, point.Y);
            result.X -= canvasRadius;
            result.Y -= canvasRadius;
            result.X /= WheelHRadius(canvasRadius);
            result.Y /= WheelHRadius(canvasRadius);
            return result;
        }

        private const float triangleHeight = 1.5000001F;
        private const float triangleSide = 0.8660244F;
        private const float triangleVerticalOffset = 0.5000001F;

        private Color WheelPointToColor(SKPoint pointSV, SKPoint pointH)
        {
            if (RotateTriangleByHue)
            {
                SKMatrix rotationHue = SKMatrix.MakeRotation(-(float)(-2D * Math.PI * lastHue - Math.PI / 2D));
                pointSV = rotationHue.MapPoint(pointSV);
            }

            var polarH = ToPolar(pointH);
            var h = (-polarH.Angle + Math.PI) / (2 * Math.PI);

            pointSV.Y = -pointSV.Y + triangleVerticalOffset;
            pointSV.X += triangleSide;

            var x1 = triangleSide;
            var y1 = triangleHeight;
            var x2 = x1 * 2;
            var y2 = 0F;

            var vCurrent = (pointSV.X * (y2 - y1) - pointSV.Y * (x2 - x1) + x2 * y1 - y2 * x1) / Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
            var v = (y1 - vCurrent) / y1;

            var sMax = x2 - vCurrent / Math.Sin(Math.PI / 3);
            var sCurrent = pointSV.Y / Math.Sin(Math.PI / 3);
            var s = sCurrent / sMax;

            lastHue = h;
            var result = ColorFromHSV(h, s, v, (int)SelectedColor.A);

            return result;
        }

        private SKPoint LimitToSVTriangle(SKPoint point, float canvasRadius)
        {
            var polar = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            polar.Radius = polar.Radius < WheelSVRadius(canvasRadius) ? polar.Radius : WheelSVRadius(canvasRadius);
            var result = FromPolar(polar);
            result.X += canvasRadius;
            result.Y += canvasRadius;
            return result;
        }

        private void LimitToHRadius(SKPoint point, float canvasRadius)
        {
            var point1 = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            point1.Radius = WheelHRadius(canvasRadius) + GetPickerRadiusPixels();

            var point2 = ToPolar(new SKPoint(point.X - canvasRadius, point.Y - canvasRadius));
            point1.Radius = WheelHRadius(canvasRadius) - GetPickerRadiusPixels();

            locationH1 = FromPolar(point1);
            locationH2 = FromPolar(point2);

            locationH1.X += canvasRadius;
            locationH1.Y += canvasRadius;
            locationH2.X += canvasRadius;
            locationH2.Y += canvasRadius;
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

        private float WheelSVRadius(float canvasRadius)
        {
            return canvasRadius - 2 * GetPickerRadiusPixels() - 2;
        }

        private float WheelHRadius(float canvasRadius)
        {
            return canvasRadius - GetPickerRadiusPixels();
        }

        private void PaintLinePicker(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            paint.Color = Color.Black.ToSKColor();
            paint.StrokeWidth = 4;
            using (SKPath pathTriangle = new SKPath())
            {
                pathTriangle.MoveTo(locationH1);
                pathTriangle.LineTo(locationH2);
                canvas.DrawPath(pathTriangle, paint);
            }
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            var rgb = new Rgb { R = Math.Round(color.R * 255F), G = Math.Round(color.G * 255F), B = Math.Round(color.B * 255F) };
            var hsv = rgb.To<Hsv>();
            hue = color.Hue;
            saturation = hsv.S;
            value = hsv.V;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value, int a)
        {
            var result = Color.FromHsv(hue, saturation, value);
            return new Color(result.R, result.G, result.B, a);
        }
    }
}
