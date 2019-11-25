using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class SkiaSharpBase : ColorPickerContentViewBase
    {
        protected SKCanvasView CanvasView = new SKCanvasView();

        public SkiaSharpBase()
        {
            CanvasView.VerticalOptions = LayoutOptions.Center;
            CanvasView.HorizontalOptions = LayoutOptions.Center;
            CanvasView.PaintSurface += CanvasView_PaintSurface;

            ColorPickerTouchEffect touchEffect = new ColorPickerTouchEffect()
            {
                Capture = true
            };
            touchEffect.TouchAction += TouchEffect_TouchAction;
            
            Content = new Grid
            {
                Children =
                {
                    new Grid
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            CanvasView
                        },
                        Effects =
                        {
                            touchEffect
                        }
                    }
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
