using DragAndDrop.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragAndDrop.Views
{
    public abstract class BaseDraggableView : ContentView
    {
        DragGestureRecognizer _dragGestureRecognizer;

        public static BindableProperty OnDragStartedCommandProperty = BindableProperty.Create
            (
                nameof(OnDragStartedCommand),
                typeof(ICommand),
                typeof(HighlightingDropTarget),
                null
            );

        public ICommand OnDragStartedCommand
        {
            get => (ICommand)GetValue(OnDragStartedCommandProperty);
            set => SetValue(OnDragStartedCommandProperty, value);
        }
        
        public static BindableProperty OnDragEndedCommandProperty = BindableProperty.Create
            (
                nameof(OnDragEndedCommand),
                typeof(ICommand),
                typeof(HighlightingDropTarget),
                null
            );

        public ICommand OnDragEndedCommand
        {
            get => (ICommand)GetValue(OnDragEndedCommandProperty);
            set => SetValue(OnDragStartedCommandProperty, value);
        }

        public BaseDraggableView()
        {
            SetGestureRecognizer();

            // Set Bindings for IDragStarter
            var source = new RelativeBindingSource(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(IDragStarter), ancestorLevel: 1);

            this.SetBinding(OnDragStartedCommandProperty, new Binding()
            {
                Source = source,
                Path = nameof(IDragStarter.OnDragStartCommand),
            });
            this.SetBinding(OnDragEndedCommandProperty, new Binding()
            {
                Source = source,
                Path = nameof(IDragStarter.OnDragEndCommand)
            });
        }

        public string DropPacketName { get; set; } = "DropPacket";

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetGestureRecognizer();
        }

        void SetGestureRecognizer()
        {
            if (BindingContext is IDraggable draggableObject)
            {
                ResetAndAddDragGestureRecognizer(draggableObject.CanDrag);
                return;
            }

            RemoveGestureRecognizer();
        }

        void ResetAndAddDragGestureRecognizer(bool canDrag)
        {
            RemoveGestureRecognizer();

            _dragGestureRecognizer = new DragGestureRecognizer()
            {
                CanDrag = canDrag
            };
            _dragGestureRecognizer.DragStarting += _dragGestureRecognizer_DragStarting;
            _dragGestureRecognizer.DropCompleted += _dragGestureRecognizer_DropCompleted;

            this.GestureRecognizers.Add(_dragGestureRecognizer);
        }

        void RemoveGestureRecognizer()
        {
            if (_dragGestureRecognizer == null)
            {
                return;
            }

            _dragGestureRecognizer.DragStarting -= _dragGestureRecognizer_DragStarting;
            _dragGestureRecognizer.DropCompleted -= _dragGestureRecognizer_DropCompleted;
            this.GestureRecognizers.Remove(_dragGestureRecognizer);
            _dragGestureRecognizer = null;
        }

        private void _dragGestureRecognizer_DropCompleted(object sender, DropCompletedEventArgs e)
        {
            OnDragEndedCommand?.Execute(null);
        }

        private void _dragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            OnDragStartedCommand?.Execute(null);
            e.Data.Properties.Add(DropPacketName, BindingContext);
        }
    }
}