using System.Windows.Input;
using Xamarin.Forms;

namespace DragAndDrop.Views
{
    public abstract class BaseDropTarget : ContentView
    {
        private DropGestureRecognizer _dropGestureRecognizer;

        public static BindableProperty OnDropCommandProperty = BindableProperty.Create
        (
            nameof(OnDropCommand),
            typeof(ICommand),
            typeof(HighlightingDropTarget),
            null
        );

        public ICommand OnDropCommand
        {
            get => (ICommand)GetValue(OnDropCommandProperty);
            set => SetValue(OnDropCommandProperty, value);
        }

        public static BindableProperty IsDraggingProperty = BindableProperty.Create
        (
            nameof(IsDraggingProperty),
            typeof(bool),
            typeof(HighlightingDropTarget),
            null,
            propertyChanged: OnIsDraggingChanged
        );

        public bool IsDragging
        {
            get => (bool)GetValue(IsDraggingProperty);
            set => SetValue(IsDraggingProperty, value);
        }

        public string DropPacketName { get; set; } = "DropPacket";

        protected abstract void OnDragStart();
        protected abstract void OnDragEnd();
        protected abstract void OnDragOver();
        protected abstract void OnDragLeave();
        protected abstract void OnDrop(object dropPacket);

        public static void OnIsDraggingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is BaseDropTarget dropTarget)
            {
                if (newValue is true)
                {
                    dropTarget.OnDragStart();
                    return;
                }

                dropTarget.OnDragEnd();
            }
        }

        public BaseDropTarget()
        {
            ResetAndAddDragGestureRecognizer();
            SetUpBindingSources();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ResetAndAddDragGestureRecognizer();
        }

        void ResetAndAddDragGestureRecognizer()
        {
            RemoveGestureRecognizer();

            _dropGestureRecognizer = new DropGestureRecognizer()
            {
                AllowDrop = true
            };
            _dropGestureRecognizer.DragOver += _dropGestureRecognizer_DragOver;
            _dropGestureRecognizer.DragLeave += _dropGestureRecognizer_DragLeave;
            _dropGestureRecognizer.Drop += _dropGestureRecognizer_Drop;

            GestureRecognizers.Add(_dropGestureRecognizer);
        }
        void _dropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            OnDrop(e.Data.Properties[DropPacketName]);
            OnDropCommand?.Execute(e.Data.Properties[DropPacketName]);
        }

        void _dropGestureRecognizer_DragOver(object sender, DragEventArgs e)
        {
            OnDragOver();
        }

        void _dropGestureRecognizer_DragLeave(object sender, DragEventArgs e)
        {
            OnDragLeave();
        }

        void SetUpBindingSources()
        {
            // Set Bindings for IDropReceiver
            var source = new RelativeBindingSource(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(IDropReceiver), ancestorLevel: 1); // 1 to find closest.
            this.SetBinding(IsDraggingProperty, new Binding()
            {
                Source = source,
                Path = nameof(IDropReceiver.IsDragging),
            });
            this.SetBinding(OnDropCommandProperty, new Binding()
            {
                Source = source,
                Path = nameof(IDropReceiver.OnDroppedCommand)
            });
        }

        void RemoveGestureRecognizer()
        {
            if (_dropGestureRecognizer == null)
            {
                return;
            }

            // Cleanup
            _dropGestureRecognizer.DragOver -= _dropGestureRecognizer_DragOver;
            _dropGestureRecognizer.DragLeave -= _dropGestureRecognizer_DragLeave;
            _dropGestureRecognizer.Drop -= _dropGestureRecognizer_Drop;

            GestureRecognizers.Remove(_dropGestureRecognizer);
            _dropGestureRecognizer = null;
        }
    }
}
