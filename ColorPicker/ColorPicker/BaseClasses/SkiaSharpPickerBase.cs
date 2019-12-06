using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class SkiaSharpPickerBase : ColorPickerContentViewBase
    {
        protected SKCanvasView CanvasView = new SKCanvasView();
        protected Grid mainGrid;
        protected const double CanvasViewRowHeight = 100;

        public SkiaSharpPickerBase()
        {
            CanvasView.VerticalOptions = LayoutOptions.Center;
            CanvasView.HorizontalOptions = LayoutOptions.Center;
            CanvasView.PaintSurface += CanvasView_PaintSurface;
            CanvasView.BackgroundColor = Color.Pink;

            ColorPickerTouchEffect touchEffect = new ColorPickerTouchEffect()
            {
                Capture = true
            };
            touchEffect.TouchAction += TouchEffect_TouchAction;

            mainGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Effects =
                {
                    touchEffect
                }
            };

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(CanvasViewRowHeight, GridUnitType.Star) });
            mainGrid.Children.Add(CanvasView, 0, 0);

            Content = new Grid
            {
                Children =
                {
                    mainGrid
                }
            };

        }

        protected abstract void OnPaintSurface(SKCanvas canvas);
        protected abstract void OnTouchActionPressed(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionMoved(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionReleased(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args);

        protected SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(CanvasView.CanvasSize.Width * pt.X / CanvasView.Width),
                               (float)(CanvasView.CanvasSize.Height * pt.Y / CanvasView.Height));
        }

        protected float ConvertToPixel(float dip)
        {
            return (float)(CanvasView.CanvasSize.Width * dip / CanvasView.Width);
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas);
        }

        private void TouchEffect_TouchAction(object sender, ColorPickerTouchActionEventArgs e)
        {
            switch (e.Type)
            {
                case ColorPickerTouchActionType.Pressed:
                    OnTouchActionPressed(e);
                    break;
                case ColorPickerTouchActionType.Moved:
                    OnTouchActionMoved(e);
                    break;
                case ColorPickerTouchActionType.Released:
                    OnTouchActionReleased(e);
                    break;
                case ColorPickerTouchActionType.Cancelled:
                    OnTouchActionCancelled(e);
                    break;
            }
        }
    }
}
