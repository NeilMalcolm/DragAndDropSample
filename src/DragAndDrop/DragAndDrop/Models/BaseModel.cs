namespace DragAndDrop.Models
{
    public class BaseModel : IDraggable
    {
        public string Text { get; set; }
        public virtual bool CanDrag { get; protected set; } = true;
    }
}
