using System.Windows.Input;

namespace DragAndDrop
{
    public interface IDragStarter
    {
        public ICommand OnDragStartCommand { get; set; }
        public ICommand OnDragEndCommand { get; set; }
    }
}
