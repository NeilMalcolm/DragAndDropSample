using DragAndDrop.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragAndDrop
{
    public class MainViewModel : INotifyPropertyChanged, IDropReceiver, IDragStarter
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<BaseModel> _itemsSource = new ObservableCollection<BaseModel>();
        public ObservableCollection<BaseModel> ItemsSource
        {
            get => _itemsSource;
            set
            {
                _itemsSource = value;
                OnPropertyChanged();
            }
        }

        private string _mostRecentDrop;
        public string MostRecentDrop
        {
            get => _mostRecentDrop;
            set 
            { 
                _mostRecentDrop = value;
                OnPropertyChanged(); 
            }
        }

        public ICommand OnDroppedCommand { get; set; }
        
        private bool _isDragging = false;
        public bool IsDragging
        {
            get => _isDragging;
            set
            {
                _isDragging = value;
                OnPropertyChanged();
            }
        }

        public ICommand OnDragStartCommand { get; set; }
        public ICommand OnDragEndCommand { get; set; }

        public MainViewModel()
        {
            OnDroppedCommand = new Command<BaseModel>(OnDrop);
            OnDragStartCommand = new Command(OnDragStarted);
            OnDragEndCommand = new Command(OnDragEnded);

            ItemsSource.Add(new TypeOneModel
            {
                Text = "First one!"
            });
            ItemsSource.Add(new TypeTwoModel
            {
                Text = "Second one!"
            });
            ItemsSource.Add(new TypeTwoModel
            {
                Text = "Third one!"
            });
        }

        private void OnDragStarted()
        {
            IsDragging = true;
        }
        
        private void OnDragEnded()
        {
            IsDragging = false;
        }

        private void OnDrop(BaseModel model)
        {
            MostRecentDrop = model.Text;
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
