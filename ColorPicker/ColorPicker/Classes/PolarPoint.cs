using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPicker.Forms
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
                //angel = (float)((value + 2 * Math.PI) % (2 * Math.PI));
                angel = (float)Math.Atan2(Math.Sin(value), Math.Cos(value));
            }
        }

        public override string ToString()
        {
            return string.Format("R: {0}; A: {1}", Radius, Angle);
        }
    }
}
