using ColorPicker.Forms.Effects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ColorPicker
{
    public abstract class ColorPickerSkiaSharpBase : SkiaSharpBase
    {

        public static readonly BindableProperty PickerRadiusProperty = BindableProperty.Create(
           nameof(PickerRadius),
           typeof(float),
           typeof(ColorPickerSkiaSharpBase),
           8F,
           propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandlePickerRadiusSet));

        public float PickerRadius
        {
            get
            {
                return (float)GetValue(PickerRadiusProperty);
            }
            set
            {
                float current = (float)GetValue(PickerRadiusProperty);
                if (value < 3)
                {
                    value = 3;
                }
                if (value != current)
                {
                    SetValue(PickerRadiusProperty, value);
                    CanvasView.InvalidateSurface();
                }
            }
        }

        static void HandlePickerRadiusSet(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorPickerSkiaSharpBase)bindable).PickerRadius = (float)newValue;
            ((ColorPickerSkiaSharpBase)bindable).CanvasView.InvalidateSurface();
        }

        public float PickerInternalRadius { get => PickerRadiusPixels - 3; }

        protected float PickerRadiusPixels { get => ConvertToPixel(PickerRadius); }
        
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
