using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Infrastructure.Commands.Generic;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class AllBabiesViewModel : ViewModelBase {

        private ObservableCollection<Baby> _babies;
        private Baby _babyToAdd;

        private bool _isUpdateListComplete;

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

            AddNewBabyCommand = new RelayCommand(AddNewBabyCommandExecute, AddNewBabyCommandCanExecut);
            UpdateListCommand = new RelayCommand(UpdateListCommandExecute);

            UpdateListCommand?.Execute(null);
        }

        public ObservableCollection<Baby> Babies {
            get => _babies;
            set {
                _babies = value;
                OnPropertyChanged();
            }
        }

        public Baby BabyToAdd {
            get {
                if (_babyToAdd == null) {
                    _babyToAdd = new Baby {
                        BirthDay = DateTime.Now
                    };
                }

                return _babyToAdd;
            }

            set {
                _babyToAdd = value;
                OnPropertyChanged();
            }
        }

        public bool IsUpdateListComplete {
            get => _isUpdateListComplete;
            set {
                _isUpdateListComplete = value;
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

        #region Commands

        private async void AddNewBabyCommandExecute(object obj) {
            try {
                await _babiesService.AddAsync(BabyToAdd);
                UpdateListCommand?.Execute(null);
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

        private bool AddNewBabyCommandCanExecut(object obj) {
            return string.IsNullOrWhiteSpace(BabyToAdd.Error);
        }

        private async void UpdateListCommandExecute(object obj) {
            try {
                IsUpdateListComplete = false;
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
            finally {
                IsUpdateListComplete = true;
            }
        }

        public ICommand AddNewBabyCommand { get; }

        public ICommand UpdateListCommand { get; }

        #endregion
    }
}
