using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class SliderPicker : ColorPickerSkiaSharpBase
    {
        private readonly List<SliderLocation> _sliders = new List<SliderLocation>();
        private float SlidersWidht { get => CanvasView.CanvasSize.Width - PickerRadiusPixels * 4; }

        protected abstract IEnumerable<SliderBase> GetSliders();

        protected override void OnPickerRadiusProtectedChanged(float newValue)
        {
            base.OnPickerRadiusProtectedChanged(newValue);
        }

        protected override void OnPaintSurface(SKCanvas canvas)
        {
            SelectedColorChanged(SelectedColor);
            canvas.Clear();
            foreach (var slider in _sliders)
            {
                PaintSlider(canvas, slider);
                PaintPicker(canvas, slider.Location);
            }
        }

        protected override void SelectedColorChanged(Color color)
        {
            foreach (var slider in _sliders)
            {
                if (slider.Slider.IsSelectedColorChanged(color) || !IsInSliderArea(slider.Location, slider.GetSliderTop(PickerRadiusPixels)))
                {
                    float left = PickerRadiusPixels * 2 + SlidersWidht * slider.Slider.NewValue(color);
                    slider.Location = new SKPoint(left, slider.GetSliderTop(PickerRadiusPixels));
                }
            }

            CanvasView.InvalidateSurface();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SetSize(width, height);
        }

        protected override void OnTouchActionPressed(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);

            foreach (var slider in _sliders)
            {
                var SliderTop = slider.GetSliderTop(PickerRadiusPixels);
                if (slider.LocationProgressId == null && IsInSliderArea(point, SliderTop))
                {
                    slider.LocationProgressId = args.Id;
                    slider.Location = LimitToSliderLocation(point, SliderTop);
                    UpdateColors(slider);
                }
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            foreach (var slider in _sliders)
            {
                if (slider.LocationProgressId == args.Id)
                {
                    slider.Location = LimitToSliderLocation(point, slider.GetSliderTop(PickerRadiusPixels));
                    UpdateColors(slider);
                }
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            SKPoint point = ConvertToPixel(args.Location);
            foreach (var slider in _sliders)
            {
                if (slider.LocationProgressId == args.Id)
                {
                    slider.LocationProgressId = null;
                    slider.Location = LimitToSliderLocation(point, slider.GetSliderTop(PickerRadiusPixels));
                    UpdateColors(slider);
                }
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            foreach (var slider in _sliders)
            {
                if (slider.LocationProgressId == args.Id)
                {
                    slider.LocationProgressId = null;
                }
            }
        }

        protected override float GetDefaultPickerRadius()
        {
            return GetDefaultPickerRadius(CanvasView.Height);
        }
               
        private void UpdateColors(SliderLocation slider)
        {
            var newColor = SelectedColor;
            var newValue = (slider.Location.X - PickerRadiusPixels * 2) / SlidersWidht;
            newColor = slider.Slider.GetNewColor(newValue, newColor);

            SetSelectedColor(newColor);
            CanvasView.InvalidateSurface();
        }

        private void PaintSlider(SKCanvas canvas, SliderLocation slider)
        {
            var sliderTop = slider.GetSliderTop(PickerRadiusPixels);
            SKPoint startPoint = new SKPoint(PickerRadiusPixels * 2, sliderTop);
            SKPoint endPoint = new SKPoint(CanvasView.CanvasSize.Width - PickerRadiusPixels * 2, sliderTop);
            var paint = slider.Slider.GetPaint(SelectedColor, startPoint, endPoint);
            paint.StrokeWidth = PickerRadiusPixels * 1.3F;
            if (slider.Slider.PaintChessPattern)
            {
                PaintChessPattern(canvas, slider);
            }
            canvas.DrawLine(startPoint, endPoint, paint);
        }

        private void PaintChessPattern(SKCanvas canvas, SliderLocation slider)
        {
            var sliderTop = slider.GetSliderTop(PickerRadiusPixels);
            var scale = PickerRadiusPixels / 3;
            SKPath path = new SKPath();
            path.MoveTo(-1 * scale, -1 * scale);
            path.LineTo(0 * scale, -1 * scale);
            path.LineTo(0 * scale, 0 * scale);
            path.LineTo(1 * scale, 0 * scale);
            path.LineTo(1 * scale, 1 * scale);
            path.LineTo(0 * scale, 1 * scale);
            path.LineTo(0 * scale, 0 * scale);
            path.LineTo(-1 * scale, 0 * scale);
            path.LineTo(-1 * scale, -1 * scale);

            SKMatrix matrix = SKMatrix.MakeScale(2 * scale, 2 * scale);
            SKPaint paint = new SKPaint();
            paint.PathEffect = SKPathEffect.Create2DPath(matrix, path);
            paint.Color = Color.LightGray.ToSKColor();
            paint.IsAntialias = true;
            
            var patternRect = new SKRect(PickerRadiusPixels, sliderTop - PickerRadiusPixels, CanvasView.CanvasSize.Width - PickerRadiusPixels, sliderTop + PickerRadiusPixels);
            var clipRect = new SKRect(PickerRadiusPixels * 1.35f, sliderTop - PickerRadiusPixels * 0.65f, CanvasView.CanvasSize.Width - PickerRadiusPixels * 1.35f, sliderTop + PickerRadiusPixels * 0.65f);
            var clipRoundRect = new SKRoundRect(clipRect, PickerRadiusPixels * 0.65f, PickerRadiusPixels * 0.65f);

            canvas.Save();
            canvas.ClipRoundRect(clipRoundRect);
            canvas.DrawRect(patternRect, paint);
            canvas.Restore();
        }

        private float GetDefaultPickerRadius(double canvasViewHeight)
        {
            return (float)(canvasViewHeight / (_sliders.Count * 2.4));
        }

        private bool IsInSliderArea(SKPoint point, float slidersHeight)
        {
            return point.Y >= slidersHeight - PickerRadiusPixels
                && point.Y <= slidersHeight + PickerRadiusPixels;
        }

        private SKPoint LimitToSliderLocation(SKPoint point, float slidersHeight)
        {
            SKPoint result = new SKPoint(point.X, point.Y);
            result.X = result.X >= PickerRadiusPixels * 2 ? result.X : PickerRadiusPixels * 2;
            result.X = result.X <= CanvasView.CanvasSize.Width - PickerRadiusPixels * 2 ? result.X : CanvasView.CanvasSize.Width - PickerRadiusPixels * 2;
            result.Y = slidersHeight;
            return result;
        }

        protected override void UpdateSliders()
        {
            _sliders.Clear();
            var i = 0;
            foreach (var slider in GetSliders())
            {
                var sliderLocation = new SliderLocation(slider)
                {
                    TopLocationMultiplier = (float)(1.4 + i * 2.2)
                };
                _sliders.Add(sliderLocation);
                i++;
            }

            SetSize(CanvasView.Width, CanvasView.Height);
            CanvasView.InvalidateSurface();
        }

        private void SetSize(double width, double height)
        {
            CanvasView.WidthRequest = width;
            if (PickerRadius != null)
            {
                height = PickerRadiusProtected * (0.6 + _sliders.Count * 2.2);
            }
            else
            {
                PickerRadiusProtected = GetDefaultPickerRadius(height);
            }
            CanvasView.HeightRequest = height;
        }

    }
}
