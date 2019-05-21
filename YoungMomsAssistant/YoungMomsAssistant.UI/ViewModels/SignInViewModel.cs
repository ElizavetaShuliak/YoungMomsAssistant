using System.Net.Http;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class SignInViewModel : ViewModelBase {

        private WindowsService _windowsService;
        private IAuthenticationService _authenticationService;
        private IAuthorizationTokensService _authorizationTokensService;
        private SignInModel _signInModel;
        private bool _isSignInWaiting;

        public SignInViewModel(
            WindowsService windowsService,
            IAuthenticationService authenticationService,
            IAuthorizationTokensService authorizationTokensService) {

            _windowsService = windowsService;
            _authenticationService = authenticationService;
            _authorizationTokensService = authorizationTokensService;

            SignInCommand = new RelayCommand(SignInCommandExecute, SignInCommandCanExecute);
            SignUpWindowOpenCommand = new RelayCommand(SignUpWindowOpenCommandExecute);
        }

        public SignInModel SignInModel {
            get {
                if (_signInModel == null) {
                    _signInModel = new SignInModel();
                }

                return _signInModel;
            }

            set {
                _signInModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsSignInWaiting {
            get => _isSignInWaiting;
            set {
                _isSignInWaiting = value;
                OnPropertyChanged();
            }
        }

        private void SignIn(JwtTokens jwtTokens) {
            _authorizationTokensService.Tokens = jwtTokens;
            _windowsService.NavigateToMainWindow(ClosableWindow);
        }

        #region Commands

        private async void SignInCommandExecute(object param) {
            try {
                IsSignInWaiting = true;
                var tokens = await _authenticationService
                .SignInAsync(SignInModel.ConvertToUser());

                SignIn(tokens);
            }
            catch (NotOkResponseException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred (code: {ex.Message})", "dialogHost");
            }
            catch (HttpRequestException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred", "dialogHost");
            }
            catch {
                await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
            }
            finally {
                IsSignInWaiting = false;
            }
        }

        private bool SignInCommandCanExecute(object param) {
            return string.IsNullOrWhiteSpace(SignInModel["Password"])
                && string.IsNullOrWhiteSpace(SignInModel.Error);
        }

        private void SignUpWindowOpenCommandExecute(object obj) {
            _windowsService.NavigateToSignUpWindow(ClosableWindow);
        }

        public ICommand SignInCommand { get; }

        public ICommand SignUpWindowOpenCommand { get; }

        #endregion
    }
}
