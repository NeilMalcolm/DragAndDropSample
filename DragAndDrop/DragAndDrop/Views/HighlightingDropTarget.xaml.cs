using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragAndDrop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HighlightingDropTarget : BaseDropTarget
    {
        public static BindableProperty TextProperty = BindableProperty.Create
        (
            nameof(Text),
            typeof(string),
            typeof(HighlightingDropTarget),
            string.Empty
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public HighlightingDropTarget()
        {
            InitializeComponent();
            BackgroundColor = Color.LightBlue;
        }

        protected override void OnDragEnd()
        {
            DisplayLabel.FontAttributes =  FontAttributes.None;
            DisplayLabel.FontSize = Device.GetNamedSize(NamedSize.Default, DisplayLabel);
            DisplayLabel.VerticalTextAlignment = TextAlignment.Start;
            DisplayLabel.HorizontalTextAlignment = TextAlignment.Start;
        }

        protected override void OnDragLeave()
        {
            BackgroundColor = Color.LightBlue;
        }

        protected override void OnDragOver()
        {
            BackgroundColor = Color.Yellow;
        }

        protected override void OnDragStart()
        {
            DisplayLabel.FontAttributes = FontAttributes.Bold;
            DisplayLabel.FontSize = Device.GetNamedSize(NamedSize.Large, DisplayLabel);
            DisplayLabel.VerticalTextAlignment = TextAlignment.Center;
            DisplayLabel.HorizontalTextAlignment = TextAlignment.Center;
        }
        
        protected override void OnDrop(object dropObject)
        {
            BackgroundColor = Color.Green;
        }
    }
}