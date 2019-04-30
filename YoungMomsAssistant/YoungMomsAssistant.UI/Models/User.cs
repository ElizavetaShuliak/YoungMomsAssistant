using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Models {
    class User : ModelBase {

        private string _login;
        private string _email;
        private string _password;

        public string Login {
            get => _login;
            set {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Email {
            get => _email;
            set {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged();
            }
        }
    }
}
