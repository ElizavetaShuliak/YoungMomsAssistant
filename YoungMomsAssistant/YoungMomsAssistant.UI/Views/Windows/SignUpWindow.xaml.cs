using System.Windows;
using Unity;
using YoungMomsAssistant.UI.ViewModels;

namespace YoungMomsAssistant.UI.Views.Windows {
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window, IClosable {
        public SignUpWindow() {
            InitializeComponent();
        }

        [Dependency]
        public SignUpViewModel ViewModel {
            set {
                value.ClosableWindow = this;
                DataContext = value;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            if (DataContext is SignUpViewModel vm) {
                vm.UserData.Password = passwordPb.Password;
            }
        }
    }
}
