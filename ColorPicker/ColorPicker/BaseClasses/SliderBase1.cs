using ColorPicker.Forms.Effects;
using SkiaSharp;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class SliderBase1 : ColorPickerSkiaSharpBase
    {
        private long? locationProgressId = null;
        private SKPoint location = new SKPoint();

        private float SlidersWidht { get => CanvasView.CanvasSize.Width - PickerRadiusPixels * 4; }
        private float SlidersHeight { get => PickerRadiusPixels * 2.2f; }


        protected override void OnPaintSurface(SKCanvas canvas)
        {
            SelectedColorChanged(SelectedColor);
            canvas.Clear();

            PaintSlider(canvas, SlidersHeight);

            PaintPicker(canvas, location);
        }
        
        protected override void SelectedColorChanged(Color color)
        {
            if(SelectedColorChanged(color,out float newValue))
            {
                float left = PickerRadiusPixels * 2 + SlidersWidht * newValue;
                location = new SKPoint(left, SlidersHeight);
            }

            CanvasView.InvalidateSurface();
        }

        protected abstract bool SelectedColorChanged(Color color, out float newValue);

        private void UpdateColors()
        {
            var newValue = (location.X - PickerRadiusPixels * 2) / SlidersWidht;
            var newColor = GetNewColor(newValue, SelectedColor);
            SetSelectedColor(newColor);
            CanvasView.InvalidateSurface();
        }

        protected abstract Color GetNewColor(float newValue, Color oldColor);

        protected override void OnSizeAllocated(double width, double height)
        {
            CanvasView.WidthRequest = width;
            if (PickerRadius != null)
            {
                height = PickerRadiusProtected * 8.4;
            }
            else
            {
                PickerRadiusProtected = GetDefaultPickerRadius(height);
            }
            CanvasView.HeightRequest = height;
            base.OnSizeAllocated(width, height);
        }

        private void PaintSlider(SKCanvas canvas, float slidersHeight)
        {
            var colors = GetColors();
            var colorPos = GetColorsPos();

            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, slidersHeight);
            SKPoint endPoint = new SKPoint(CanvasView.CanvasSize.Width - PickerRadiusPixels * 2, slidersHeight);
            SKShader shader = SKShader.CreateLinearGradient(startPoint, endPoint
                    , colors, colorPos, SKShaderTileMode.Clamp);

            PaintSlider(canvas, slidersHeight, shader);

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

        protected abstract SKColor[] GetColors();
        protected abstract float[] GetColorsPos();

        private void PaintSlider(SKCanvas canvas, float slidersHeight, SKShader shader)
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
            if (locationProgressId == null && IsInSliderArea(point, SlidersHeight))
            {
                locationProgressId = args.Id;
                location = LimitToSliderLocation(point, SlidersHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationProgressId == args.Id)
            {
                location = LimitToSliderLocation(point, SlidersHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            if (locationProgressId == args.Id)
            {
                locationProgressId = null;
                location = LimitToSliderLocation(point, SlidersHeight);
                UpdateColors();
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            if (locationProgressId == args.Id)
            {
                locationProgressId = null;
            }
        }

        protected override float GetDefaultPickerRadius()
        {
            return GetDefaultPickerRadius(CanvasView.Height);
        }

        private float GetDefaultPickerRadius(double canvasViewHeight)
        {
            return (float)(canvasViewHeight / 8.4d);
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
