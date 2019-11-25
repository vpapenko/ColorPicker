using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ColorPicker
{
    interface IColorPicker : INotifyPropertyChanged
    {
        Color SelectedColor { get; set; }
    }
}
