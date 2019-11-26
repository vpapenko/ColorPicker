using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ColorPicker
{
    public class HSLSliders : ColorPickerSkiaSharpBase
    {
        private long? locationHProgressId = null;
        private long? locationSProgressId = null;
        private long? locationLProgressId = null;
        private SKPoint locationH = new SKPoint();
        private SKPoint locationS = new SKPoint();
        private SKPoint locationL = new SKPoint();

        private float SlidersWidht { get => CanvasView.CanvasSize.Width - PickerRadiusPixels * 4; }
        private float SlidersHHeight { get => PickerRadiusPixels * 2; }
        private float SlidersSHeight { get => PickerRadiusPixels * 4.2F; }
        private float SlidersLHeight { get => PickerRadiusPixels * 6.4F; }

        protected override void OnSizeAllocated(double width, double height)
        {
            CanvasView.WidthRequest = width;
            CanvasView.HeightRequest = PickerRadius * 8.4;
            base.OnSizeAllocated(width, height);
        }

        protected override void OnPaintSurface(SKCanvas canvas)
        {
            SelectedColorChanged(SelectedColor);
            canvas.Clear();

            var startColor =  Color.FromHsla(0, SelectedColor.Saturation, SelectedColor.Luminosity).ToSKColor();
            var endColor = Color.FromHsla(1, SelectedColor.Saturation, SelectedColor.Luminosity).ToSKColor();
            PaintHSlider(canvas, SlidersHHeight, startColor, endColor);

            startColor = Color.FromHsla(SelectedColor.Hue, 0, SelectedColor.Luminosity).ToSKColor();
            endColor = Color.FromHsla(SelectedColor.Hue, 1, SelectedColor.Luminosity).ToSKColor();
            PaintSSlider(canvas, SlidersSHeight, startColor, endColor);

            startColor = Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0).ToSKColor();
            endColor = Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 1).ToSKColor();
            PaintLSlider(canvas, SlidersLHeight, startColor, endColor);

            PaintPicker(canvas, locationH);
            PaintPicker(canvas, locationS);
            PaintPicker(canvas, locationL);
        }

        private void PaintHSlider(SKCanvas canvas, float slidersHeight, SKColor startColor, SKColor endColor)
        {
            var colors = new List<SKColor>();
            var colorPos = new List<float>();
            for (int i = 0; i <= 255; i++)
            {
                colors.Add(Color.FromHsla(i / 255D, 1, 0.5).ToSKColor());
                colorPos.Add(i / 255F);
            }

            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, slidersHeight);
            SKPoint endPoint = new SKPoint(CanvasView.CanvasSize.Width - PickerRadiusPixels * 2, slidersHeight);
            SKShader shader = SKShader.CreateLinearGradient(startPoint, endPoint
                    , colors.ToArray(), colorPos.ToArray(), SKShaderTileMode.Clamp);

            PaintSlider(canvas, slidersHeight, startColor, endColor, shader);
        }

        private void PaintSSlider(SKCanvas canvas, float slidersHeight, SKColor startColor, SKColor endColor)
        {
            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, slidersHeight);
            SKPoint endPoint = new SKPoint(CanvasView.CanvasSize.Width - PickerRadiusPixels * 2, slidersHeight);
            SKShader shader = SKShader.CreateLinearGradient(startPoint, endPoint
                    , new SKColor[] { startColor, endColor }, new float[] { 0, 1 }, SKShaderTileMode.Clamp);

            PaintSlider(canvas, slidersHeight, startColor, endColor, shader);
        }

        private void PaintLSlider(SKCanvas canvas, float slidersHeight, SKColor startColor, SKColor endColor)
        {
            var colors = new List<SKColor>()
            {
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0).ToSKColor(),
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 0.5).ToSKColor(),
                Color.FromHsla(SelectedColor.Hue, SelectedColor.Saturation, 1).ToSKColor(),
            };
            
            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, slidersHeight);
            SKPoint endPoint = new SKPoint(CanvasView.CanvasSize.Width - PickerRadiusPixels * 2, slidersHeight);
            SKShader shader = SKShader.CreateLinearGradient(startPoint, endPoint
                    , colors.ToArray(), new float[] { 0, 0.5F, 1 }, SKShaderTileMode.Clamp);

            PaintSlider(canvas, slidersHeight, startColor, endColor, shader);
        }

        private void PaintSlider(SKCanvas canvas, float slidersHeight, SKColor startColor, SKColor endColor, SKShader shader)
        {
            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, slidersHeight);
            SKPoint endPoint = new SKPoint(CanvasView.CanvasSize.Width - PickerRadiusPixels * 2, slidersHeight);

            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.StrokeJoin = SKStrokeJoin.Round;
                paint.StrokeWidth = PickerRadiusPixels * 1.3F;
                paint.Shader = shader;
                canvas.DrawLine(startPoint, endPoint, paint);
            }
        }

        protected override void OnTouchActionPressed(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHProgressId == null && IsInSliderArea(point, SlidersHHeight))
            {
                locationHProgressId = args.Id;
                locationH = LimitToSliderLocation(point, SlidersHHeight);
                UpdateColors();
            }
            else if (locationSProgressId == null && IsInSliderArea(point, SlidersSHeight))
            {
                locationSProgressId = args.Id;
                locationS = LimitToSliderLocation(point, SlidersSHeight);
                UpdateColors();
            }
            else if (locationLProgressId == null && IsInSliderArea(point, SlidersLHeight))
            {
                locationLProgressId = args.Id;
                locationL = LimitToSliderLocation(point, SlidersLHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHProgressId == args.Id)
            {
                locationH = LimitToSliderLocation(point, SlidersHHeight);
                UpdateColors();
            }
            else if (locationSProgressId == args.Id)
            {
                locationS = LimitToSliderLocation(point, SlidersSHeight);
                UpdateColors();
            }
            else if (locationLProgressId == args.Id)
            {
                locationL = LimitToSliderLocation(point, SlidersLHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationHProgressId == args.Id)
            {
                locationHProgressId = null;
                locationH = LimitToSliderLocation(point, SlidersHHeight);
                UpdateColors();
            }
            else if (locationSProgressId == args.Id)
            {
                locationSProgressId = null;
                locationS = LimitToSliderLocation(point, SlidersSHeight);
                UpdateColors();
            }
            else if (locationLProgressId == args.Id)
            {
                locationLProgressId = null;
                locationL = LimitToSliderLocation(point, SlidersLHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            if (locationHProgressId == args.Id)
            {
                locationHProgressId = null;
            }
            else if (locationSProgressId == args.Id)
            {
                locationSProgressId = null;
            }
            else if (locationLProgressId == args.Id)
            {
                locationLProgressId = null;
            }
        }

        protected override void SelectedColorChanged(Color color)
        {
            if (color.Saturation != 0 || color.Luminosity != 0 || !IsInSliderArea(locationH, SlidersHHeight))
            {
                float leftH = PickerRadiusPixels * 2 + SlidersWidht * (float)color.Hue;
                locationH = new SKPoint(leftH, SlidersHHeight);
            }

            if (color.Luminosity != 0 || !IsInSliderArea(locationS, SlidersSHeight))
            {
                float leftS = PickerRadiusPixels * 2 + SlidersWidht * (float)color.Saturation;
                locationS = new SKPoint(leftS, SlidersSHeight);
            }

            float leftL = PickerRadiusPixels * 2 + SlidersWidht * (float)color.Luminosity;
            locationL = new SKPoint(leftL, SlidersLHeight);

            CanvasView.InvalidateSurface();
        }

        private void UpdateColors()
        {
            var h = (locationH.X - PickerRadiusPixels * 2) / SlidersWidht;
            var s = (locationS.X - PickerRadiusPixels * 2) / SlidersWidht;
            var l = (locationL.X - PickerRadiusPixels * 2) / SlidersWidht;
            var newColor = Color.FromHsla(h, s, l);
            SetSelectedColor(newColor);
            CanvasView.InvalidateSurface();
        }

        private SKPoint LimitToSliderLocation(SKPoint point, float slidersHeight)
        {
            SKPoint result = new SKPoint(point.X, point.Y);
            result.X = result.X >= PickerRadiusPixels * 2 ? result.X : PickerRadiusPixels * 2;
            result.X = result.X <= CanvasView.CanvasSize.Width - PickerRadiusPixels * 2 ? result.X : CanvasView.CanvasSize.Width - PickerRadiusPixels * 2;
            result.Y = slidersHeight;
            return result;
        }

        private bool IsInSliderArea(SKPoint point, float slidersHeight)
        {
            return point.Y >= slidersHeight - PickerRadiusPixels
                && point.Y <= slidersHeight + PickerRadiusPixels;
        }
    }
}
