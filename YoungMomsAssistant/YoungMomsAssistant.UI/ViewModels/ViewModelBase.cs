using System.ComponentModel;
using System.Runtime.CompilerServices;
using YoungMomsAssistant.UI.Views.Windows;

namespace YoungMomsAssistant.UI.ViewModels {
    public abstract class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public IClosable ClosableWindow { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
