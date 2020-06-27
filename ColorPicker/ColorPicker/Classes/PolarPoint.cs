using System;

namespace ColorPicker.Classes
{
    public class PolarPoint
    {
        public PolarPoint(float radius, float angle)
        {
            Radius = radius;
            Angle = angle;
        }

        public float Radius { get; set; }

        float angel;
        public float Angle
        {
            get
            {
                return angel;
            }
            set
            {
                angel = (float)Math.Atan2(Math.Sin(value), Math.Cos(value));
            }
        }

        public override string ToString()
        {
            return string.Format("R: {0}; A: {1}", Radius, Angle);
        }
    }
}
