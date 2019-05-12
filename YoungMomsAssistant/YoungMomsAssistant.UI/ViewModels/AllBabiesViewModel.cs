using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class AllBabiesViewModel : ViewModelBase {

        private ObservableCollection<Baby> _babies;

        private TemplatesNavigationService _navigationService;
        private WindowsService _windowsService;
        private IBabiesService _babiesService;

        public AllBabiesViewModel(
            IBabiesService babiesService,
            TemplatesNavigationService navigationService,
            WindowsService windowsService) {

            _babiesService = babiesService;
            _navigationService = navigationService;
            _windowsService = windowsService;

            DownloadBabiesAsync();
        }

        public ObservableCollection<Baby> Babies {
            get => _babies;
            set {
                _babies = value;
                OnPropertyChanged();
            }
        }

        private async void DownloadBabiesAsync() {
            try {
                var result = await _babiesService.GetAllAsync();
                Babies = new ObservableCollection<Baby>(result);
            }
            catch (NotOkResponseException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred (code: {ex.Message})", "dialogHost");
            }
            catch (AuthorizationException ex) {
                await _windowsService.OpenErrorDialogAsync("An Authorization error has occurred", "dialogHost");
            }
            catch (HttpRequestException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred", "dialogHost");
            }
            catch (Exception e){
                await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
            }
        }
    }
}
