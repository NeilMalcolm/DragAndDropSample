using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DragAndDrop.Models
{
    public class DropTargetViewModel : INotifyPropertyChanged, IDropTarget
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanReceiveDrop { get; set; }

        public string Text { get; set; }

        public DropTargetViewModel(bool canReceiveDrop, string text) => 
            (CanReceiveDrop, Text) = (canReceiveDrop, text);

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
