using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.CustomControls;
using YoungMomsAssistant.UI.Views.Windows;

namespace YoungMomsAssistant.UI.Services {
    public class WindowsService {

        private Func<MainWindow> _mainWindowFactory;
        private Func<SignInWindow> _signInFactory;
        private Func<SignUpWindow> _signUpFactory;

        public WindowsService(
            Func<MainWindow> mainWindowFactory,
            Func<SignInWindow> signInFactory,
            Func<SignUpWindow> signUpFactory) {

            _mainWindowFactory = mainWindowFactory;
            _signInFactory = signInFactory;
            _signUpFactory = signUpFactory;
        }

        public void NavigateToMainWindow(IClosable current) {
            _mainWindowFactory?.Invoke()?.Show();
            current?.Close();
        }

        public void NaviagteToSignInWindow(IClosable current) {
            _signInFactory?.Invoke()?.Show();
            current?.Close();
        }

        public void NavigateToSignUpWindow(IClosable current) {
            _signUpFactory?.Invoke().Show();
            current?.Close();
        }

        public async Task OpenErrorDialogAsync(string error, string hostName) {
            var content = new ErrorDialogContent {
                Message = { Text = error }
            };
            await DialogHost.Show(content, hostName);
        }
    }
}
