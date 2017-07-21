using System.ComponentModel;

namespace TouchScreenKeyboard.LogicalKeys
{
    public interface ILogicalKey : INotifyPropertyChanged
    {
        string DisplayName { get; }
        void Press();
        event LogicalKeyPressedEventHandler LogicalKeyPressed;
    }
}