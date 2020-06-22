using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ColorPicker.Classes
{
    public static class SliderFunctionsAlpha
    {
        public static float NewValueAlpha(Color color)
        {
            return (float)color.A;
        }

        public static bool IsSelectedColorChangedAlpha(Color color)
        {
            return true;
        }

        public static Color GetNewColorAlpha(float newValue, Color oldColor)
        {
            return Color.FromRgba(oldColor.R, oldColor.G, oldColor.B, newValue);
        }

        public static SKPaint GetPaintAlpha(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var startColor = Color.FromRgba(color.R, color.G, color.B, 0).ToSKColor();
            var endColor = Color.FromRgba(color.R, color.G, color.B, 1).ToSKColor();
            return GetPaint(startColor, endColor, startPoint, endPoint);
        }

        public static SKPaint GetPaint(SKColor startColor, SKColor endColor, SKPoint startPoint, SKPoint endPoint)
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
