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
            UpdateSliders();
        }

        public static readonly BindableProperty ShowAlphaSliderProperty = BindableProperty.Create(
           nameof(ShowAlphaSlider),
           typeof(bool),
           typeof(ColorPickerSkiaSharpBase),
           false,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleShowAlphaSliderSet));

        public bool ShowAlphaSlider
        {
            get
            {
                return (bool)GetValue(ShowAlphaSliderProperty);
            }
            set
            {
                var currentValue = (bool)GetValue(ShowAlphaSliderProperty);
                if (value != currentValue)
                {
                    UpdateSliders();
                }
            }
        }

        static void HandleShowAlphaSliderSet(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
            {
                ((ColorPickerSkiaSharpBase)bindable).UpdateSliders();
            }
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
                SetPickerRadius(currentValue, value);
            }
        }

        static void HandlePickerRadiusSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorPickerSkiaSharpBase)bindable).SetPickerRadius((float?)oldValue, (float?)newValue);
            ((ColorPickerSkiaSharpBase)bindable).CanvasView.InvalidateSurface();
        }


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

        protected virtual void OnPickerRadiusProtectedChanged(float newValue)
        {

        }

        protected abstract void UpdateSliders();
        protected abstract float GetDefaultPickerRadius();
               
        private void SetPickerRadius(float? oldValue, float? newValue)
        {
            if (newValue != oldValue)
            {
                float newPickerRadius;
                if (newValue == null)
                {
                    newPickerRadius = GetDefaultPickerRadius();
                }
                else
                {
                    newPickerRadius = (float)newValue;
                }
                PickerRadiusProtected = newPickerRadius;
            }
        }

    }
}
