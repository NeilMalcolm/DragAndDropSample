using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragAndDrop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TypeOneView : BaseDraggableView
    {
        public TypeOneView()
        {
            InitializeComponent();
        }
    }
}