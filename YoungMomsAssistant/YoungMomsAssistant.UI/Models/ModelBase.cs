using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YoungMomsAssistant.UI.Models {
    public class ModelBase : INotifyPropertyChanged {

        protected Dictionary<string, string> _errors = new Dictionary<string, string>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}