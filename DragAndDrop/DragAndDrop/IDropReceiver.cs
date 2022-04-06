using System.Windows.Input;

namespace DragAndDrop
{
    public interface IDropReceiver
    {
        ICommand OnDroppedCommand { get; set; }
        bool IsDragging { get; }
    }
}
