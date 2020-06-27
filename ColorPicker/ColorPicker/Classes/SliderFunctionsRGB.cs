using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ColorPicker.Classes
{
    public static class SliderFunctionsRGB
    {
        public static float NewValueR(Color color)
        {
            return (float)color.R;
        }

        public static float NewValueG(Color color)
        {
            return (float)color.G;
        }

        public static float NewValueB(Color color)
        {
            return (float)color.B;
        }

        public static bool IsSelectedColorChanged(Color color)
        {
            return true;
        }

        public static Color GetNewColorR(float newValue, Color oldColor)
        {
            return Color.FromRgba(newValue, oldColor.G, oldColor.B, oldColor.A);
        }

        public static Color GetNewColorG(float newValue, Color oldColor)
        {
            return Color.FromRgba(oldColor.R, newValue, oldColor.B, oldColor.A);
        }

        public static Color GetNewColorB(float newValue, Color oldColor)
        {
            return Color.FromRgba(oldColor.R, oldColor.G, newValue, oldColor.A);
        }

        public static SKPaint GetPaintR(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = new Color(0, color.G, color.B).ToSKColor();
            var endColor = new Color(1, color.G, color.B).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        public static SKPaint GetPaintG(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = new Color(color.R, 0, color.B).ToSKColor();
            var endColor = new Color(color.R, 1, color.B).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        public static SKPaint GetPaintB(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = new Color(color.R, color.G, 0).ToSKColor();
            var endColor = new Color(color.R, color.G, 1).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        public static SKPaint GetPaint(SKColor startColor, SKColor endColor, SKPoint startPoint, SKPoint endPoint)
        {
            var paint = new SKPaint()
            {
                IsAntialias = true,
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
