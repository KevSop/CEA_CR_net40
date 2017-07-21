using System.Collections.ObjectModel;
using WindowsInput;
using TouchScreenKeyboard.LogicalKeys;
using System.Windows;

namespace TouchScreenKeyboard.Controls
{
    public class OnScreenKeypad : UniformOnScreenKeyboard
    {
        public OnScreenKeypad()
        {
            Keys = new ObservableCollection<OnScreenKey>
                       {
                           new OnScreenKey { GridRow = 0, GridColumn = 0, Key =  new VirtualKey(VirtualKeyCode.SPACE, " "), GridWidth = new GridLength(5, GridUnitType.Star)},
                       };
        }
    }
}