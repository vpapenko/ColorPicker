using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ColorPicker
{
    public class RGBSliders : SliderPicker
    {
        public RGBSliders()
            : base(GetSliders())
        {
        }

        private static IEnumerable<SliderBase> GetSliders()
        {
            return new Slider[]
            {
                new Slider(NewValueR, IsSelectedColorChanged, GetNewColorR, GetPaintR),
                new Slider(NewValueG, IsSelectedColorChanged, GetNewColorG, GetPaintG),
                new Slider(NewValueB, IsSelectedColorChanged, GetNewColorB, GetPaintB)
            };
        }

        private static float NewValueR(Color color)
        {
            return (float)color.R;
        }

        private static float NewValueG(Color color)
        {
            return (float)color.G;
        }

        private static float NewValueB(Color color)
        {
            return (float)color.B;
        }

        private static bool IsSelectedColorChanged(Color color)
        {
            return true;
        }

        private static Color GetNewColorR(float newValue, Color oldColor)
        {
            return Color.FromRgb(newValue, oldColor.G, oldColor.B);
        }

        private static Color GetNewColorG(float newValue, Color oldColor)
        {
            return Color.FromRgb(oldColor.R, newValue, oldColor.B);
        }

        private static Color GetNewColorB(float newValue, Color oldColor)
        {
            return Color.FromRgb(oldColor.R, oldColor.G, newValue);
        }
               
        private static SKPaint GetPaintR(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = new Color(0, color.G, color.B).ToSKColor();
            var endColor = new Color(1, color.G, color.B).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        private static SKPaint GetPaintG(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = new Color(color.R, 0, color.B).ToSKColor();
            var endColor = new Color(color.R, 1, color.B).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        private static SKPaint GetPaintB(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = new Color(color.R, color.G, 0).ToSKColor();
            var endColor = new Color(color.R, color.G, 1).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        private static SKPaint GetPaint(SKColor startColor, SKColor endColor, SKPoint startPoint, SKPoint endPoint)
        {
            var paint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                Shader = SKShader.CreateLinearGradient(startPoint, endPoint
                    , new SKColor[] { startColor, endColor }, new float[] { 0, 1 }, SKShaderTileMode.Clamp)
            };
            return paint;
        }
    }
}
