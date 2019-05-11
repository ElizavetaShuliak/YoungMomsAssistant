using System.Windows;
using Unity;
using YoungMomsAssistant.UI.ViewModels;

namespace YoungMomsAssistant.UI.Views.Windows {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        [Dependency]
        public MainViewModel ViewModel {
            set => DataContext = value;
        }
    }
}
