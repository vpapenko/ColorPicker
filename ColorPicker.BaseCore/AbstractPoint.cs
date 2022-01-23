namespace ColorPicker.BaseCore
{
    public struct AbstractPoint
    {
        public AbstractPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
