using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class SignUpViewModel : ViewModelBase {

        private IAuthenticationService _authenticationService;
        private WindowsService _windowsService;

        private User _userData;
        private bool _isSignUpWaiting;

        public SignUpViewModel(
            IAuthenticationService authenticationService,
            WindowsService windowsService) {
            _authenticationService = authenticationService;
            _windowsService = windowsService;

            SignUpCommand = new RelayCommand(SignUpCommandExecute, SignUpCommandCanExecute);
            BackToSignInCommand = new RelayCommand(BackToSignInCommandExecute);
        }

        public User UserData {
            get {
                if (_userData == null) {
                    _userData = new User();
                }

                return _userData;
            }

            set {
                _userData = value;
                OnPropertyChanged();
            }
        }

        public bool IsSignUpWaiting {
            get => _isSignUpWaiting;
            set {
                _isSignUpWaiting = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        private async void SignUpCommandExecute(object obj) {
            try {
                IsSignUpWaiting = true;
                var result = await _authenticationService.SignUpAsync(UserData);

                if (result) {
                    //TODO: popup
                    _windowsService.NaviagteToSignInWindow(ClosableWindow);
                }
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
                IsSignUpWaiting = false;
            }
        }

        private bool SignUpCommandCanExecute(object obj) {
            return
                string.IsNullOrWhiteSpace(UserData["Password"])
                && string.IsNullOrWhiteSpace(UserData.Error);
        }

        private void BackToSignInCommandExecute(object obj) {
            _windowsService.NaviagteToSignInWindow(ClosableWindow);
        }

        public ICommand SignUpCommand { get; }

        public ICommand BackToSignInCommand { get; }

        #endregion
    }
}
