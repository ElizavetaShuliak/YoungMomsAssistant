using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using YoungMomsAssistant.UI.Infrastructure.Commands.Generic;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.Services;

namespace YoungMomsAssistant.UI.ViewModels {
    public class BabyDetailsViewModel : ViewModelBase {

        private SeriesCollection _weightSeries;
        private SeriesCollection _growthSeries;
        private ObservableCollection<string> _weightLables;
        private ObservableCollection<string> _growthLables;
        private BabyWeight _babyWeightToAdd;
        private BabyGrowth _babyGrowthToAdd;

        private Visibility _weightChartVisibility = Visibility.Collapsed;
        private Visibility _growthChartVisibility = Visibility.Collapsed;


        private IBabiesService _babiesService;
        private WindowsService _windowsService;

        public BabyDetailsViewModel(IBabiesService babiesService, WindowsService windowsService) {
            _babiesService = babiesService;
            _windowsService = windowsService;

            OpenChartCommand = new RelayCommand<string>(OpenChartCommandExecute, OpenChartCommandCanExecute);
            UpdateChartCommand = new RelayCommand<string>(UpdateChartCommandExecute, UpdateChartCommandCanExecute);
            AddSerieCommand = new RelayCommand<string>(AddSerieCommandExecute, AddSerieCommandCanExecute);
        }

        public Baby SelectedBaby { get; set; }

        public BabyWeight BabyWeightToAdd {
            get {
                if (_babyWeightToAdd == null) {
                    _babyWeightToAdd = new BabyWeight {
                        BabyId = SelectedBaby.Id,
                        Date = DateTime.Now.Date
                    };
                }

                return _babyWeightToAdd;
            }
            set {
                _babyWeightToAdd = value;
                OnPropertyChanged();
            }
        }

        public BabyGrowth BabyGrowthToAdd {
            get {
                if (_babyGrowthToAdd == null) {
                    _babyGrowthToAdd = new BabyGrowth {
                        BabyId = SelectedBaby.Id,
                        Date = DateTime.Now.Date
                    };
                }

                return _babyGrowthToAdd;
            }
            set {
                _babyGrowthToAdd = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection WeightSeries {
            get {
                if (_weightSeries == null) {
                    _weightSeries = new SeriesCollection();
                }

                return _weightSeries;
            }
            set {
                _weightSeries = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> WeightLables {
            get {
                if (_weightLables == null) {
                    _weightLables = new ObservableCollection<string>();
                }
                return _weightLables;
            }
            set {
                _weightLables = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection GrowthSeries {
            get {
                if (_growthSeries == null) {
                    _growthSeries = new SeriesCollection();
                }

                return _growthSeries;
            }
            set {
                _growthSeries = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> GrowthLables {
            get {
                if (_growthLables == null) {
                    _growthLables = new ObservableCollection<string>();
                }
                return _growthLables;
            }
            set {
                _growthLables = value;
                OnPropertyChanged();
            }
        }

        public Visibility WeightChartVisibility {
            get => _weightChartVisibility;
            set {
                _weightChartVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility GrowthChartVisibility {
            get => _growthChartVisibility;
            set {
                _growthChartVisibility = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        private async void OpenChartCommandExecute(string obj) {
            try {
                switch (obj) {
                    case "weight":
                        if (WeightSeries == null || WeightSeries.Count == 0) {
                            UpdateChartCommand?.Execute("weight");
                        }

                        SwitchCharts("weight");
                        break;
                    case "growth":
                        if (GrowthSeries == null || GrowthSeries.Count == 0) {
                            UpdateChartCommand?.Execute("growth");
                        }

                        SwitchCharts("growth");
                        break;
                }
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

        private bool OpenChartCommandCanExecute(string obj) {
            return SelectedBaby != null;
        }

        private async void UpdateChartCommandExecute(string obj) {
            try {
                switch (obj) {
                    case "weight":
                        var weights = await _babiesService.GetWeightsAsync(SelectedBaby.Id);
                        weights.Sort((a, b) => a.Date.CompareTo(b.Date));

                        WeightSeries.Clear();
                        WeightSeries.Add(
                            new LineSeries {
                                Title = "Weight",
                                Values = new ChartValues<double>(weights.Select(w => w.Weight))
                            });

                        WeightLables = new ObservableCollection<string>(weights.Select(w => w.Date.ToShortDateString()));

                        break;
                    case "growth":
                        var growths = await _babiesService.GetGrowthsAsync(SelectedBaby.Id);
                        growths.Sort((a, b) => a.Date.CompareTo(b.Date));

                        GrowthSeries.Clear();
                        GrowthSeries.Add(
                            new LineSeries {
                                Title = "Growth",
                                Values = new ChartValues<double>(growths.Select(w => w.Growth))
                            });

                        GrowthLables = new ObservableCollection<string>(growths.Select(g => g.Date.ToShortDateString()));

                        break;
                }
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

        private bool UpdateChartCommandCanExecute(string obj) {
            return SelectedBaby != null;
        }

        private async void AddSerieCommandExecute(string obj) {
            try {
                switch (obj) {
                    case "weight":
                        var weight = await _babiesService.AddWeightAsync(BabyWeightToAdd);

                        var index = WeightLables.IndexOf(weight.Date.ToShortDateString());
                        if (index >= 0) {
                            WeightSeries[0].Values[index] = weight.Weight;
                        }
                        else {
                            var lastSmallerDate = WeightLables.LastOrDefault(date => DateTime.Parse(date).Date < weight.Date.Date);

                            if (lastSmallerDate != null) {
                                index = WeightLables.IndexOf(lastSmallerDate) + 1;
                                if (index >= WeightLables.Count) {
                                    WeightSeries[0].Values.Add(weight.Weight);
                                    WeightLables.Add(weight.Date.ToShortDateString());
                                }
                                else {
                                    WeightLables.Insert(index, weight.Date.ToShortDateString());
                                    WeightSeries[0].Values.Insert(index, weight.Weight);
                                }
                            }
                            else {
                                WeightSeries[0].Values.Insert(0, weight.Weight);
                                WeightLables.Insert(0, weight.Date.ToShortDateString());
                            }
                        }

                        BabyWeightToAdd = null;
                        break;
                    case "growth":
                        var growth = await _babiesService.AddGrowthAsync(BabyGrowthToAdd);

                        index = GrowthLables.IndexOf(growth.Date.ToShortDateString());
                        if (index >= 0) {
                            GrowthSeries[0].Values[index] = growth.Growth;
                        }
                        else {
                            var lastSmallerDate = GrowthLables.LastOrDefault(date => DateTime.Parse(date).Date < growth.Date.Date);

                            if (lastSmallerDate != null) {
                                index = GrowthLables.IndexOf(lastSmallerDate) + 1;
                                if (index >= GrowthLables.Count) {
                                    GrowthSeries[0].Values.Add(growth.Growth);
                                    GrowthLables.Add(growth.Date.ToShortDateString());
                                }
                                else {
                                    GrowthLables.Insert(index, growth.Date.ToShortDateString());
                                    GrowthSeries[0].Values.Insert(index, growth.Growth);
                                }
                            }
                            else {
                                GrowthSeries[0].Values.Add(growth.Growth);
                                GrowthLables.Add(growth.Date.ToShortDateString());
                            }
                        }

                        BabyGrowthToAdd = null;
                        break;
                }
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

        private bool AddSerieCommandCanExecute(string obj) {
            switch (obj) {
                case "weight":
                    return string.IsNullOrWhiteSpace(BabyWeightToAdd.Error) && WeightSeries.Count > 0;
                case "growth":
                    return string.IsNullOrWhiteSpace(BabyGrowthToAdd.Error) && GrowthSeries.Count > 0;
                default:
                    return false;
            }
        }

        public ICommand OpenChartCommand { get; }

        public ICommand UpdateChartCommand { get; }

        public ICommand AddSerieCommand { get; }

        #endregion

        private void SwitchCharts(string chartName) {
            switch (chartName) {
                case "weight":
                    WeightChartVisibility = Visibility.Visible;
                    GrowthChartVisibility = Visibility.Collapsed;
                    break;
                case "growth":
                    WeightChartVisibility = Visibility.Collapsed;
                    GrowthChartVisibility = Visibility.Visible;
                    break;
            }
        }
    }
}
