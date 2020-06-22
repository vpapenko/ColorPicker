using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

using ColorPicker.Effects;

namespace ColorPicker.BaseClasses
{
    public abstract class SkiaSharpPickerBase : ColorPickerViewBase
    {
        private readonly View canvasView;

        public SkiaSharpPickerBase()
        {
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;


            if (Device.RuntimePlatform == "Windows" && Device.Idiom == TargetIdiom.Phone)
            {
                var view = new SKCanvasView();
                view.PaintSurface += CanvasView_PaintSurface;
                canvasView = view;
            }
            else
            {
                var view = new SKGLView();
                view.PaintSurface += GLView_PaintSurface;
                canvasView = view;
            }

            ColorPickerTouchEffect touchEffect = new ColorPickerTouchEffect()
            {
                Capture = true
            };
            touchEffect.TouchAction += TouchEffect_TouchAction;
            Effects.Add(touchEffect);
            Children.Add(canvasView);
        }

        public static readonly BindableProperty PickerRadiusScaleProperty = BindableProperty.Create(
           nameof(PickerRadiusScale),
           typeof(float),
           typeof(SkiaSharpPickerBase),
           0.05F,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandlePickerRadiusScaleSet));

        static void HandlePickerRadiusScaleSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((SkiaSharpPickerBase)bindable).InvalidateSurface();
        }

        public float PickerRadiusScale
        {
            get
            {
                return (float)GetValue(PickerRadiusScaleProperty);
            }
            set
            {
                SetValue(PickerRadiusScaleProperty, value);
            }
        }

        public abstract float GetPickerRadiusPixels();
        public abstract float GetPickerRadiusPixels(SKSize canvasSize);

        protected abstract SizeRequest GetMeasure(double widthConstraint, double heightConstraint);
        protected abstract float GetSize();
        protected abstract float GetSize(SKSize canvasSize);
        protected abstract void OnPaintSurface(SKCanvas canvas, int width, int height);
        protected abstract void OnTouchActionPressed(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionMoved(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionReleased(ColorPickerTouchActionEventArgs args);
        protected abstract void OnTouchActionCancelled(ColorPickerTouchActionEventArgs args);


        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return GetMeasure(widthConstraint, heightConstraint);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            canvasView.Layout(new Rectangle(x, y, width, height));
        }

        protected SKPoint ConvertToPixel(Point pt)
        {
            var canvasSize = GetCanvasSize();
            return new SKPoint((float)(canvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasSize.Height * pt.Y / canvasView.Height));
        }

        protected void InvalidateSurface()
        {
            if (canvasView is SKCanvasView)
            {
                (canvasView as SKCanvasView).InvalidateSurface();
            }
            else
            {
                (canvasView as SKGLView).InvalidateSurface();
            }
        }

        protected SKSize GetCanvasSize()
        {
            if (canvasView is SKCanvasView)
            {
                return (canvasView as SKCanvasView).CanvasSize;
            }
            else
            {
                return (canvasView as SKGLView).CanvasSize;
            }
        }

        protected void PaintPicker(SKCanvas canvas, SKPoint point)
        {
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke
            };

            paint.Color = Color.White.ToSKColor();
            paint.StrokeWidth = 2;
            canvas.DrawCircle(point, GetPickerRadiusPixels() - 2, paint);

            paint.Color = Color.Black.ToSKColor();
            paint.StrokeWidth = 1;
            canvas.DrawCircle(point, GetPickerRadiusPixels() - 4, paint);
            canvas.DrawCircle(point, GetPickerRadiusPixels(), paint);
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

        private void GLView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas, e.RenderTarget.Width, e.RenderTarget.Height);
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
