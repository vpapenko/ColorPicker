namespace ColorPicker.BaseCore
{
    public struct AbstractPoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        public AbstractPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public AbstractPoint AddX(float x)
        {
            X += x;
            return this;
        }

        public AbstractPoint AddY(float y)
        {
            Y += y;
            return this;
        }

        public PolarPoint ToPolarPoint()
        {
            return new PolarPoint(this);
        }

        public AbstractPoint Clone()
        {
            return new AbstractPoint(X, Y);
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
