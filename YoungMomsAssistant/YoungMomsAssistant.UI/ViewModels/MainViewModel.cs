using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class MainViewModel {
        public MainViewModel(TemplatesNavigationService navigationService) {
            NavigationService = navigationService;

            NavigationService.NavigateToAllBabies();
        }

        public TemplatesNavigationService NavigationService { get; }
    }
}
