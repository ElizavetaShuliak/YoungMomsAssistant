using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands.Generic;
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

        public async Task UpdateBabyDetails(Baby baby) {
            try {
                await _babiesService.UpdateAsync(baby);
            }
            catch (NotOkResponseException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred (code: {ex.Message})", "dialogHost");
            }
            catch (AuthorizationException ex) {
                await _windowsService.OpenErrorDialogAsync("An Authorization error has occurred", "dialogHost");
                //
            }
            catch (HttpRequestException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred", "dialogHost");
            }
            catch {
                await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
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
                //
            }
            catch (HttpRequestException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred", "dialogHost");
            }
            catch {
                await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
            }
        }

        #region Commands

        

        #endregion
    }
}
