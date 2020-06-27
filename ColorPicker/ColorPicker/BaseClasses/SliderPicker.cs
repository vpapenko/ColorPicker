using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

using ColorPicker.Effects;
using ColorPicker.Classes;

namespace ColorPicker.BaseClasses
{
    public abstract class SliderPicker : SkiaSharpPickerBase
    {
        private readonly List<SliderLocation> sliders = new List<SliderLocation>();

        public SliderPicker()
        {
            UpdateSliders();
        }

        public override float GetPickerRadiusPixels()
        {
            return GetPickerRadiusPixels(GetCanvasSize());
        }

        public override float GetPickerRadiusPixels(SKSize canvasSize)
        {
            return GetSize(canvasSize) / (sliders.Count) / 2.2F;
        }

        protected abstract IEnumerable<SliderBase> GetSliders();

        protected void UpdateSliders()
        {
            sliders.Clear();
            var i = 1;
            foreach (var slider in GetSliders())
            {
                var sliderLocation = new SliderLocation(slider)
                {
                    TopLocationMultiplier = (float)(-1.1 + i * 2.2)
                };
                sliders.Add(sliderLocation);
                i++;
            }
            InvalidateSurface();
        }

        protected override void OnPaintSurface(SKCanvas canvas, int width, int height)
        {
            UpdateLocations(SelectedColor, width);
            canvas.Clear();
            foreach (var slider in sliders)
            {
                PaintSlider(canvas, slider, width);
                PaintPicker(canvas, slider.Location);
            }
        }

        protected override void SelectedColorChanged(Color color)
        {
            InvalidateSurface();
        }

        protected override void OnTouchActionPressed(ColorPickerTouchActionEventArgs args)
        {
            var canvasWidth = GetCanvasSize().Width;
            SKPoint point = ConvertToPixel(args.Location);

            foreach (var slider in sliders)
            {
                var SliderTop = slider.GetSliderTop(GetPickerRadiusPixels());
                if (slider.LocationProgressId == null && IsInSliderArea(point, SliderTop))
                {
                    slider.LocationProgressId = args.Id;
                    slider.Location = LimitToSliderLocation(point, SliderTop, canvasWidth);
                    UpdateColors(slider, canvasWidth);
                }
            }
        }

        protected override void OnTouchActionMoved(ColorPickerTouchActionEventArgs args)
        {
            var canvasWidth = GetCanvasSize().Width;
            SKPoint point = ConvertToPixel(args.Location);
            foreach (var slider in sliders)
            {
                if (slider.LocationProgressId == args.Id)
                {
                    slider.Location = LimitToSliderLocation(point, slider.GetSliderTop(GetPickerRadiusPixels()), canvasWidth);
                    UpdateColors(slider, canvasWidth);
                }
            }
        }

        protected override void OnTouchActionReleased(ColorPickerTouchActionEventArgs args)
        {
            var canvasWidth = GetCanvasSize().Width;
            SKPoint point = ConvertToPixel(args.Location);
            foreach (var slider in sliders)
            {
                if (slider.LocationProgressId == args.Id)
                {
                    slider.LocationProgressId = null;
                    slider.Location = LimitToSliderLocation(point, slider.GetSliderTop(GetPickerRadiusPixels()), canvasWidth);
                    UpdateColors(slider, canvasWidth);
                }
            }
        }

        protected override void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args)
        {
            foreach (var slider in sliders)
            {
                if (slider.LocationProgressId == args.Id)
                {
                    slider.LocationProgressId = null;
                }
            }
        }

        protected override SizeRequest GetMeasure(double widthConstraint, double heightConstraint)
        {
            if (Double.IsPositiveInfinity(widthConstraint) &&
                Double.IsPositiveInfinity(heightConstraint))
            {
                throw new InvalidOperationException(
                     "AspectRatioLayout cannot be used with both dimensions unconstrained.");
            }
            var height = double.IsInfinity(heightConstraint) ? widthConstraint * 0.2 * sliders.Count : heightConstraint;
            return new SizeRequest(new Size(widthConstraint, height));
        }

        protected override float GetSize()
        {
            return GetSize(GetCanvasSize());
        }

        protected override float GetSize(SKSize canvasSize)
        {
            return canvasSize.Height;
        }

        private void UpdateLocations(Color color, float canvasWidth)
        {
            foreach (var slider in sliders)
            {
                if (slider.Slider.IsSelectedColorChanged(color) || !IsInSliderArea(slider.Location, slider.GetSliderTop(GetPickerRadiusPixels())))
                {
                    float left = GetPickerRadiusPixels() * 2 + SlidersWidht(canvasWidth) * slider.Slider.NewValue(color);
                    slider.Location = new SKPoint(left, slider.GetSliderTop(GetPickerRadiusPixels()));
                }
            }
        }

        private float SlidersWidht(float canvasWidth)
        {
            return canvasWidth - GetPickerRadiusPixels() * 4;
        }

        private void UpdateColors(SliderLocation slider, float canvasWidth)
        {
            var newColor = SelectedColor;
            var newValue = (slider.Location.X - GetPickerRadiusPixels() * 2) / SlidersWidht(canvasWidth);
            newColor = slider.Slider.GetNewColor(newValue, newColor);

            SelectedColor = newColor;
            InvalidateSurface();
        }

        private void PaintSlider(SKCanvas canvas, SliderLocation slider, float canvasWidth)
        {
            var sliderTop = slider.GetSliderTop(GetPickerRadiusPixels());
            SKPoint startPoint = new SKPoint(GetPickerRadiusPixels() * 2, sliderTop);
            SKPoint endPoint = new SKPoint(canvasWidth - GetPickerRadiusPixels() * 2, sliderTop);
            var paint = slider.Slider.GetPaint(SelectedColor, startPoint, endPoint);
            paint.StrokeWidth = GetPickerRadiusPixels() * 1.3F;
            if (slider.Slider.PaintChessPattern)
            {
                PaintChessPattern(canvas, slider, canvasWidth);
            }
            canvas.DrawLine(startPoint, endPoint, paint);
        }

        private void PaintChessPattern(SKCanvas canvas, SliderLocation slider, float canvasWidth)
        {
            var sliderTop = slider.GetSliderTop(GetPickerRadiusPixels());
            var scale = GetPickerRadiusPixels() / 3;
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
            SKPaint paint = new SKPaint
            {
                PathEffect = SKPathEffect.Create2DPath(matrix, path),
                Color = Color.LightGray.ToSKColor(),
                IsAntialias = true
            };

            var patternRect = new SKRect(GetPickerRadiusPixels(), sliderTop - GetPickerRadiusPixels()
                , canvasWidth - GetPickerRadiusPixels(), sliderTop + GetPickerRadiusPixels());
            var clipRect = new SKRect(GetPickerRadiusPixels() * 1.35f, sliderTop - GetPickerRadiusPixels() * 0.65f
                , canvasWidth - GetPickerRadiusPixels() * 1.35f, sliderTop + GetPickerRadiusPixels() * 0.65f);
            var clipRoundRect = new SKRoundRect(clipRect, GetPickerRadiusPixels() * 0.65f, GetPickerRadiusPixels() * 0.65f);

            canvas.Save();
            canvas.ClipRoundRect(clipRoundRect);
            canvas.DrawRect(patternRect, paint);
            canvas.Restore();
        }

        private bool IsInSliderArea(SKPoint point, float slidersHeight)
        {
            return point.Y >= slidersHeight - GetPickerRadiusPixels()
                && point.Y <= slidersHeight + GetPickerRadiusPixels();
        }

        private SKPoint LimitToSliderLocation(SKPoint point, float slidersHeight, float canvasWidth)
        {
            SKPoint result = new SKPoint(point.X, point.Y);
            result.X = result.X >= GetPickerRadiusPixels() * 2 ? result.X : GetPickerRadiusPixels() * 2;
            result.X = result.X <= canvasWidth - GetPickerRadiusPixels() * 2 ? result.X : canvasWidth - GetPickerRadiusPixels() * 2;
            result.Y = slidersHeight;
            return result;
        }
    }
}
