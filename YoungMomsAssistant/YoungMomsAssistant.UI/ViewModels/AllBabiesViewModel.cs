using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Infrastructure.Commands.Generic;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class AllBabiesViewModel : ViewModelBase {

        private ObservableCollection<Baby> _babies;
        private Baby _babyToAdd;

        private bool _isUpdateListComplete = true;
        private bool _isAddComplete = true;
        private string _imageToAddPath;

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
            LoadImageCommand = new RelayCommand<Baby>(LoadImageCommandExecute);

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

        public bool IsAddComplete {
            get => _isAddComplete;
            set {
                _isAddComplete = value;
                OnPropertyChanged();
            }
        }

        public string ImageToAddPath {
            get => _imageToAddPath;
            set {
                _imageToAddPath = value;
                OnPropertyChanged();
            }
        }

        public async Task UpdateBabyDetailsAsync(Baby baby) {
            try {
                await _babiesService.UpdateAsync(baby);
            }
            catch (NotOkResponseException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred (code: {ex.Message})", "dialogHost");
            }
            catch (AuthorizationException ex) {
                await _windowsService.OpenErrorDialogAsync("An Authorization error has occurred", "dialogHost");
                _windowsService.NaviagteToSignInWindow(ClosableWindow);
            }
            catch (HttpRequestException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred", "dialogHost");
            }
            catch {
                await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
            }
        }

        public async Task DeleteBabyAsync(Baby baby) {
            try {
                await _babiesService.DeleteAsync(baby.Id);

                var babyToRemove = Babies.FirstOrDefault(b => b.Id == baby.Id);

                if (babyToRemove != null) {
                    Babies.Remove(babyToRemove);
                }
            }
            catch (NotNoContentResponseException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred (code: {ex.Message})", "dialogHost");
            }
            catch (AuthorizationException ex) {
                await _windowsService.OpenErrorDialogAsync("An Authorization error has occurred", "dialogHost");
                _windowsService.NaviagteToSignInWindow(ClosableWindow);
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
                IsAddComplete = false;
                var addedBaby = await _babiesService.AddAsync(BabyToAdd);
                Babies.Add(addedBaby);
                BabyToAdd = null;
                ImageToAddPath = null;
            }
            catch (NotCreatedResponseException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred (code: {ex.Message})", "dialogHost");
            }
            catch (AuthorizationException ex) {
                await _windowsService.OpenErrorDialogAsync("An Authorization error has occurred", "dialogHost");
                _windowsService.NaviagteToSignInWindow(ClosableWindow);
            }
            catch (HttpRequestException ex) {
                await _windowsService.OpenErrorDialogAsync($"An request error has occurred", "dialogHost");
            }
            catch {
                await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
            }
            finally {
                IsAddComplete = true;
            }
        }

        private bool AddNewBabyCommandCanExecut(object obj) {
            return string.IsNullOrWhiteSpace(BabyToAdd["Sex"])
                && string.IsNullOrWhiteSpace(BabyToAdd["BloodType"])
                && string.IsNullOrWhiteSpace(BabyToAdd["Image"])
                && string.IsNullOrWhiteSpace(BabyToAdd.Error);
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
                _windowsService.NaviagteToSignInWindow(ClosableWindow);
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

        private async void LoadImageCommandExecute(Baby obj) {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true) {
                try {
                    new BitmapImage(new Uri(openFileDialog.FileName));

                    using (var stream = openFileDialog.OpenFile()) {
                        if (obj == null) {
                            await stream.ReadAsync(BabyToAdd.Image = new byte[(int)stream.Length], 0, (int)stream.Length);
                            ImageToAddPath = openFileDialog.FileName;
                        }
                        else {
                            byte[] bytes;
                            await stream.ReadAsync(bytes = new byte[(int)stream.Length], 0, (int)stream.Length);
                            obj.Image = bytes;
                        }
                    }
                }
                catch (NotSupportedException ex) {
                    await _windowsService.OpenErrorDialogAsync("Bad image format", "dialogHost");
                }
                catch (IOException ex) {
                    await _windowsService.OpenErrorDialogAsync("Open file error", "dialogHost");
                }
                catch {
                    await _windowsService.OpenErrorDialogAsync("An unexpected error has occurred", "dialogHost");
                }
            }
        }

        public ICommand AddNewBabyCommand { get; }

        public ICommand UpdateListCommand { get; }

        public ICommand LoadImageCommand { get; }

        #endregion
    }
}
