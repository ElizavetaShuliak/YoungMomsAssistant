using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YoungMomsAssistant.UI.Services {
    public class TemplatesNavigationService : INotifyPropertyChanged {

        private List<UserControl> _navigationList;
        private int _currentTemplateIndex;
        private UserControl _currentTemplate;

        public TemplatesNavigationService(UserControl homeTemplate) {
            _navigationList = new List<UserControl> {
                homeTemplate
            };
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
                CurrentTemplate = _navigationList[_currentTemplateIndex--];
            }
        }

        public void NavigateForward() {
            if (CanNavigateForward()) {
                CurrentTemplate = _navigationList[_currentTemplateIndex++];
            }
        }

        public bool CanNavigateBack() => _currentTemplateIndex > 0;

        public bool CanNavigateForward() => _currentTemplateIndex < _navigationList.Count;

        virtual protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
