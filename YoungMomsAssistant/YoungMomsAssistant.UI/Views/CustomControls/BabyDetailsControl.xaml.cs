using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
