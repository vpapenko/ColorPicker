using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ColorPicker
{
    public static class SliderFunctionsHSL
    {
        public static float NewValueH(Color color)
        {
            return (float)color.Hue;
        }

        public static float NewValueS(Color color)
        {
            return (float)color.Saturation;
        }

        public static float NewValueL(Color color)
        {
            return (float)color.Luminosity;
        }

        public static bool IsSelectedColorChangedH(Color color)
        {
            return true;
        }

        public static bool IsSelectedColorChangedS(Color color)
        {
            return true;
        }

        public static bool IsSelectedColorChangedL(Color color)
        {
            return true;
        }

        public static Color GetNewColorH(float newValue, Color oldColor)
        {
            return Color.FromHsla(newValue, oldColor.Saturation, oldColor.Luminosity, oldColor.A);
        }

        public static Color GetNewColorS(float newValue, Color oldColor)
        {
            return Color.FromHsla(oldColor.Hue, newValue, oldColor.Luminosity, oldColor.A);
        }

        public static Color GetNewColorL(float newValue, Color oldColor)
        {
            return Color.FromHsla(oldColor.Hue, oldColor.Saturation, newValue, oldColor.A);
        }

        public static SKPaint GetPaintH(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var colors = new List<SKColor>();
            for (int i = 0; i <= 255; i++)
            {
                colors.Add(Color.FromHsla(i / 255D, 1, 0.5).ToSKColor());
            }

            var colorPos = new List<float>();
            for (int i = 0; i <= 255; i++)
            {
                colorPos.Add(i / 255F);
            }

            return GetPaint(colors.ToArray(), colorPos.ToArray(), startPoint, endPoint);
        }

        public static SKPaint GetPaintS(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var colors = new SKColor[]
            {
                Color.FromHsla(color.Hue, 0, color.Luminosity).ToSKColor(),
                Color.FromHsla(color.Hue, 1, color.Luminosity).ToSKColor()
            };

            var colorPos = new float[] { 0, 1 };
            return GetPaint(colors, colorPos, startPoint, endPoint);
        }

        public static SKPaint GetPaintL(Color color, SKPoint startPoint, SKPoint endPoint)
        {
            var colors = new SKColor[]
            {
                Color.FromHsla(color.Hue, color.Saturation, 0).ToSKColor(),
                Color.FromHsla(color.Hue, color.Saturation, 0.5).ToSKColor(),
                Color.FromHsla(color.Hue, color.Saturation, 1).ToSKColor()
            };

            var colorPos = new float[] { 0, 0.5F, 1 };
            return GetPaint(colors, colorPos, startPoint, endPoint);
        }

        public static SKPaint GetPaint(SKColor[] colors, float[] colorPos, SKPoint startPoint, SKPoint endPoint)
        {
            SKShader shader = SKShader.CreateLinearGradient(startPoint, endPoint
           , colors, colorPos, SKShaderTileMode.Clamp);

            var paint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                Shader = shader
            };
            return paint;
        }
    }
}
