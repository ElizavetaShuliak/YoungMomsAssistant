using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Infrastructure.Commands.Generic;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class MainViewModel : ViewModelBase {

        private WindowsService _windowsService;

        public MainViewModel(
            TemplatesNavigationService navigationService,
            WindowsService windowsService) {

            NavigationService = navigationService;

            SignOutCommand = new RelayCommand(SignOutCommandExecute);
            OpenLifeEventsCommand = new RelayCommand(OpenLifeEventsCommandExecute);
            NavigateCommand = new RelayCommand<string>(NavigateCommandExecute, NavigateCommandCanExecute);

            NavigationService.NavigateToAllBabies();
            _windowsService = windowsService;
        }

        public TemplatesNavigationService NavigationService { get; }

        #region Commands

        private void SignOutCommandExecute(object obj) {
            NavigationService.ClearNavigationList();
            _windowsService.NaviagteToSignInWindow(ClosableWindow);
        }

        private void OpenLifeEventsCommandExecute(object obj) {
            NavigationService.NavigateToLifeEvents();
        }

        private void NavigateCommandExecute(string obj) {
            switch (obj) {
                case "back":
                    NavigationService.NavigateBack();
                    break;
                case "forward":
                    NavigationService.NavigateForward();
                    break;
                case "home":
                    NavigationService.NavigateToAllBabies();
                    break;
            }
        }

        private bool NavigateCommandCanExecute(string obj) {
            switch (obj) {
                case "back":
                    return NavigationService.CanNavigateBack();
                case "forward":
                    return NavigationService.CanNavigateForward();
                default: return true;
            }
        }

        public ICommand SignOutCommand { get; }

        public ICommand OpenLifeEventsCommand { get; }

        public ICommand NavigateCommand { get; }

        #endregion
    }
}
