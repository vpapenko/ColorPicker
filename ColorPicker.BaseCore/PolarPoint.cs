using System;

namespace ColorPicker.BaseCore
{
    public class PolarPoint
    {
        public PolarPoint(float radius, float angle)
        {
            Radius = radius;
            Angle = angle;
        }

        public PolarPoint(AbstractPoint point)
        {
            float radius = (float)Math.Sqrt((point.X * point.X) + (point.Y * point.Y));
            float angle = (float)Math.Atan2(point.Y, point.X);
            Radius = radius;
            Angle = angle;
        }

        public float Radius { get; set; }

        float _angel;
        public float Angle
        {
            get
            {
                return _angel;
            }
            set
            {
                _angel = (float)Math.Atan2(Math.Sin(value), Math.Cos(value));
            }
        }

        public AbstractPoint ToAbstractPoint()
        {
            float x = (float)(Radius * Math.Cos(Angle));
            float y = (float)(Radius * Math.Sin(Angle));
            return new AbstractPoint(x, y);
        }

        public override string ToString()
        {
            return string.Format("R: {0}; A: {1}", Radius, Angle);
        }
    }
}
