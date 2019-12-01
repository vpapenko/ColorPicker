using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class ColorPickerSkiaSharpBase : SkiaSharpPickerBase
    {
        public ColorPickerSkiaSharpBase()
        {
            PickerRadius = (float?)GetValue(PickerRadiusProperty);
        }

        public static readonly BindableProperty PickerRadiusProperty = BindableProperty.Create(
           nameof(PickerRadius),
           typeof(float?),
           typeof(ColorPickerSkiaSharpBase),
           null,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandlePickerRadiusSet));

        public float? PickerRadius
        {
            get
            {
                return (float?)GetValue(PickerRadiusProperty);
            }
            set
            {
                var currentValue = (float?)GetValue(PickerRadiusProperty);
                if (value == currentValue)
                {
                    float newPickerRadius;
                    if(value == null)
                    {
                        newPickerRadius = GetDefaultPickerRadius();
                    }
                    else
                    {
                        newPickerRadius = (float)value;
                    }
                    PickerRadiusProtected = newPickerRadius;
                }
            }
        }

        protected abstract float GetDefaultPickerRadius();

        float _pickerRadiusProtected;
        protected float PickerRadiusProtected 
        {
            get
            {
                return _pickerRadiusProtected;
            }
            set
            {
                if(_pickerRadiusProtected !=value)
                {
                    _pickerRadiusProtected = value;
                    OnPickerRadiusProtectedChanged(value);
                }
            }
        }

        protected virtual void OnPickerRadiusProtectedChanged(float newValue)
        {

        }

        static void HandlePickerRadiusSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorPickerSkiaSharpBase)bindable).PickerRadius = (float)newValue;
            ((ColorPickerSkiaSharpBase)bindable).CanvasView.InvalidateSurface();
        }

        protected float PickerRadiusPixels { get => ConvertToPixel(PickerRadiusProtected); }
        
        protected void PaintPicker(SKCanvas canvas, SKPoint point)
        {
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke
            };

            paint.Color = Color.White.ToSKColor();
            paint.StrokeWidth = 2;
            canvas.DrawCircle(point, PickerRadiusPixels - 2, paint);

            paint.Color = Color.Black.ToSKColor();
            paint.StrokeWidth = 1;
            canvas.DrawCircle(point, PickerRadiusPixels - 4, paint);
            canvas.DrawCircle(point, PickerRadiusPixels, paint);
        }
    }
}
