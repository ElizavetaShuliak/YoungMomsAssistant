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
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.ViewModels;

namespace YoungMomsAssistant.UI.Views.CustomControls {
    /// <summary>
    /// Interaction logic for AllBabiesControl.xaml
    /// </summary>
    public partial class AllBabiesControl : UserControl {
        public AllBabiesControl() {
            InitializeComponent();
        }

        [Dependency]
        public AllBabiesViewModel ViewModel {
            get => DataContext as AllBabiesViewModel;
            set => DataContext = value;
        }

        public async void UpdateBabyDetails_Click(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                button.IsEnabled = false;
                await ViewModel?.UpdateBabyDetailsAsync(button?.DataContext as Baby);
                button.IsEnabled = true;
            }  
        }

        public async void DeleteBaby_Click(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                button.IsEnabled = false;
                await ViewModel?.DeleteBabyAsync(button?.DataContext as Baby);
                button.IsEnabled = true;
            }
        }

        public void LoadImage_Click(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                button.IsEnabled = false;
                if (button.DataContext is Baby baby) {
                    ViewModel?.LoadImageCommand?.Execute(baby);
                }
                button.IsEnabled = true;
            }
        }
    }
}
