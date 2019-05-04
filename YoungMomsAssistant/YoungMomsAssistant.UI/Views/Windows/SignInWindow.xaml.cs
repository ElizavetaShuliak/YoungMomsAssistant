using System.Windows;
using Unity;
using YoungMomsAssistant.UI.ViewModels;

namespace YoungMomsAssistant.UI.Views.Windows {
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window, IClosable {
        public SignInWindow() {
            InitializeComponent();
        }

        [Dependency]
        public SignInViewModel ViewModel {
            set {
                value.ClosableWindow = this;
                DataContext = value;
            }
        }

        private void PasswordPb_PasswordChanged(object sender, RoutedEventArgs e) {
            if (DataContext is SignInViewModel vm) {
                vm.SignInModel.Password = passwordPb.Password;
            }
        }
    }
}
