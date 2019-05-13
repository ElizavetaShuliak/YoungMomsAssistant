using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class MainViewModel : ViewModelBase {

        private WindowsService _windowsService;

        public MainViewModel(
            TemplatesNavigationService navigationService,
            WindowsService windowsService) {

            NavigationService = navigationService;

            SignOutCommand = new RelayCommand(SignOutCommandExecute);

            NavigationService.NavigateToAllBabies();
            _windowsService = windowsService;
        }

        public TemplatesNavigationService NavigationService { get; }

        #region Commands

        private void SignOutCommandExecute(object obj) {
            _windowsService.NaviagteToSignInWindow(ClosableWindow);
        }

        public ICommand SignOutCommand { get;}

        #endregion
    }
}
