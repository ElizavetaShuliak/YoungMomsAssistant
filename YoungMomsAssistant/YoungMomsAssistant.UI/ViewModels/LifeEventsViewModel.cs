using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YoungMomsAssistant.UI.Infrastructure.Commands;
using YoungMomsAssistant.UI.Infrastructure.Commands.Generic;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class LifeEventsViewModel : ViewModelBase {

        private LifeEvent _lifeEventToAdd;
        private LifeEvent _lifeEventToEdit;
        private string _imageToAddPath;
        private string _imageToEditPath;
        private bool _isUpdateListComplete;
        private ObservableCollection<LifeEvent> _lifeEvents;

        private ILifeEventsService _lifeEventsService;
        private WindowsService _windowsService;
        private DateTime _selectedDate = DateTime.Today;

        public LifeEventsViewModel(
            ILifeEventsService lifeEventsService,
            WindowsService windowsService) {

            _lifeEventsService = lifeEventsService;
            _windowsService = windowsService;

            AddNewLifeEventCommand = new RelayCommand(AddNewLifeEventCommandExecute, AddNewLifeEventCommandCanExecute);
            LoadImageCommand = new RelayCommand<string>(LoadImageCommandExecute);
            UpdateListCommand = new RelayCommand(UpdateListCommandExecute);
            UpdateLifeEventCommand = new RelayCommand(UpdateLifeEventCommandExecute, UpdateLifeEventCommandCanExecute);
            CancelEditLifeEventCommand = new RelayCommand(CancelEditLifeEventCommandExecute);

            UpdateListCommand?.Execute(null);
        }

        public LifeEvent LifeEventToAdd {
            get {
                if (_lifeEventToAdd == null) {
                    _lifeEventToAdd = new LifeEvent();
                }

                return _lifeEventToAdd;
            }
            set {
                _lifeEventToAdd = value;
                OnPropertyChanged();
            }
        }

        public LifeEvent LifeEventToEdit {
            get => _lifeEventToEdit;
            set {
                _lifeEventToEdit = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LifeEvent> LifeEvents {
            get => _lifeEvents;
            set {
                _lifeEvents = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate {
            get => _selectedDate;
            set {
                _selectedDate = value;
                UpdateListCommand?.Execute(null);
            }
        }

        public string ImageToAddPath {
            get => _imageToAddPath;
            set {
                _imageToAddPath = value;
                OnPropertyChanged();
            }
        }

        public string ImageToEditPath {
            get => _imageToEditPath;
            set {
                _imageToEditPath = value;
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

        #region Commands

        private async void AddNewLifeEventCommandExecute(object obj) {
            try {
                await _lifeEventsService.AddAsync(LifeEventToAdd);
                LifeEventToAdd = null;
                ImageToAddPath = null;
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

        private bool AddNewLifeEventCommandCanExecute(object obj) {
            return string.IsNullOrWhiteSpace(LifeEventToAdd["Image"]) && string.IsNullOrWhiteSpace(LifeEventToAdd.Error);
        }

        private async void LoadImageCommandExecute(string obj) {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true) {
                try {
                    new BitmapImage(new Uri(openFileDialog.FileName));

                    using (var stream = openFileDialog.OpenFile()) {
                        if (obj == "add") {
                            await stream.ReadAsync(LifeEventToAdd.Image = new byte[(int)stream.Length], 0, (int)stream.Length);
                            ImageToAddPath = openFileDialog.FileName;
                        }
                        else if (obj == "edit") {
                            await stream.ReadAsync(LifeEventToEdit.Image = new byte[(int)stream.Length], 0, (int)stream.Length);
                            ImageToEditPath = openFileDialog.FileName;
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

        private async void UpdateLifeEventCommandExecute(object obj) {
            try {
                await _lifeEventsService.UpdateAsync(LifeEventToEdit);
                LifeEventToEdit = null;
                ImageToEditPath = null;
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

        private bool UpdateLifeEventCommandCanExecute(object obj) {
            return LifeEventToEdit != null
                && string.IsNullOrWhiteSpace(LifeEventToEdit["Image"])
                && string.IsNullOrWhiteSpace(LifeEventToEdit.Error);
        }

        private void CancelEditLifeEventCommandExecute(object obj) {
            LifeEventToEdit = null;
            ImageToEditPath = null;
        }

        private async void UpdateListCommandExecute(object obj) {
            try {
                IsUpdateListComplete = false;
                var result = await _lifeEventsService.GetByDateAsync(SelectedDate);
                LifeEvents = new ObservableCollection<LifeEvent>(result);
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

        public ICommand AddNewLifeEventCommand { get; }

        public ICommand LoadImageCommand { get; }

        public ICommand UpdateListCommand { get; }

        public ICommand UpdateLifeEventCommand { get; }

        public ICommand CancelEditLifeEventCommand { get; }

        #endregion
    }
}
