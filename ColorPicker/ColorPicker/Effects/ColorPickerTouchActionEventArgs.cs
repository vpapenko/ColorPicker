using System;
using Xamarin.Forms;

namespace ColorPicker.Effects
{
    public class ColorPickerTouchActionEventArgs : EventArgs
    {
        public ColorPickerTouchActionEventArgs(long id, ColorPickerTouchActionType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public ColorPickerTouchActionType Type { private set; get; }

        public Point Location { private set; get; }

        public bool IsInContact { private set; get; }
    }
}
