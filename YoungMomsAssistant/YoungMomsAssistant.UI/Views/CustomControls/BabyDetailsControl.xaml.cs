using System.Windows.Controls;
using Unity;
using YoungMomsAssistant.UI.ViewModels;

namespace YoungMomsAssistant.UI.Views.CustomControls {
    /// <summary>
    /// Interaction logic for BabyDetailsControl.xaml
    /// </summary>
    public partial class BabyDetailsControl : UserControl {
        public BabyDetailsControl() {
            InitializeComponent();
        }

        [Dependency]
        public BabyDetailsViewModel ViewModel {
            set => DataContext = value;
        }
    }
}
