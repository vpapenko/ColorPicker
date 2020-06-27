using System.ComponentModel;
using Xamarin.Forms;

namespace ColorPicker.Interfaces
{
    public interface IColorPicker : INotifyPropertyChanged
    {
        Color SelectedColor { get; set; }
        IColorPicker ConnectedColorPicker { get; set; }
    }
}
