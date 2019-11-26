using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ColorPicker
{
    public class RGBSliders : ColorPickerSkiaSharpBase
    {
        private long? locationRProgressId = null;
        private long? locationGProgressId = null;
        private long? locationBProgressId = null;
        private SKPoint locationR = new SKPoint();
        private SKPoint locationG = new SKPoint();
        private SKPoint locationB = new SKPoint();

        private float SlidersWidht { get => CanvasView.CanvasSize.Width - PickerRadiusPixels * 4; }
        private float SlidersRHeight { get => PickerRadiusPixels * 2; }
        private float SlidersGHeight { get => PickerRadiusPixels * 4.2F; }
        private float SlidersBHeight { get => PickerRadiusPixels * 6.4F; }

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

            var startColor = new Color(0, SelectedColor.G, SelectedColor.B).ToSKColor();
            var endColor = new Color(1, SelectedColor.G, SelectedColor.B).ToSKColor();
            PaintSlider(canvas, SlidersRHeight, startColor, endColor);

            startColor = new Color(SelectedColor.R, 0, SelectedColor.B).ToSKColor();
            endColor = new Color(SelectedColor.R, 1, SelectedColor.B).ToSKColor();
            PaintSlider(canvas, SlidersGHeight, startColor, endColor);

            startColor = new Color(SelectedColor.R, SelectedColor.G, 0).ToSKColor();
            endColor = new Color(SelectedColor.R, SelectedColor.G, 1).ToSKColor();
            PaintSlider(canvas, SlidersBHeight, startColor, endColor);

            PaintPicker(canvas, locationR);
            PaintPicker(canvas, locationG);
            PaintPicker(canvas, locationB);
        }

        private void PaintSlider(SKCanvas canvas, float slidersHeight, SKColor startColor, SKColor endColor)
        {
            var canvasWidth = CanvasView.CanvasSize.Width;
            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, slidersHeight);
            SKPoint endPoint = new SKPoint(canvasWidth - PickerRadiusPixels * 2, slidersHeight);

            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.StrokeJoin = SKStrokeJoin.Round;
                paint.StrokeWidth = PickerRadiusPixels * 1.3F;
                paint.Shader = SKShader.CreateLinearGradient(startPoint, endPoint
                    , new SKColor[] { startColor, endColor }, new float[] { 0, 1 }, SKShaderTileMode.Clamp);
                canvas.DrawLine(startPoint, endPoint, paint);
            }
        }
        
        protected override void OnTouchActionPressed(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationRProgressId == null && IsInSliderArea(point, SlidersRHeight))
            {
                locationRProgressId = args.Id;
                locationR = LimitToSliderLocation(point, SlidersRHeight);
                UpdateColors();
            }
            else if (locationGProgressId == null && IsInSliderArea(point, SlidersGHeight))
            {
                locationGProgressId = args.Id;
                locationG = LimitToSliderLocation(point, SlidersGHeight);
                UpdateColors();
            }
            else if (locationBProgressId == null && IsInSliderArea(point, SlidersBHeight))
            {
                locationBProgressId = args.Id;
                locationB = LimitToSliderLocation(point, SlidersBHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationRProgressId == args.Id)
            {
                locationR = LimitToSliderLocation(point, SlidersRHeight);
                UpdateColors();
            }
            else if (locationGProgressId == args.Id)
            {
                locationG = LimitToSliderLocation(point, SlidersGHeight);
                UpdateColors();
            }
            else if (locationBProgressId == args.Id)
            {
                locationB = LimitToSliderLocation(point, SlidersBHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationRProgressId == args.Id)
            {
                locationRProgressId = null;
                locationR = LimitToSliderLocation(point, SlidersRHeight);
                UpdateColors();
            }
            else if (locationGProgressId == args.Id)
            {
                locationGProgressId = null;
                locationG = LimitToSliderLocation(point, SlidersGHeight);
                UpdateColors();
            }
            else if (locationBProgressId == args.Id)
            {
                locationBProgressId = null;
                locationB = LimitToSliderLocation(point, SlidersBHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            if (locationRProgressId == args.Id)
            {
                locationRProgressId = null;
            }
            else if (locationGProgressId == args.Id)
            {
                locationGProgressId = null;
            }
            else if (locationBProgressId == args.Id)
            {
                locationBProgressId = null;
            }
        }

        protected override void SelectedColorChanged(Color color)
        {
            float leftR = PickerRadiusPixels * 2 + SlidersWidht * (float)color.R;
            locationR = new SKPoint(leftR, SlidersRHeight);

            float leftG = PickerRadiusPixels * 2 + SlidersWidht * (float)color.G;
            locationG = new SKPoint(leftG, SlidersGHeight);

            float leftB = PickerRadiusPixels * 2 + SlidersWidht * (float)color.B;
            locationB = new SKPoint(leftB, SlidersBHeight);

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

        private void UpdateColors()
        {
            var r = (locationR.X - PickerRadiusPixels * 2) / SlidersWidht;
            var g = (locationG.X - PickerRadiusPixels * 2) / SlidersWidht;
            var b = (locationB.X - PickerRadiusPixels * 2) / SlidersWidht;
            var newColor = new Color(r, g, b);
            SetSelectedColor(newColor);
            CanvasView.InvalidateSurface();
        }
    }
}
