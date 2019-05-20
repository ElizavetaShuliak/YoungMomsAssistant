using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using YoungMomsAssistant.UI.Views.CustomControls;

namespace YoungMomsAssistant.UI.Services {
    public class TemplatesNavigationService : INotifyPropertyChanged {

        private List<UserControl> _navigationList;
        private int _currentTemplateIndex = -1;
        private UserControl _currentTemplate;

        public TemplatesNavigationService() {
            _navigationList = new List<UserControl>();
        }

        public UserControl CurrentTemplate {
            get => _currentTemplate;
            set {
                _currentTemplate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NavigateBack() {
            if (CanNavigateBack()) {
                CurrentTemplate = _navigationList[--_currentTemplateIndex];
            }
        }

        public void NavigateForward() {
            if (CanNavigateForward()) {
                CurrentTemplate = _navigationList[++_currentTemplateIndex];
            }
        }

        public bool CanNavigateBack() => _currentTemplateIndex > 0;

        public bool CanNavigateForward() => _currentTemplateIndex < _navigationList.Count - 1;

        public void ClearNavigationList() {
            _navigationList.Clear();
            CurrentTemplate = null;
        }

        #region Templates to navigate to

        [Unity.Dependency]
        public Func<AllBabiesControl> AllBabiesControlFactory { get; set; }

        [Unity.Dependency]
        public Func<LifeEventsControl> LifeEventsControlFactory { get; set; }

        public void NavigateToAllBabies() {
            if (CurrentTemplate?.GetType() == typeof(AllBabiesControl)) {
                return;
            }

            CurrentTemplate = AllBabiesControlFactory?.Invoke();

            AddToNavigationList();
        }

        public void NavigateToLifeEvents() {
            if (CurrentTemplate?.GetType() == typeof(LifeEventsControl)) {
                return;
            }

            CurrentTemplate = LifeEventsControlFactory?.Invoke();

            AddToNavigationList();
        }

        private void AddToNavigationList() {
            if (_currentTemplateIndex < _navigationList.Count - 1) {
                _navigationList[++_currentTemplateIndex] = CurrentTemplate;
                _navigationList.RemoveRange(_currentTemplateIndex + 1, _navigationList.Count - _currentTemplateIndex - 1);
            }
            else {
                _navigationList.Add(CurrentTemplate);
                _currentTemplateIndex++;
            }
        }

        #endregion

        virtual protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
